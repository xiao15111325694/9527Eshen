using mshtml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace 网页模拟点击.Controllers
{
    public class PubilcController : ApiController
    {
        [AllowAnonymous]
        public string AnalogClicks([FromUri] PublicFindModel findModel)
        {
            SHDocVw.InternetExplorer ie = new SHDocVw.InternetExplorer();

            ie.Navigate(findModel.Url);
            ie.Visible = true;

            IHTMLDocument3 doc = (IHTMLDocument3)ie.Document;
            doc.getElementById(findModel.InputText1).innerText = "hello world";
            doc.getElementById(findModel.SavaId).click();
            return "yes";
        }
    }
}
