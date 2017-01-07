using InformationEngine.Struct;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace InformationEngine
{
    public class EducationSystem
    {


        Struct_EducationSystem res_struct = new Struct_EducationSystem();
        string username;
        string password;

        public string Get(string USERNAME, string PASSWORD)
        {

            try
            {
                username = USERNAME;
                password = PASSWORD;
                string cookie = string.Empty;
                string __VIEWSTATE = string.Empty;
                string __VIEWSTATEGENERATOR = string.Empty;
                string html = string.Empty;
                string check_code = string.Empty;
                EducationSystemCheckCodeOCR ocr = new EducationSystemCheckCodeOCR();
                ocr.loadTrainData();

                #region 首页获取 cookie 、 __VIEWSTATE 及 __VIEWSTATEGENERATOR
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://10.10.8.68/default2.aspx");
                    request.CookieContainer = new CookieContainer();
                    request.Referer = "http://10.10.8.68/default2.aspx";
                    request.Accept = "Accept:text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                    request.Headers["Accept-Language"] = "zh-CN,zh;q=0.";
                    request.Headers["Accept-Charset"] = "GBK,utf-8;q=0.7,*;q=0.3";
                    request.UserAgent = "User-Agent:Mozilla/5.0 (Windows NT 5.1) AppleWebKit/535.1 (KHTML, like Gecko) Chrome/14.0.835.202 Safari/535.1";
                    request.KeepAlive = true;
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.Method = "GET";
                    request.AllowAutoRedirect = false;
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Stream myResponseStream = response.GetResponseStream();
                    StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                    string retString = myStreamReader.ReadToEnd();

                    Regex regResult__VIEWSTATE = new Regex("name=\"__VIEWSTATE\" value=\"(.+?)\"");
                    MatchCollection mcResul__VIEWSTATEt = regResult__VIEWSTATE.Matches(retString);
                    Match mc__VIEWSTATEt = mcResul__VIEWSTATEt[0];
                    __VIEWSTATE = mc__VIEWSTATEt.Groups[1].Value;

                    Regex regResult__VIEWSTATEGENERATOR = new Regex("name=\"__VIEWSTATEGENERATOR\" value=\"(.+?)\"");
                    //MatchCollection mcResul__VIEWSTATEGENERATOR = regResult__VIEWSTATEGENERATOR.Matches(retString);
                    //Match mc__VIEWSTATEGENERATOR = mcResul__VIEWSTATEGENERATOR[0];
                    //__VIEWSTATEGENERATOR = mc__VIEWSTATEGENERATOR.Groups[1].Value;

                    cookie = "ASP.NET_SessionId=" + response.Headers["Set-Cookie"];
                    Regex regResultSessionId = new Regex("ASP.NET_SessionId=(.+?); path=/");
                    MatchCollection mcResulSessionId = regResultSessionId.Matches(cookie);
                    Match mc = mcResulSessionId[0];
                    cookie = mc.Groups[1].Value;
                    myStreamReader.Close();
                    myResponseStream.Close();
                }
                #endregion

                bool flag_login = false;
                for (int code_err = 0; code_err < 5; code_err++)
                {
                    #region 获取并识别验证码
                    {
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://10.10.8.68/CheckCode.aspx");
                        request.Referer = "http://10.10.8.68/";
                        request.Accept = "Accept:text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                        request.Headers["Accept-Language"] = "zh-CN,zh;q=0.";
                        request.Headers["Accept-Charset"] = "GBK,utf-8;q=0.7,*;q=0.3";
                        request.Headers["Cookie"] = cookie;
                        request.UserAgent = "User-Agent:Mozilla/5.0 (Windows NT 5.1) AppleWebKit/535.1 (KHTML, like Gecko) Chrome/14.0.835.202 Safari/535.1";
                        request.KeepAlive = true;
                        request.ContentType = "application/x-www-form-urlencoded";
                        request.Method = "GET";

                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        Stream resStream = response.GetResponseStream();//得到验证码数据流
                        Bitmap bt = new Bitmap(resStream);//初始化Bitmap图片

                        string bt_path = @"temp\" + username + ".png";

                        //本地OCR识别
                        check_code = ocr.Get(bt);

                        //bt.Save(bt_path);
                        bt.Dispose();
                        //resStream.Dispose();


                        WebClient wc = new WebClient();
                        //check_code = wc.DownloadString("http://10.10.56.204:8080/WhxyJw/yzm.jsp?c=312fz&url=http://10.10.9.131/hbxy/temp/" + username + ".png").Replace("\r\n", "");
                        //check_code = wc.DownloadString("http://10.10.56.204:8080/WhxyJw/yzm.jsp?c=312fz&url=http://10.10.151.229/hbxy/temp/" + username + ".png").Replace("\r\n", "");
                        //wc.Dispose();

                        //if (File.Exists(bt_path))
                        //{
                        //    File.Delete(bt_path);
                        //}
                    }
                    #endregion

                    #region 模拟登陆操作
                    {
                        //string postDataStr = string.Format("__VIEWSTATE={0}&__VIEWSTATEGENERATOR={1}&txtUserName={2}&TextBox2={3}&txtSecretCode={4}&RadioButtonList1=%D1%A7%C9%FA&Button1=&lbLanguage=&hidPdrs=&hidsc=",
                        //    __VIEWSTATE.Replace("+", "%2B").Replace("=", "%3D").Replace("/", "%2F"),
                        //    __VIEWSTATEGENERATOR.Replace("+", "%2B").Replace("=", "%3D").Replace("/", "%2F"),
                        //    username, password, check_code);
                        string postDataStr = string.Format("__VIEWSTATE={0}&txtUserName={1}&TextBox2={2}&txtSecretCode={3}&RadioButtonList1=%D1%A7%C9%FA&Button1=&lbLanguage=&hidPdrs=&hidsc=",
                            __VIEWSTATE.Replace("+", "%2B").Replace("=", "%3D").Replace("/", "%2F"),
                            username, password, check_code);
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://10.10.8.68/default2.aspx");
                        request.Method = "POST";
                        request.Referer = "http://10.10.8.68/default2.aspx";
                        request.Host = "10.10.8.68";
                        request.Headers["Origin"] = "http://10.10.8.68";
                        request.Headers["Upgrade-Insecure-Requests"] = "1";
                        request.Accept = "Accept:text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                        request.Headers["Accept-Language"] = "zh-CN,zh;q=0.8";
                        request.Headers["Accept-Charset"] = "GBK,utf-8;q=0.7,*;q=0.3";
                        request.Headers["Cookie"] = cookie;
                        request.UserAgent = "User-Agent:Mozilla/5.0 (Windows NT 5.1) AppleWebKit/535.1 (KHTML, like Gecko) Chrome/14.0.835.202 Safari/535.1";
                        request.KeepAlive = true;
                        request.ContentType = "application/x-www-form-urlencoded";
                        request.ContentLength = postDataStr.Length;
                        Stream myRequestStream = request.GetRequestStream();
                        StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));
                        myStreamWriter.Write(postDataStr);
                        myStreamWriter.Close();
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        Stream myResponseStream = response.GetResponseStream();
                        StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("gb2312"));
                        string retString = myStreamReader.ReadToEnd();
                        myStreamReader.Close();
                        myResponseStream.Close();
                        if (retString.IndexOf("用户名不存在") > 0)
                            return ReturnResult("Username Error!");
                        if (retString.IndexOf("密码错误") > 0)
                            return ReturnResult("Password Error!");
                        if (retString.IndexOf("验证码不正确") > 0)
                            continue;
                        else
                        {
                            flag_login = true;
                            break;
                        }
                    }
                    #endregion
                }

                if (!flag_login)
                    return ReturnResult("Check Code Error!");

                #region 模拟获取课表操作
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://10.10.8.68/xskbcx.aspx?xh=" + username);
                    request.Referer = "http://10.10.8.68/xs_main.aspx?xh=" + username;
                    request.Accept = "Accept:text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                    request.Headers["Accept-Language"] = "zh-CN,zh;q=0.";
                    request.Headers["Accept-Charset"] = "GBK,utf-8;q=0.7,*;q=0.3";
                    request.Headers["Upgrade-Insecure-Requests"] = "1";
                    request.UserAgent = "User-Agent:Mozilla/5.0 (Windows NT 5.1) AppleWebKit/535.1 (KHTML, like Gecko) Chrome/14.0.835.202 Safari/535.1";
                    request.Headers["Cookie"] = cookie;
                    request.Host = "10.10.8.68";
                    request.KeepAlive = true;
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.Method = "GET";
                    request.AllowAutoRedirect = false;
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Stream myResponseStream = response.GetResponseStream();
                    StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("gb2312"));
                    string retString = myStreamReader.ReadToEnd();
                    myStreamReader.Close();
                    myResponseStream.Close();
                    html = retString;
                }
                #endregion

                #region 解析课表信息
                {
                    Regex regResultSessionId = new Regex("<td align=\"Center\">(.+?)</td>");
                    html = html.Replace(" width=\"7%\"", "").Replace(" rowspan=\"2\"", "");
                    MatchCollection mcResulSessionId = regResultSessionId.Matches(html);
                    if (mcResulSessionId.Count > 0)
                    {
                        string temp = string.Empty;
                        foreach (Match mc in mcResulSessionId)
                        {
                            temp += mc.Groups[1].Value + "\n\n";
                            string str = '|' + mc.Groups[1].Value + '|';
                            MatchCollection br_3 = new Regex(@"\|([^<|]*?)<br>([^<]*?)<br>([^<]*?)<br>([^<]*?)\|").Matches(str);
                            if (br_3.Count > 0)
                            {
                                res_struct.Data.CourseSchedule.Add(new Struct_EducationSystem.Course_Info
                                {
                                    Course_Name = br_3[0].Groups[1].Value,
                                    Day_Of_Week = br_3[0].Groups[2].Value.Substring(0, 2),
                                    Number_Of_Day = GetMidString(br_3[0].Groups[2].Value, "第", "节"),
                                    Begin_Week = GetMidString(br_3[0].Groups[2].Value, "{第", "-"),
                                    End_Week = GetMidString(br_3[0].Groups[2].Value, "-", "周}"),
                                    Teacher_Name = br_3[0].Groups[3].Value,
                                    Address = br_3[0].Groups[4].Value
                                });
                                continue;
                            }
                            MatchCollection br_5 = new Regex(@"\|([^<|]*?)<br>([^<]*?)<br>([^<]*?)<br>([^<]*?)<br>([^<]*?)<br>([^<]*?)\|").Matches(str);
                            if (br_5.Count > 0)
                            {
                                res_struct.Data.CourseSchedule.Add(new Struct_EducationSystem.Course_Info
                                {
                                    Course_Name = br_5[0].Groups[1].Value,
                                    Day_Of_Week = br_5[0].Groups[2].Value.Substring(0, 2),
                                    Number_Of_Day = GetMidString(br_5[0].Groups[2].Value, "第", "节"),
                                    Begin_Week = GetMidString(br_5[0].Groups[2].Value, "{第", "-"),
                                    End_Week = GetMidString(br_5[0].Groups[2].Value, "-", "周}"),
                                    Teacher_Name = br_5[0].Groups[3].Value,
                                    Address = br_5[0].Groups[4].Value
                                });
                                continue;
                            }
                            MatchCollection br_8 = new Regex(@"\|([^<|]*?)<br>([^<]*?)<br>([^<]*?)<br>([^<]*?)<br><br>([^<]*?)<br>([^<]*?)<br>([^<]*?)<br>([^<]*?)\|").Matches(str);
                            if (br_8.Count > 0)
                            {
                                res_struct.Data.CourseSchedule.Add(new Struct_EducationSystem.Course_Info
                                {
                                    Course_Name = br_8[0].Groups[1].Value,
                                    Day_Of_Week = br_8[0].Groups[2].Value.Substring(0, 2),
                                    Number_Of_Day = GetMidString(br_8[0].Groups[2].Value, "第", "节"),
                                    Begin_Week = GetMidString(br_8[0].Groups[2].Value, "{第", "-"),
                                    End_Week = GetMidString(br_8[0].Groups[2].Value, "-", "周}"),
                                    Teacher_Name = br_8[0].Groups[3].Value,
                                    Address = br_8[0].Groups[4].Value
                                });
                                res_struct.Data.CourseSchedule.Add(new Struct_EducationSystem.Course_Info
                                {
                                    Course_Name = br_8[0].Groups[5].Value,
                                    Day_Of_Week = br_8[0].Groups[6].Value.Substring(0, 2),
                                    Number_Of_Day = GetMidString(br_8[0].Groups[6].Value, "第", "节"),
                                    Begin_Week = GetMidString(br_8[0].Groups[6].Value, "{第", "-"),
                                    End_Week = GetMidString(br_8[0].Groups[6].Value, "-", "周}"),
                                    Teacher_Name = br_8[0].Groups[7].Value,
                                    Address = br_8[0].Groups[8].Value
                                });
                                continue;
                            }
                            MatchCollection br_12 = new Regex(@"\|([^<|]*?)<br>([^<]*?)<br>([^<]*?)<br>([^<]*?)<br>([^<]*?)<br>([^<]*?)<br><br>([^<]*?)<br>([^<]*?)<br>([^<]*?)<br>([^<]*?)<br>([^<]*?)<br>([^<]*?)\|").Matches(str);
                            if (br_12.Count > 0)
                            {
                                res_struct.Data.CourseSchedule.Add(new Struct_EducationSystem.Course_Info
                                {
                                    Course_Name = br_12[0].Groups[1].Value,
                                    Day_Of_Week = br_12[0].Groups[2].Value.Substring(0, 2),
                                    Number_Of_Day = GetMidString(br_12[0].Groups[2].Value, "第", "节"),
                                    Begin_Week = GetMidString(br_12[0].Groups[2].Value, "{第", "-"),
                                    End_Week = GetMidString(br_12[0].Groups[2].Value, "-", "周}"),
                                    Teacher_Name = br_12[0].Groups[3].Value,
                                    Address = br_12[0].Groups[4].Value
                                });
                                res_struct.Data.CourseSchedule.Add(new Struct_EducationSystem.Course_Info
                                {
                                    Course_Name = br_12[0].Groups[7].Value,
                                    Day_Of_Week = br_12[0].Groups[8].Value.Substring(0, 2),
                                    Number_Of_Day = GetMidString(br_12[0].Groups[8].Value, "第", "节"),
                                    Begin_Week = GetMidString(br_12[0].Groups[8].Value, "{第", "-"),
                                    End_Week = GetMidString(br_12[0].Groups[8].Value, "-", "周}"),
                                    Teacher_Name = br_12[0].Groups[9].Value,
                                    Address = br_12[0].Groups[10].Value
                                });
                                continue;
                            }
                            MatchCollection br_3_FONT = new Regex(@"\|([^<|]*?)<br>([^<]*?)<br>([^<]*?)<br>([^<]*?)<br><br><FONT color=red>[^<]*?</FONT>\|").Matches(str);
                            if (br_3_FONT.Count > 0)
                            {
                                res_struct.Data.CourseSchedule.Add(new Struct_EducationSystem.Course_Info
                                {
                                    Course_Name = br_3_FONT[0].Groups[1].Value,
                                    Day_Of_Week = br_3_FONT[0].Groups[2].Value.Substring(0, 2),
                                    Number_Of_Day = GetMidString(br_3_FONT[0].Groups[2].Value, "第", "节"),
                                    Begin_Week = GetMidString(br_3_FONT[0].Groups[2].Value, "{第", "-"),
                                    End_Week = GetMidString(br_3_FONT[0].Groups[2].Value, "-", "周}"),
                                    Teacher_Name = br_3_FONT[0].Groups[3].Value,
                                    Address = br_3_FONT[0].Groups[4].Value
                                });
                                continue;
                            }
                            MatchCollection br_3_FONT_2 = new Regex(@"\|([^<|]*?)<br>([^<]*?)<br>([^<]*?)<br>([^<]*?)<br><br><FONT color=red>.+?</FONT><br><br>([^<]*?)<br>([^<]*?)<br>([^<]*?)<br>([^<]*?)<br>([^<]*?)<br>([^<]*?).+?\|").Matches(str);
                            if (br_3_FONT_2.Count > 0)
                            {
                                res_struct.Data.CourseSchedule.Add(new Struct_EducationSystem.Course_Info
                                {
                                    Course_Name = br_3_FONT_2[0].Groups[1].Value,
                                    Day_Of_Week = br_3_FONT_2[0].Groups[2].Value.Substring(0, 2),
                                    Number_Of_Day = GetMidString(br_3_FONT_2[0].Groups[2].Value, "第", "节"),
                                    Begin_Week = GetMidString(br_3_FONT_2[0].Groups[2].Value, "{第", "-"),
                                    End_Week = GetMidString(br_3_FONT_2[0].Groups[2].Value, "-", "周}"),
                                    Teacher_Name = br_3_FONT_2[0].Groups[3].Value,
                                    Address = br_3_FONT_2[0].Groups[4].Value
                                });
                                res_struct.Data.CourseSchedule.Add(new Struct_EducationSystem.Course_Info
                                {
                                    Course_Name = br_3_FONT_2[0].Groups[5].Value,
                                    Day_Of_Week = br_3_FONT_2[0].Groups[6].Value.Substring(0, 2),
                                    Number_Of_Day = GetMidString(br_3_FONT_2[0].Groups[6].Value, "第", "节"),
                                    Begin_Week = GetMidString(br_3_FONT_2[0].Groups[6].Value, "{第", "-"),
                                    End_Week = GetMidString(br_3_FONT_2[0].Groups[6].Value, "-", "周}"),
                                    Teacher_Name = br_3_FONT_2[0].Groups[7].Value,
                                    Address = br_3_FONT_2[0].Groups[8].Value
                                });
                                continue;
                            }
                            MatchCollection br_5_FONT_2 = new Regex(@"\|([^<|]*?)<br>([^<]*?)<br>([^<]*?)<br>([^<]*?)<br>([^<]*?)<br>([^<]*?)<br><FONT color=red>.+?</FONT><br><br>([^<]*?)<br>([^<]*?)([^<]*?)<br>([^<]*?)<br>([^<]*?)<br>([^<]*?)<br>([^<]*?)<br><FONT color=red>.+?</FONT>\|").Matches(str);
                            if (br_5_FONT_2.Count > 0)
                            {
                                res_struct.Data.CourseSchedule.Add(new Struct_EducationSystem.Course_Info
                                {
                                    Course_Name = br_5_FONT_2[0].Groups[1].Value,
                                    Day_Of_Week = br_5_FONT_2[0].Groups[2].Value.Substring(0, 2),
                                    Number_Of_Day = GetMidString(br_5_FONT_2[0].Groups[2].Value, "第", "节"),
                                    Begin_Week = GetMidString(br_5_FONT_2[0].Groups[2].Value, "{第", "-"),
                                    End_Week = GetMidString(br_5_FONT_2[0].Groups[2].Value, "-", "周}"),
                                    Teacher_Name = br_5_FONT_2[0].Groups[3].Value,
                                    Address = br_5_FONT_2[0].Groups[4].Value
                                });
                                res_struct.Data.CourseSchedule.Add(new Struct_EducationSystem.Course_Info
                                {
                                    Course_Name = br_5_FONT_2[0].Groups[7].Value,
                                    Day_Of_Week = br_5_FONT_2[0].Groups[9].Value.Substring(0, 2),
                                    Number_Of_Day = GetMidString(br_5_FONT_2[0].Groups[9].Value, "第", "节"),
                                    Begin_Week = GetMidString(br_5_FONT_2[0].Groups[9].Value, "{第", "-"),
                                    End_Week = GetMidString(br_5_FONT_2[0].Groups[9].Value, "-", "周}"),
                                    Teacher_Name = br_5_FONT_2[0].Groups[10].Value,
                                    Address = br_5_FONT_2[0].Groups[11].Value
                                });
                                continue;
                            }
                        }
                    }
                }
                #endregion

                #region 模拟获取考试信息
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://10.10.8.68/xskscx.aspx?xh=" + username);
                    request.Referer = "http://10.10.8.68/xs_main.aspx?xh=" + username;
                    request.Accept = "Accept:text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                    request.Headers["Accept-Language"] = "zh-CN,zh;q=0.";
                    request.Headers["Accept-Charset"] = "GBK,utf-8;q=0.7,*;q=0.3";
                    request.Headers["Upgrade-Insecure-Requests"] = "1";
                    request.UserAgent = "User-Agent:Mozilla/5.0 (Windows NT 5.1) AppleWebKit/535.1 (KHTML, like Gecko) Chrome/14.0.835.202 Safari/535.1";
                    request.Headers["Cookie"] = cookie;
                    request.Host = "10.10.8.68";
                    request.KeepAlive = true;
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.Method = "GET";
                    request.AllowAutoRedirect = false;
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Stream myResponseStream = response.GetResponseStream();
                    StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("gb2312"));
                    string retString = myStreamReader.ReadToEnd();
                    myStreamReader.Close();
                    myResponseStream.Close();
                    html = retString;
                }
                #endregion

                #region 解析考试信息
                {
                    html = html.Replace("\n", "").Replace(" ", "").Replace("\r", "").Replace("&nbsp;", " ");
                    Regex regResultSessionId = new Regex(@"<td>.+?</td><td>(.+?)</td><td>.+?</td><td>(.+?)</td><td>(.+?)</td><td>.+?</td><td>(.+?)</td><td>.+?</td>");
                    MatchCollection mcResulSessionId = regResultSessionId.Matches(html);
                    if (mcResulSessionId.Count > 0)
                    {
                        bool flag = true;
                        foreach (Match mc in mcResulSessionId)
                        {
                            if (flag)
                            {
                                flag = false;
                                continue;
                            }
                            res_struct.Data.ExaminationQuery.Add(new Struct_EducationSystem.ExaminationQuery_Info
                            {
                                Course_Name = mc.Groups[1].Value,
                                Date = mc.Groups[2].Value,
                                Address = mc.Groups[3].Value,
                                Number = mc.Groups[4].Value
                            });
                        }
                    }
                }
                #endregion

                #region 模拟成绩查询 
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://10.10.8.68/xscjcx.aspx?xh=" + username);
                    request.Referer = "http://10.10.8.68/xs_main.aspx?xh=" + username;
                    request.Accept = "Accept:text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                    request.Headers["Accept-Language"] = "zh-CN,zh;q=0.";
                    request.Headers["Accept-Charset"] = "GBK,utf-8;q=0.7,*;q=0.3";
                    request.Headers["Upgrade-Insecure-Requests"] = "1";
                    request.UserAgent = "User-Agent:Mozilla/5.0 (Windows NT 5.1) AppleWebKit/535.1 (KHTML, like Gecko) Chrome/14.0.835.202 Safari/535.1";
                    request.Headers["Cookie"] = cookie;
                    request.Host = "10.10.8.68";
                    request.KeepAlive = true;
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.Method = "GET";
                    request.AllowAutoRedirect = false;
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Stream myResponseStream = response.GetResponseStream();
                    StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("gb2312"));
                    string retString = myStreamReader.ReadToEnd();
                    myStreamReader.Close();
                    myResponseStream.Close();
                    Regex regResult__VIEWSTATE = new Regex("name=\"__VIEWSTATE\" value=\"(.+?)\"");
                    MatchCollection mcResul__VIEWSTATEt = regResult__VIEWSTATE.Matches(retString);
                    Match mc__VIEWSTATEt = mcResul__VIEWSTATEt[0];
                    __VIEWSTATE = mc__VIEWSTATEt.Groups[1].Value;
                }
                #endregion

                #region 解析成绩信息
                {
                    string postDataStr = string.Format("__EVENTTARGET=&__EVENTARGUMENT=&__VIEWSTATE={0}&hidLanguage=&ddlXN=&ddlXQ=&ddl_kcxz=&btn_zcj=%C0%FA%C4%EA%B3%C9%BC%A8", __VIEWSTATE.Replace("+", "%2B").Replace("=", "%3D").Replace("/", "%2F"));
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://10.10.8.68/xscjcx.aspx?xh=" + username);
                    request.Method = "POST";
                    request.Referer = "http://10.10.8.68/xscjcx.aspx?xh=" + username;
                    request.Host = "10.10.8.68";
                    request.Headers["Origin"] = "http://10.10.8.68";
                    request.Headers["Upgrade-Insecure-Requests"] = "1";
                    request.Accept = "Accept:text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                    request.Headers["Accept-Language"] = "zh-CN,zh;q=0.8";
                    request.Headers["Accept-Charset"] = "GBK,utf-8;q=0.7,*;q=0.3";
                    request.Headers["Cookie"] = cookie;
                    request.UserAgent = "User-Agent:Mozilla/5.0 (Windows NT 5.1) AppleWebKit/535.1 (KHTML, like Gecko) Chrome/14.0.835.202 Safari/535.1";
                    request.KeepAlive = true;
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.ContentLength = postDataStr.Length;
                    Stream myRequestStream = request.GetRequestStream();
                    StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));
                    myStreamWriter.Write(postDataStr);
                    myStreamWriter.Close();
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Stream myResponseStream = response.GetResponseStream();
                    StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("gb2312"));
                    html = myStreamReader.ReadToEnd().Replace("&nbsp;", " ");
                    myStreamReader.Close();
                    myResponseStream.Close();

                    Regex regResultSessionId = new Regex(@"<td>(.+?)</td><td>(.+?)</td><td>.+?</td><td>(.+?)</td><td>(.+?)</td><td>.+?</td><td>(.+?)</td><td>(.+?)</td><td>(.+?)</td><td>.+?</td><td>(.+?)</td><td>(.+?)</td><td>(.+?)</td><td>(.{0,})</td><td>.{0,}</td>");
                    MatchCollection mcResulSessionId = regResultSessionId.Matches(html);
                    if (mcResulSessionId.Count > 0)
                    {
                        bool flag = true;
                        foreach (Match mc in mcResulSessionId)
                        {
                            if (flag)
                            {
                                flag = false;
                                continue;
                            }
                            res_struct.Data.Score.Add(new Struct_EducationSystem.Score_Info
                            {
                                School_Year = mc.Groups[1].Value.Trim(),
                                Semester = mc.Groups[2].Value.Trim(),
                                Name = mc.Groups[3].Value.Trim(),
                                Course_Type = mc.Groups[4].Value.Trim(),
                                Credit = mc.Groups[5].Value.Trim(),
                                Grade_Point = mc.Groups[6].Value.Trim(),
                                Result = mc.Groups[7].Value.Trim(),
                                BK_Result = mc.Groups[8].Value.Trim(),
                                CX_Result = mc.Groups[9].Value.Trim(),
                                Department = mc.Groups[10].Value.Trim(),
                                Remark = mc.Groups[11].Value.Trim()
                            });
                        }
                    }
                }
                #endregion

                return ReturnResult();

            }
            catch (Exception e)
            {
                return ReturnResult(e.Message);
            }

        }
        private string ReturnResult(string Status = "OK")
        {
            res_struct.Session = username;
            res_struct.Update_Date = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + " " + DateTime.Now.DayOfWeek.ToString();
            res_struct.Update_Time = string.Format("{0:00}:{1:00}", DateTime.Now.Hour, DateTime.Now.Minute);
            res_struct.Class = "EducationSystem";
            res_struct.Status = Status;
            return Struct_EducationSystem.Serialiaze(res_struct);
        }

        private string GetMidString(string src, string left, string right)
        {
            string res = string.Empty;
            int l = src.IndexOf(left) + left.Length;
            int r = src.IndexOf(right);
            if (l >= r)
                return res;
            res = src.Substring(l, r - l);
            return res;
        }

    }
}
