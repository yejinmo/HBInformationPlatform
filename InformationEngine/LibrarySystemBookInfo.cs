using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using InformationEngine.Struct;

namespace InformationEngine
{
    public class LibrarySystemBookInfo
    {
        Struct_LibrarySystemBookInfo res_struct = new Struct_LibrarySystemBookInfo();
        
        string Host_URL = "http://10.10.57.76:8080/opac/";
        string username = "null";

        public string Get(string USERNAME, string URL)
        {
            username = USERNAME;
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(URL))
                return ReturnResult("Error");
            Regex regRes = new Regex(@"item.php\?marc_no=[\d]+");
            if (regRes.IsMatch(URL))
            {
                URL = Host_URL + URL;
                string html = WebUtility.HtmlDecode(GetHtmlCode(URL)).Replace("\n", "").Replace("\r", "").Replace("	", "");
                Regex reg1 = new Regex("<dt class=\"grey\">借阅次数：(.+?)</dt>");
                Regex reg2 = new Regex("<dt class=\"grey\">浏览次数：(.+?)</dt>");
                Regex reg3 = new Regex("<dt>题名/责任者:</dt><dd><a href=\".+?\">(.+?)</a>/(.+?)</dd></dl><dl class=\"booklist\"><dt>出版发行项:</dt>");
                Regex reg4 = new Regex("<dt>ISBN及定价:</dt><dd>(.+?)/(.+?)</dd>");
                Regex reg5 = new Regex("<dt>载体形态项:</dt><dd>([\\d]+)");
                Regex reg6 = new Regex("<dt>提要文摘附注:</dt><dd>(.+?)</dd>");
                Regex reg7 = new Regex("<td align=\"center\" class=\"whitetext\" width=\"20%\" bgcolor=\"#FFFFFF\"  >(.+?)</td>            <td align=\"center\" class=\"whitetext\" width=\"15%\" bgcolor=\"#FFFFFF\" >(.+?)</td>            <td align=\"center\" class=\"whitetext\" width=\"15%\" bgcolor=\"#FFFFFF\"  >.+?</td>            <td align=\"center\" class=\"whitetext\" width=\"25%\" bgcolor=\"#FFFFFF\"  >(.+?)</td>            <td align=\"center\" class=\"whitetext\" width=\"25%\" bgcolor=\"#FFFFFF\"  >(.+?)</td>");
                Regex reg8 = new Regex("<dt>出版发行项:</dt><dd>(.+?)</dd>");
                res_struct.Data.Borrow_Count = reg1.IsMatch(html) ? reg1.Matches(html)[0].Groups[1].Value.Trim() : "";
                res_struct.Data.Browse_Count = reg2.IsMatch(html) ? reg2.Matches(html)[0].Groups[1].Value.Trim() : "";
                if (reg3.IsMatch(html))
                {
                    MatchCollection mc = reg3.Matches(html);
                    res_struct.Data.Name = mc[0].Groups[1].Value.Trim();
                    res_struct.Data.Author = mc[0].Groups[2].Value.Trim();
                }
                else
                {
                    res_struct.Data.Name = "";
                    res_struct.Data.Author = "";
                }
                if (reg4.IsMatch(html))
                {
                    MatchCollection mc = reg4.Matches(html);
                    res_struct.Data.ISBN = mc[0].Groups[1].Value.Trim();
                    res_struct.Data.Price = mc[0].Groups[2].Value.Trim();
                }
                else
                {
                    res_struct.Data.ISBN = "";
                    res_struct.Data.Price = "";
                }
                res_struct.Data.Pages = reg5.IsMatch(html) ? reg5.Matches(html)[0].Groups[1].Value.Trim() : "";
                res_struct.Data.Remark = reg6.IsMatch(html) ? reg6.Matches(html)[0].Groups[1].Value.Trim() : "";
                if (reg7.IsMatch(html))
                {
                    MatchCollection mcRes = reg7.Matches(html);
                    foreach (Match mc in mcRes)
                    {
                        res_struct.Data.BorrowList.Add(new Struct_LibrarySystemBookInfo.BorrowList_Info
                        {
                            Location = mc.Groups[1].Value.Trim(),
                            BarCode = mc.Groups[2].Value.Trim(),
                            State = mc.Groups[3].Value.Trim(),
                            Building = mc.Groups[4].Value.Trim()
                        });
                    } 
                }
                res_struct.Data.Publisher = reg8.IsMatch(html) ? reg8.Matches(html)[0].Groups[1].Value.Trim() : "";
            }
            else
            {

            }
            return ReturnResult();
        }

        private string GetHtmlCode(string url)
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
        private string ReturnResult(string Status = "OK")
        {
            res_struct.Session = username;
            res_struct.Update_Date = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + " " + DateTime.Now.DayOfWeek.ToString();
            res_struct.Update_Time = string.Format("{0:00}:{1:00}", DateTime.Now.Hour, DateTime.Now.Minute);
            res_struct.Class = "LibrarySystemBookInfo";
            res_struct.Status = Status;
            return Struct_LibrarySystemBookInfo.Serialiaze(res_struct);
        }

    }
}
