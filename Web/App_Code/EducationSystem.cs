using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

/// <summary>
/// GetEducationSystem 的摘要说明
/// </summary>
public static class EducationSystem
{

    static Struct_EducationSystem res_struct = new Struct_EducationSystem();
    static string username;
    static string password;
    static string FinalResultHtml;
    static string HtmlTemplateV3 = @"<html>    <head>        <meta charset=""GB2312"">        <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">        <meta name=""viewport"" content=""width=device-width, initial-scale=1"">        <link href=""css/base.css"" rel=""stylesheet"">        <link href=""css/project.css"" rel=""stylesheet"">        <title>教务信息系统</title>    <base target=""_blank"" /></head>    <body class=""page-brand"">        <header class=""header header-transparent header-waterfall ui-header affix-top"">            <ul class=""nav nav-list pull-left"">                <li>					<a data-toggle=""menu"" href=""#ui_menu"">						<span class=""icon icon-lg"">menu</span>					</a>				</li>            </ul>            <nav class=""tab-nav pull-right hidden-xx"">                <ul class=""nav nav-justified"">                    <li>						<a class=""waves-attach waves-effect"" data-toggle=""tab"" href=""#selector1"">  课表  </a>					</li>                    <li class=""active"">						<a class=""waves-attach waves-effect"" data-toggle=""tab"" href=""#selector2"">  成绩  </a>					</li>                    <li>						<a class=""waves-attach waves-effect"" data-toggle=""tab"" href=""#selector3"">  考试  </a>					</li>                </ul>                <div class=""tab-nav-indicator"">				</div>            </nav>        </header>        <nav aria-hidden=""true"" class=""menu"" id=""ui_menu"" tabindex=""-1"" style=""display: none;"">            <div class=""menu-scroll"">                <div class=""menu-content"">                    <a class=""menu-logo"" href=""index.html"">首页</a>					                    <ul class=""nav"">                        <li>                            <a class=""collapsed waves-attach waves-effect"" data-toggle=""collapse"" href=""#ui_menu_samples"">教务信息系统</a>							                            <ul class=""menu-collapse collapse"" id=""ui_menu_samples"">                                <li>									<a class=""waves-attach waves-effect"" data-toggle=""tab"" href=""#selector1"">  课表  </a>								</li>                                <li class=""active"">									<a class=""waves-attach waves-effect"" data-toggle=""tab"" href=""#selector2"">  成绩  </a>								</li>                                <li>									<a class=""waves-attach waves-effect"" data-toggle=""tab"" href=""#selector3"">  考试  </a>								</li>                            </ul>                        </li>                    </ul>                </div>            </div>        </nav>        <main class=""content"">            <div class=""content-header ui-content-header"">                <div class=""container"">                    <div class=""row"">                        <div class=""col-lg-6 col-lg-offset-3 col-md-8 col-md-offset-2"">                            <h1 class=""content-heading"">教务信息系统</h1>                        </div>                    </div>                </div>            </div>            <div class=""container"">                <div class=""row"">                    <section class=""content-inner margin-top-no"">                        <div class=""card-inner"">                            <div class=""tab-content"">                                <div class=""tab-pane fade"" id=""selector1"">                                    <center>                                    <br>									                                    <h1 class=""content-sub-heading"">                                        <!-->替换.课表.标题<-->									                                    </h1>                                    <center>                                    <!-->替换.课表.内容<-->														                                </div>                                <div class=""tab-pane fade  active in"" id=""selector2"">					<center>                                    <br>									                                    <h1 class=""content-sub-heading"">                                        <!-->替换.成绩.标题<-->									                                    </h1>                                    <center>                                    <!-->替换.成绩.内容<-->												                                </div>                                <div class=""tab-pane fade"" id=""selector3"">					<center>                                    <br>									                                    <h1 class=""content-sub-heading"">                                        <!-->替换.考试.标题<-->									                                    </h1>                                    <center>                                    <!-->替换.考试.内容<-->												                                </div>                            </div>                        </div><center><a href=""http://www.yinjiayi.net/cloud"">@佳逸云计算&nbsp;</a><a href=""https://github.com/yejinmo/HBInformationPlatform/tree/master/InformationEngine/CheckCodeEngine"">@X2验证码识别引擎&nbsp;</a><a href=""https://github.com/yejinmo/HBInformationPlatform"">@GitHub开源项目&nbsp;</a><a href=""https://www.yejinmo.com"">@今墨交互</a><center>                    </section>                </div>            </div>        </main>        <script src=""js/jquery.min.js""></script>		<script src=""js/base.min.js""></script>		<script src=""js/project.min.js""></script>	    </body></html>";

