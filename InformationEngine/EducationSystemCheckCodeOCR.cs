using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.IO;

namespace InformationEngine
{
    public class EducationSystemCheckCodeOCR
    {

        static Dictionary<Bitmap, string> TrainMap = null;
        static bool IsLoadTrainMap = false;

        string TrainPath = "TrainIamge\\";

        bool IsBlue(Color color)
        {
            int rgb = color.R + color.G + color.B;
            if (rgb == 153)
            {
                return true;
            }
            return false;
        }

        bool IsBlack(Color color)
        {
            if (color.R + color.G + color.B <= 100)
            {
                return true;
            }
            return false;
        }

        Bitmap removeBackgroud(Bitmap img)
        {
            img = getSubimage(img, 5, 1, img.Width - 5, img.Height - 2);
            img = getSubimage(img, 0, 0, 50, img.Height);
            int width = img.Width;
            int height = img.Height;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (IsBlue(img.GetPixel(x, y)))
                    {
                        img.SetPixel(x, y, Color.Black);
                    }
                    else
                    {
                        img.SetPixel(x, y, Color.White);
                    }
                }
            }
            return img;
        }

        Bitmap getSubimage(Bitmap b, int StartX, int StartY, int iWidth, int iHeight)
        {
            if (b == null)
            {
                return null;
            }
            int w = b.Width;
            int h = b.Height;
            if (StartX >= w || StartY >= h)
            {
                return null;
            }
            if (StartX + iWidth > w)
            {
                iWidth = w - StartX;
            }
            if (StartY + iHeight > h)
            {
                iHeight = h - StartY;
            }
            try
            {
                Bitmap bmpOut = new Bitmap(iWidth, iHeight, PixelFormat.Format24bppRgb);
                Graphics g = Graphics.FromImage(bmpOut);
                g.DrawImage(b, new Rectangle(0, 0, iWidth, iHeight), new Rectangle(StartX, StartY, iWidth, iHeight), GraphicsUnit.Pixel);
                g.Dispose();
                return bmpOut;
            }
            catch
            {
                return null;
            }
        }

        List<Bitmap> splitImage(Bitmap img)
        {
            List<Bitmap> subImgs = new List<Bitmap>();
            int width = img.Width / 4;
            int height = img.Height;
            subImgs.Add(getSubimage(img, 0, 0, width, height));
            subImgs.Add(getSubimage(img, width, 0, width, height));
            subImgs.Add(getSubimage(img, width * 2, 0, width, height));
            subImgs.Add(getSubimage(img, width * 3, 0, width, height));
            return subImgs;
        }

        public void loadTrainData()
        {
            if (TrainMap == null)
            {
                TrainMap = new Dictionary<Bitmap, string>();

                DirectoryInfo dir = new DirectoryInfo(TrainPath);
                FileInfo[] files = dir.GetFiles();

                foreach (FileInfo file in files)
                    TrainMap.Add(new Bitmap(Image.FromFile(file.FullName)), file.Name.ToArray()[0] + "");
                IsLoadTrainMap = true;
            }
        }

        string getSingleCharOcr(Bitmap img, Dictionary<Bitmap, string> map)
        {
            string result = "#";
            int width = img.Width;
            int height = img.Height;
            int min = width * height;
            foreach (Bitmap bi in map.Keys)
            {
                int count = 0;
                if (Math.Abs(bi.Width - width) > 2)
                    continue;
                int widthmin = width < bi.Width ? width : bi.Width;
                int heightmin = height < bi.Height ? height : bi.Height;
                bool flag = false;
                for (int x = 0; x < widthmin; ++x)
                {
                    for (int y = 0; y < heightmin; ++y)
                    {
                        if (IsBlack(img.GetPixel(x, y)) != IsBlack(bi.GetPixel(x, y)))
                        {
                            count++;
                            if (count >= min)
                            {
                                flag = true;
                                break;
                            }
                        }
                    }
                    if (flag)
                        break;
                }
                if (count < min)
                {
                    min = count;
                    map.TryGetValue(bi, out result);
                }
            }
            return result;
        }

        public string Get(Bitmap src)
        {
            if (!IsLoadTrainMap)
                loadTrainData();
            Bitmap img = removeBackgroud(src);
            List<Bitmap> listImg = splitImage(img);
            string result = "";
            foreach (Bitmap bi in listImg)
                result += getSingleCharOcr(bi, TrainMap);
            return result;
        }
    }
}
