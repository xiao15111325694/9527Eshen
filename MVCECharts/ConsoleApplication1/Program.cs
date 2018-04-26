using mshtml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        private static bool ie_Read = false;
        static void Main(string[] args)
        {
            SHDocVw.InternetExplorer ie = new SHDocVw.InternetExplorer();
            ie.DocumentComplete += ie_DocumentComplete;
      
            ie.Navigate("https://www.baidu.com/");
            ie.Visible = true;
            //while (ie_Read)
            //{
            //    Thread.Sleep(1000);
            //    if (ie.ReadyState == SHDocVw.tagREADYSTATE.READYSTATE_COMPLETE)
            //        break;
            //}
            IHTMLDocument3 doc = (IHTMLDocument3)ie.Document;
            doc.getElementById("kw").innerText = "hello world";
            doc.getElementById("su").click();
            Console.Read();
        }

        private static void ie_DocumentComplete(object pDisp, ref object URL)
        {
            ie_Read = true;
        }
    }
}
