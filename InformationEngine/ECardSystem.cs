using InformationEngine.Struct;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace InformationEngine
{
    public class ECardSystem
    {
        string username;
        string password;
        string Host_URL = "10.10.8.9";
        Struct_ECardSystem res_struct = new Struct_ECardSystem();

        public string Get(string USERNAME, string PASSWORD)
        {
            username = USERNAME;
            password = PASSWORD;
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return ReturnResult("Error");
            string cookie = string.Empty;
            string __VIEWSTATE = string.Empty;
            string __EVENTVALIDATION = string.Empty;
            string html = string.Empty;
            string check_code = string.Empty;
            int check_code_err = 0;

            #region 首页获取 cookie
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://" + Host_URL + "/selfsearch/Index_main.aspx");
                request.CookieContainer = new CookieContainer();
                request.Referer = "http://" + Host_URL + "/selfsearch/";
                request.Accept = "Accept:text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                request.Headers["Accept-Language"] = "zh-CN,zh;q=0.8";
                request.Headers["Accept-Charset"] = "GBK,utf-8;q=0.7,*;q=0.3";
                request.UserAgent = "User-Agent:Mozilla/5.0 (Windows NT 5.1) AppleWebKit/535.1 (KHTML, like Gecko) Chrome/14.0.835.202 Safari/535.1";
                request.KeepAlive = true;
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = "GET";
                request.Host = Host_URL;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                string retString = myStreamReader.ReadToEnd();
                cookie = "ASP.NET_SessionId=" + response.Headers["Set-Cookie"];
                Regex regResultSessionId = new Regex("ASP.NET_SessionId=(.+?); path=/");
                MatchCollection mcResulSessionId = regResultSessionId.Matches(cookie);
                Match mc = mcResulSessionId[0];
                cookie = mc.Groups[1].Value;
                myStreamReader.Close();
                myResponseStream.Close();
                if (string.IsNullOrEmpty(cookie))
                    return ReturnResult("Error");
            }
            #endregion

            #region 登录页面获取 __VIEWSTATE 及 __EVENTVALIDATION
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://" + Host_URL + "/selfsearch/UserInfo/UserLogin.aspx");
                request.Referer = "http://" + Host_URL + "/selfsearch/Index_button.aspx";
                request.Headers["Cookie"] = cookie;
                request.Accept = "Accept:text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                request.Headers["Accept-Language"] = "zh-CN,zh;q=0.8";
                request.Headers["Accept-Charset"] = "GBK,utf-8;q=0.7,*;q=0.3";
                request.UserAgent = "User-Agent:Mozilla/5.0 (Windows NT 5.1) AppleWebKit/535.1 (KHTML, like Gecko) Chrome/14.0.835.202 Safari/535.1";
                request.KeepAlive = true;
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = "GET";
                request.Host = Host_URL;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                string retString = myStreamReader.ReadToEnd();

                Regex regResult__VIEWSTATE = new Regex("id=\"__VIEWSTATE\" value=\"(.+?)\"");
                MatchCollection mcResul__VIEWSTATEt = regResult__VIEWSTATE.Matches(retString);
                Match mc__VIEWSTATEt = mcResul__VIEWSTATEt[0];
                __VIEWSTATE = mc__VIEWSTATEt.Groups[1].Value;

                Regex regResult__EVENTVALIDATION = new Regex("id=\"__EVENTVALIDATION\" value=\"(.+?)\"");
                MatchCollection mcResul__EVENTVALIDATION = regResult__EVENTVALIDATION.Matches(retString);
                Match mc__EVENTVALIDATION = mcResul__EVENTVALIDATION[0];
                __EVENTVALIDATION = mc__EVENTVALIDATION.Groups[1].Value;

                myStreamReader.Close();
                myResponseStream.Close();
                if (string.IsNullOrEmpty(__EVENTVALIDATION) || string.IsNullOrEmpty(__VIEWSTATE))
                    return ReturnResult("Error");
            }
            #endregion

            do
            {
                check_code_err++;

                #region 获取并识别验证码
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://" + Host_URL + "/selfsearch/Other/pic.aspx");
                    request.Referer = "http://" + Host_URL + "/selfsearch/UserInfo/UserLogin.aspx";
                    request.Headers["Cookie"] = cookie;
                    request.Accept = "Accept:image/webp,image/*,*/*;q=0.8";
                    request.Headers["Accept-Language"] = "zh-CN,zh;q=0.8";
                    request.Headers["Accept-Charset"] = "GBK,utf-8;q=0.7,*;q=0.3";
                    request.UserAgent = "User-Agent:Mozilla/5.0 (Windows NT 5.1) AppleWebKit/535.1 (KHTML, like Gecko) Chrome/14.0.835.202 Safari/535.1";
                    request.KeepAlive = true;
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.Method = "GET";
                    request.Host = Host_URL;
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Stream myResponseStream = response.GetResponseStream();
                    check_code = new CheckCodeEngine.X2Engine().Get(new Bitmap(myResponseStream));
                }
                #endregion

                #region 模拟登录操作
                {
                    string postDataStr = string.Format("__EVENTTARGET=&__EVENTARGUMENT=&__VIEWSTATE={0}&__EVENTVALIDATION={1}&txtUserName={2}&txtPwd={3}&txtCheckCode={4}&btnUserLogin=",
                        __VIEWSTATE.Replace("+", "%2B").Replace("=", "%3D").Replace("/", "%2F"), __EVENTVALIDATION.Replace("+", "%2B").Replace("=", "%3D").Replace("/", "%2F"), username, password, check_code);
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://" + Host_URL + "/selfsearch/UserInfo/UserLogin.aspx");
                    request.Method = "POST";
                    request.Referer = "http://" + Host_URL + "/selfsearch/UserInfo/UserLogin.aspx";
                    request.Host = Host_URL;
                    request.Headers["Origin"] = "http://" + Host_URL;
                    request.Headers["Upgrade-Insecure-Requests"] = "1";
                    request.Accept = "Accept:text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                    request.Headers["Accept-Language"] = "zh-CN,zh;q=0.88";
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
                    if (retString.IndexOf("用户密码错误") > 0)
                        return ReturnResult("password error.");
                    else if (retString.IndexOf("用户不存在") > 0)
                        return ReturnResult("username error.");
                    else if (retString.IndexOf("验证码错误") > 0)
                        continue;
                    break;
                }
                #endregion


            } while (check_code_err <= 5);

            #region 消费信息查询界面获取 __VIEWSTATE 及 __EVENTVALIDATION
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://" + Host_URL + "/selfsearch/EcardInfo/CONSUMEINFO_SEACH.ASPX");
                request.Referer = "http://" + Host_URL + "/selfsearch/EcardInfo/USERSEACHINFO.ASPX";
                request.Headers["Cookie"] = cookie;
                request.Accept = "Accept:text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                request.Headers["Accept-Language"] = "zh-CN,zh;q=0.8";
                request.Headers["Accept-Charset"] = "GBK,utf-8;q=0.7,*;q=0.3";
                request.UserAgent = "User-Agent:Mozilla/5.0 (Windows NT 5.1) AppleWebKit/535.1 (KHTML, like Gecko) Chrome/14.0.835.202 Safari/535.1";
                request.KeepAlive = true;
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = "GET";
                request.Host = Host_URL;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                string retString = myStreamReader.ReadToEnd();

                Regex regResult__VIEWSTATE = new Regex("id=\"__VIEWSTATE\" value=\"(.+?)\"");
                MatchCollection mcResul__VIEWSTATEt = regResult__VIEWSTATE.Matches(retString);
                Match mc__VIEWSTATEt = mcResul__VIEWSTATEt[0];
                __VIEWSTATE = mc__VIEWSTATEt.Groups[1].Value;

                Regex regResult__EVENTVALIDATION = new Regex("id=\"__EVENTVALIDATION\" value=\"(.+?)\"");
                MatchCollection mcResul__EVENTVALIDATION = regResult__EVENTVALIDATION.Matches(retString);
                Match mc__EVENTVALIDATION = mcResul__EVENTVALIDATION[0];
                __EVENTVALIDATION = mc__EVENTVALIDATION.Groups[1].Value;

                myStreamReader.Close();
                myResponseStream.Close();
            }
            #endregion

            #region 模拟查询操作
            {
                string postDataStr = string.Format("__EVENTTARGET=&__EVENTARGUMENT=&__VIEWSTATE={0}&__EVENTVALIDATION={1}&ctl00%24ContentPlaceHolder1%24ConsumeAscx1%24aa=Main&ctl00%24ContentPlaceHolder1%24ConsumeAscx1%24ddlAccType=&ctl00%24ContentPlaceHolder1%24ConsumeAscx1%24sDateTime={2}&ctl00%24ContentPlaceHolder1%24ConsumeAscx1%24eDateTime={3}&ctl00%24ContentPlaceHolder1%24ConsumeAscx1%24btnExcel=%E5%AF%BC++%E5%87%BA",
                    __VIEWSTATE.Replace("+", "%2B").Replace("=", "%3D").Replace("/", "%2F"), __EVENTVALIDATION.Replace("+", "%2B").Replace("=", "%3D").Replace("/", "%2F"), DateTime.Now.AddMonths(-1).ToString("yyyyMMdd"), DateTime.Now.ToString("yyyyMMdd"));
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://" + Host_URL + "/selfsearch/EcardInfo/CONSUMEINFO_SEACH.ASPX");
                request.Method = "POST";
                request.Referer = "http://" + Host_URL + "/selfsearch/EcardInfo/CONSUMEINFO_SEACH.ASPX";
                request.Host = Host_URL;
                request.Headers["Origin"] = "http://" + Host_URL;
                request.Headers["Upgrade-Insecure-Requests"] = "1";
                request.Accept = "Accept:text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                request.Headers["Accept-Language"] = "zh-CN,zh;q=0.88";
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

                Regex regResult = new Regex(@"<td>.+?</td><td>.+?</td><td>(.+?)</td><td>(.+?)</td><td>(.+?)</td><td>.+?</td><td>.+?</td><td>(.+?)</td><td>(.+?)</td>");
                MatchCollection mcResult = regResult.Matches(retString);
                if (mcResult.Count > 0)
                {
                    foreach (Match mc in mcResult)
                    {
                        res_struct.Data.Records.Add(new Struct_ECardSystem.Records_Info
                        {
                            Date = mc.Groups[1].Value,
                            Balance = mc.Groups[2].Value,
                            Transaction_Amount = mc.Groups[3].Value,
                            Terminal = mc.Groups[4].Value,
                            Station = mc.Groups[5].Value
                        });
                    }
                }
            }
            #endregion

            return ReturnResult();
        }

        private string ReturnResult(string Status = "OK")
        {
            res_struct.Session = username;
            res_struct.Update_Date = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + " " + DateTime.Now.DayOfWeek.ToString();
            res_struct.Update_Time = string.Format("{0:00}:{1:00}", DateTime.Now.Hour, DateTime.Now.Minute);
            res_struct.Class = "ECardSystem";
            res_struct.Status = Status;
            return Struct_ECardSystem.Serialiaze(res_struct);
        }

        /// <summary>
        /// HTTP POST方式请求数据(带图片)
        /// </summary>
        /// <param name="url">URL</param>        
        /// <param name="param">POST的数据</param>
        /// <param name="fileByte">图片Byte</param>
        /// <returns></returns>
        private string GetCheckCode(string url, IDictionary<object, object> param, byte[] fileByte)
        {
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
            wr.ContentType = "multipart/form-data; boundary=" + boundary;
            wr.UserAgent = "RK_C# 1.2";
            wr.Method = "POST";

            //wr.Timeout = 150000;
            //wr.KeepAlive = true;

            //wr.Credentials = System.Net.CredentialCache.DefaultCredentials;
            Stream rs = null;
            try
            {
                rs = wr.GetRequestStream();
            }
            catch { return "无法连接.请检查网络."; }
            string responseStr = null;

            string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
            foreach (string key in param.Keys)
            {
                rs.Write(boundarybytes, 0, boundarybytes.Length);
                string formitem = string.Format(formdataTemplate, key, param[key]);
                byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                rs.Write(formitembytes, 0, formitembytes.Length);
            }
            rs.Write(boundarybytes, 0, boundarybytes.Length);

            string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
            string header = string.Format(headerTemplate, "image", "i.gif", "image/gif");//image/jpeg
            byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
            rs.Write(headerbytes, 0, headerbytes.Length);

            rs.Write(fileByte, 0, fileByte.Length);

            byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--");
            rs.Write(trailer, 0, trailer.Length);
            rs.Close();

            WebResponse wresp = null;
            try
            {
                wresp = wr.GetResponse();

                Stream stream2 = wresp.GetResponseStream();
                StreamReader reader2 = new StreamReader(stream2);
                responseStr = reader2.ReadToEnd();

            }
            catch
            {
                //throw;
            }
            finally
            {
                if (wresp != null)
                {
                    wresp.Close();
                    wresp = null;
                }
                wr.Abort();
                wr = null;

            }
            return responseStr;
        }


    }
}
