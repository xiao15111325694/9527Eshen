using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace 自定义权限控制.Error
{
    public class LogErrorAttribute: HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            string controllerName = filterContext.RouteData.Values["controller"].ToString();
            string actionName = filterContext.RouteData.Values["action"].ToString();
            var reportDirectory = string.Format("~/Log/");
            reportDirectory = System.Web.Hosting.HostingEnvironment.MapPath(reportDirectory);
            if (!Directory.Exists(reportDirectory))
            {
                Directory.CreateDirectory(reportDirectory);
            }
            FileStream fs = new FileStream(reportDirectory + "/test.log", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.BaseStream.Seek(0, SeekOrigin.End);
            string writeText = string.Format("引发异常时间:[{0}]controllerName:[{1}]actionName:[{2}]{3}",DateTime.Now.ToString(),
            controllerName, actionName, filterContext.Exception.ToString());
            sw.WriteLine(writeText);
            sw.Flush();
            sw.Close();
            fs.Close();
            filterContext.Result = new ContentResult() { Content = "系统错误" };
        }
    }
}