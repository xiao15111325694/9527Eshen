using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace MVCEchartsManager.Controllers
{
    public class OtherController : Controller
    {
        // GET: uploadify
        public ActionResult uploadify()
        {
            return View();
        }

        public ActionResult UploadifyDome(HttpPostedFileBase filedata)
        {
            if (filedata == null || string.IsNullOrEmpty(filedata.FileName)||filedata.ContentLength==0)
            {
                return this.HttpNotFound();
            }

            string filename = Path.GetFileName(filedata.FileName);
            string virtualPath = string.Format("~/UploadFile/{0}", filename);
            string path = this.Server.MapPath(virtualPath);
            filedata.SaveAs(path);
            return this.Json(new object { });
        }
    }
}