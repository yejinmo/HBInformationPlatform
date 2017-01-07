using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace InformationEngine.CheckCodeEngine
{
    public class X2Engine
    {

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="bt"></param>
        /// <returns></returns>
        public string Get(Bitmap bt)
        {
            string[] str = GetPartition(DeNoise(GetSingleBmpCode(BitmapTo1Bpp(bt, 0.8), 1)));
            return Discern(str[0]) + Discern(str[1]) + Discern(str[2]) + Discern(str[3]);
        }

        /// <summary>
        /// 返回灰度图片的点阵描述字串，1表示灰点，0表示背景
        /// </summary>
        /// <param name="singlepic">灰度图</param>
        /// <param name="dgGrayValue">背前景灰色界限</param>
        /// <returns></returns>
        private string GetSingleBmpCode(Bitmap singlepic, int dgGrayValue)
        {
            Color piexl;
            string code = "";
            for (int posy = 0; posy < singlepic.Height; posy++)
            {
                for (int posx = 0; posx < singlepic.Width; posx++)
                {
                    piexl = singlepic.GetPixel(posx, posy);
                    if (piexl.R < dgGrayValue)  // Color.Black )
                        code = code + "1";
                    else
                        code = code + "0";
                }
                code = code + "\r\n";
            }
            return code;
        }

        /// <summary>
        /// 图片二值化
        /// </summary>
        /// <param name="bmpobj">原图</param>
        /// <param name="hsb">hsb</param>
        private Bitmap BitmapTo1Bpp(Bitmap bmpobj, double hsb)
        {
            int w = bmpobj.Width;
            int h = bmpobj.Height;
            Bitmap bmp = new Bitmap(w, h, PixelFormat.Format1bppIndexed);
            BitmapData data = bmp.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadWrite, PixelFormat.Format1bppIndexed);
            for (int y = 0; y < h; y++)
            {
                byte[] scan = new byte[(w + 7) / 8];
                for (int x = 0; x < w; x++)
                {
                    Color c = bmpobj.GetPixel(x, y);
                    if (c.GetBrightness() >= hsb) scan[x / 8] |= (byte)(0x80 >> (x % 8));
                }
                Marshal.Copy(scan, 0, (IntPtr)((int)data.Scan0 + data.Stride * y), scan.Length);
            }
            bmp.UnlockBits(data);
            return bmp;
        }

        /// <summary>
        /// 降噪操作
        /// </summary>
        /// <param name="src">源</param>
        /// <returns></returns>
        private string DeNoise(string src)
        {
            char chr = '0';
            string ans = "";
            char[] temp = new char[72];
            string[] str = src.Replace("\r", "").Split('\n');
            for (int i = 0; i < 30; i++)
            {
                for (int j = 0; j < 72; j++)
                {
                    if (i <= 2 || i >= 28 || j <= 2 || j >= 70)
                    {
                        temp[j] = chr;
                        continue;
                    }
                    if (str[i][j] == chr)
                    {
                        temp[j] = chr;
                        continue;
                    }
                    int res = 0;
                    if (str[i - 1][j] == chr)
                        res++;
                    if (str[i + 1][j] == chr)
                        res++;
                    if (str[i][j - 1] == chr)
                        res++;
                    if (str[i][j + 1] == chr)
                        res++;
                    if (str[i - 1][j + 1] != chr && str[i + 1][j - 1] != chr || str[i - 1][j - 1] != chr && str[i + 1][j + 1] != chr)
                        res -= 2;
                    if (res == 4)
                    {
                        temp[j] = chr;
                        continue;
                    }
                    if (str[i][j + 2] != chr && str[i][j - 2] != chr || str[i + 2][j] != chr && str[i - 2][j] != chr)
                        res -= 2;
                    if (res >= 3)
                        temp[j] = chr;
                    else
                        temp[j] = '1';
                }
                ans = ans + new string(temp) + "\r\n";
            }
            return ans;
        }

        /// <summary>
        /// 分割区间
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        private string[] GetPartition(string src)
        {
            char chr = '0';
            string[] ans = new string[4];
            int ians = 0;
            int b_x = -1;
            int e_x = -1;
            char[] temp = new char[72];
            string[] str = src.Replace("\r", "").Split('\n');
            for (int i = 0; i < 72; ++i)
            {
                bool flag = true;
                for (int j = 0; j < 30; ++j)
                {
                    if (str[j][i] != chr)
                    {
                        flag = false;
                        break;
                    }
                }
                if (b_x == -1 && !flag)
                    b_x = i;
                if (e_x == -1 && b_x != -1 && flag)
                    e_x = i;
                if (b_x != -1 && e_x != -1)
                {
                    ans[ians] = GetSPstr(str, b_x, e_x);
                    b_x = -1;
                    e_x = -1;
                    flag = true;
                    ians++;
                }
            }
            return ans;
        }

        /// <summary>
        /// 去除无意义行
        /// </summary>
        /// <param name="str"></param>
        /// <param name="b_x"></param>
        /// <param name="e_x"></param>
        /// <returns></returns>
        private string GetSPstr(string[] str, int b_x, int e_x)
        {
            string tem = "";
            string res = "";
            for (int i = 0; i < 30; ++i)
            {
                for (int j = b_x; j < e_x; ++j)
                    tem += str[i][j];
                tem += "\n";
            }
            foreach (string s in tem.Split('\n'))
            {
                if (s.IndexOf('1') != -1)
                    res = res + s + "\r\n";
            }
            return res;
        }

        /// <summary>
        /// 获取长宽
        /// </summary>
        /// <param name="str"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void GetWidthAndHeight(string str, ref int x, ref int y)
        {
            string s = str;
            string[] temp = s.Split('\n');
            x = temp.Length - 1;
            s = s.Replace("\r", "").Replace("\n", "");
            y = s.Length / x;
        }

        /// <summary>
        /// 识别单个字符
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private string Discern(string s)
        {
            int x = 0, y = 0;
            GetWidthAndHeight(s, ref x, ref y);
            int[][] temp = new int[50][];
            string[] str = s.Replace("\r", "").Split('\n');
            for (int i = 0; i < str.Length; ++i)
            {
                temp[i] = new int[50];
                for (int j = 0; j < str[i].Length; ++j)
                    temp[i][j] = str[i][j] == '1' ? 1 : 0;
            }
            return new X2().Get(y, x, temp);
        }

    }
}