    public static string GetEducationSystem(string username,string password)
    {
        string res = string.Empty;
        res = Get(username, password);
        return (
@"HTTP/1.1 200 OK
Content-type: text/html

" + res);
    }

    private static string Get(string USERNAME, string PASSWORD)
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
            string StudentTitle = string.Empty;
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

            #region 预处理
            {
                html = html.Replace("\r", "").Replace("\n", "");
                string final = string.Empty;
                {
                    Regex reg = new Regex("<span id=\"Label7\">学院：(.*?)</span>");
                    MatchCollection mc = reg.Matches(html);
                    if (mc.Count == 1)
                        final += " - " + mc[0].Groups[1].ToString() + " - ";
                }
                {
                    Regex reg = new Regex("<span id=\"Label8\">专业：(.*?)</span>");
                    MatchCollection mc = reg.Matches(html);
                    if (mc.Count == 1)
                        final += mc[0].Groups[1].ToString() + " - ";
                }
                {
                    Regex reg = new Regex("<span id=\"Label9\">行政班：(.*?)</span>");
                    MatchCollection mc = reg.Matches(html);
                    if (mc.Count == 1)
                        final += mc[0].Groups[1].ToString() + " - ";
                }
                {
                    Regex reg = new Regex("<span id=\"Label6\">姓名：(.*?)</span>");
                    MatchCollection mc = reg.Matches(html);
                    if (mc.Count == 1)
                        final += mc[0].Groups[1].ToString() + " - ";
                }
                {
                    Regex reg = new Regex("<span id=\"Label5\">学号：(.*?)</span>");
                    MatchCollection mc = reg.Matches(html);
                    if (mc.Count == 1)
                        final += mc[0].Groups[1].ToString();
                }
                StudentTitle = final;
                {
                    Regex reg = new Regex("<option selected=\"selected\" value=.*?>(.*?)</option>");
                    MatchCollection mc = reg.Matches(html);
                    if (mc.Count == 2)
                        final = mc[0].Groups[1].ToString() + " 学年度第 " + mc[1].Groups[1].ToString() + @" 学期学生个人课程表" + final;
                }
                FinalResultHtml = HtmlTemplateV3.Replace("<!-->替换.课表.标题<-->", final);
                final = string.Empty;
                {
                    Regex reg = new Regex("<table id=\"Table1\".*?</table>");
                    MatchCollection mc = reg.Matches(html);
                    if (mc.Count == 1)
                        final += mc[0].Groups[0].ToString().Replace("bordercolor=\"Black\" border=\"0\"",
                            "bordercolor=\"Black\" border=\"1\"");
                    //.Replace("<td width=\"1%\">第", "<td width=\"5%\">第")
                }
                {
                    Regex reg = new Regex("<tr>.*?星期日</td>	</tr>");
                    MatchCollection mc = reg.Matches(html);
                    if (mc.Count == 1)
                    {
                        string str = mc[0].Groups[0].ToString();
                        string str2 = "<thead>" + str.Replace("td", "th") + "</thead>";
                        final = final.Replace(str, str2);
                    }
                }
                final = final.Replace("class=\"blacktab\"", "class=\"table\"");
                final = final.Replace("align=\"Center\"", "").Replace("border=\"1\"", "").Replace("bordercolor=\"Black\"", "");
                FinalResultHtml = FinalResultHtml.Replace("<!-->替换.课表.内容<-->", final);
                //return FinalResultHtml;
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

                {
                    html = html.Replace("\n", "").Replace("\r", "");
                    string final = string.Empty;
                    Regex reg = new Regex("<table class=\"datelist\".*?</table>");
                    MatchCollection mc = reg.Matches(html);
                    if (mc.Count == 1)
                        final = mc[0].Groups[0].ToString().Replace("bordercolor=\"Black\" border=\"0\"",
                            "bordercolor=\"Black\" border=\"1\"").Replace("datelist", "table");
                    final = final.Replace("href=\"javascript:__doPostBack('Datagrid1$_ctl1$_ctl0','')\"", "")
                                 .Replace("href=\"javascript:__doPostBack('Datagrid1$_ctl1$_ctl1','')\"", "")
                                 .Replace("href=\"javascript:__doPostBack('Datagrid1$_ctl1$_ctl2','')\"", "")
                                 .Replace("href=\"javascript:__doPostBack('Datagrid1$_ctl1$_ctl3','')\"", "");
                    {
                        Regex _reg = new Regex("<tr class=\"tablehead\">.*?</td>	</tr>");
                        MatchCollection _mc = _reg.Matches(final);
                        if (_mc.Count == 1)
                        {

                            string str = _mc[0].Groups[0].ToString();
                            string str2 = "<thead>" + str.Replace("td", "th") + "</thead>";
                            final = final.Replace(str, str2).Replace("<a >", "").Replace("</a>", "");
                        }
                    }
                    FinalResultHtml = FinalResultHtml.Replace("<!-->替换.成绩.标题<-->", "在校学习成绩" + StudentTitle)
                        .Replace("<!-->替换.成绩.内容<-->", final);

                    return FinalResultHtml;
                }

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

    private static string ReturnResult(string Status = "OK")
    {
        res_struct.Session = username;
        res_struct.Update_Date = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + " " + DateTime.Now.DayOfWeek.ToString();
        res_struct.Update_Time = string.Format("{0:00}:{1:00}", DateTime.Now.Hour, DateTime.Now.Minute);
        res_struct.Class = "EducationSystem";
        res_struct.Status = Status;
        return Struct_EducationSystem.Serialiaze(res_struct);
    }

    private static string GetMidString(string src, string left, string right)
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