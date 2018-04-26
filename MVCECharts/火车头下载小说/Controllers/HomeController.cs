using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using HtmlAgilityPack;
using 火车头下载小说.Help;

namespace 火车头下载小说.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
            return View();
        }

        [HttpPost]
        public void Index(string url)
        {

            HtmlWeb htmlWeb = new HtmlWeb();
            HtmlDocument document = htmlWeb.Load(url);
            HtmlNodeCollection nodeCollection = document.DocumentNode.SelectNodes(@"//table/tr/td/a[@href]");  //代表获取所有
            HtmlNodeCollection nodeImgUrl = document.DocumentNode.SelectNodes(@"img");
            string name = document.DocumentNode.SelectNodes(@"//meta[@name='keywords']")[0].GetAttributeValue("content", "").Split(',')[0];
            foreach (var node in nodeCollection)
            {
                HtmlAttribute attribute = node.Attributes["href"];
                String val = attribute.Value;  //章节url
                var title = htmlWeb.Load(val).DocumentNode.SelectNodes(@"//h1")[0].InnerText;  //文章标题
                //var doc = htmlWeb.Load(val).DocumentNode.SelectNodes(@"//div[@id='readerFs']");
                var doc = htmlWeb.Load(val).DocumentNode.SelectNodes(@"//div[@id='readerFs']");
                var content = doc[0].InnerHtml.Replace("&nbsp;", "").Replace("<br>", "\r\n");  //文章内容
                
                var reportDirectory = string.Format("~/{0}", name);
                reportDirectory = System.Web.Hosting.HostingEnvironment.MapPath(reportDirectory);
                if (!Directory.Exists(reportDirectory))
                {
                    Directory.CreateDirectory(reportDirectory);
                }
                var dailyReportFullPath = string.Format("{0}text_.log", reportDirectory);
                var logContent = string.Format("{0}-{1}-{2}", DateTime.Now, "滴 滴滴", Environment.NewLine);

               System.IO.File.AppendAllText(dailyReportFullPath, logContent);

                //var dailyReportFullPath = string.Format("{0}.log", name);
                //var logContent = string.Format("\r\n{0}\r\n{1}", title, content);
                //AddText he = new AddText();
                ////he.AddTextContext(dailyReportFullPath, logContent);
                //System.IO.File.AppendAllText(dailyReportFullPath, logContent);

                //FileStream fs = new FileStream("00.txt", FileMode.Append, FileAccess.Write);
                //StreamWriter sr = new StreamWriter(fs, Encoding.UTF8);
                //sr.WriteLine("\r\n" + title + "\r\n" + content);// 开始写入
                //sr.Close();
                //fs.Close();

                ////txt文本输出
                //string path = AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "/") + "Txt/";
                //System.IO.File.AppendAllText(path, content);
                //ViewBag.data = content;
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult GetHtmlContent(string url)
        {
            WebRequest request= WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream, Encoding.Default);
            ViewBag.data = reader.ReadToEnd();
            return View("Index");
        }
    }
}