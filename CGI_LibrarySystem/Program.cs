﻿using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace CGI_LibrarySystem
{
    public class Program
    {
        static void Main(string[] args)
        {
            string res = string.Empty;
            {
                string data = string.Empty;
                data = Environment.GetEnvironmentVariable("QUERY_STRING");
                if (string.IsNullOrEmpty(data))
                    data = string.Empty;
                Regex reg = new Regex(@"username=(.+?)&keyword=(.+?)&page=(.+)");
                MatchCollection mcResul = reg.Matches(data);
                if (mcResul.Count > 0)
                {
                    username = mcResul[0].Groups[1].Value;
                    res = Get(username, mcResul[0].Groups[2].Value, mcResul[0].Groups[3].Value);
                }
                else
                {
                    res = ReturnResult("Error");
                }
            }


            Console.Write(
@"HTTP/1.1 200 OK
Content-type: text/html

<html>
<head>
    <title>LibrarySystem</title>
</head>
<body>" +
res + @"
</body>
</html>
");
        }

        static string url = "http://10.10.57.76:8080/opac/openlink.php?historyCount=1&strText={0}&doctype=ALL&strSearchType=title&match_flag=forward&displaypg=20&sort=CATA_DATE&orderby=desc&showmode=list&location=ALL&page={1}";
        static Struct_LibrarySystem res_struct = new Struct_LibrarySystem();
        static string username = "null";
        private static string Get(string USERNAME, string strText, string page)
        {
            username = USERNAME;
            url = string.Format(url, strText, page);
            string html = System.Net.WebUtility.HtmlDecode(GetHtmlCode(url)).Replace("\n", "").Replace("\r", "").Replace(" ", "");
            Regex regRes = new Regex("</span><ahref=\"(.+?)\">\\d+\\.(.+?)</a>(.+?)</h3><p><span><strong>.+?</strong>(.+?)<br/><strong>.+?</strong>(.+?)</span>(.+?)<br/>(.+?)</p>");
            MatchCollection mcRes = regRes.Matches(html);
            if (mcRes.Count > 0)
            {
                foreach (Match mc in mcRes)
                {
                    res_struct.Data.Book.Add(new Struct_LibrarySystem.Book_Info
                    {
                        URL = mc.Groups[1].Value,
                        Name = mc.Groups[2].Value,
                        Location = mc.Groups[3].Value,
                        Total = mc.Groups[4].Value,
                        Residue_Total = mc.Groups[5].Value,
                        Author = mc.Groups[6].Value,
                        Publisher = mc.Groups[7].Value
                    });
                }
            }

            return ReturnResult();

        }

        private static string GetHtmlCode(string url)
        {
            string htmlCode;
            HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            webRequest.Timeout = 30000;
            webRequest.Method = "GET";
            webRequest.UserAgent = "Mozilla/4.0";
            webRequest.Headers.Add("Accept-Encoding", "gzip, deflate");
            HttpWebResponse webResponse = (System.Net.HttpWebResponse)webRequest.GetResponse();
            if (webResponse.ContentEncoding.ToLower() == "gzip")//如果使用了GZip则先解压
            {
                using (System.IO.Stream streamReceive = webResponse.GetResponseStream())
                {
                    using (var zipStream =
                        new System.IO.Compression.GZipStream(streamReceive, System.IO.Compression.CompressionMode.Decompress))
                    {
                        using (StreamReader sr = new System.IO.StreamReader(zipStream, Encoding.Default))
                        {
                            htmlCode = sr.ReadToEnd();
                        }
                    }
                }
            }
            else
            {
                using (System.IO.Stream streamReceive = webResponse.GetResponseStream())
                {
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(streamReceive, Encoding.UTF8))
                    {
                        htmlCode = sr.ReadToEnd();
                    }
                }
            }

            return htmlCode;
        }

        private static string ReturnResult(string Status = "OK")
        {
            res_struct.Session = username;
            res_struct.Update_Date = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + " " + DateTime.Now.DayOfWeek.ToString();
            res_struct.Update_Time = string.Format("{0:00}:{1:00}", DateTime.Now.Hour, DateTime.Now.Minute);
            res_struct.Class = "LibrarySystem";
            res_struct.Status = Status;
            return Struct_LibrarySystem.Serialiaze(res_struct);
        }

    }
}
