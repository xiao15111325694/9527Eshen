using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using SingnalRAndIldentity.Models;

namespace SingnalRAndIldentity.Controllers
{
    
    public class HomeController : Controller
    {
        [RoleAuthorize(Roles="Admin")]
        public ActionResult Index()
        {
            HttpContext http = System.Web.HttpContext.Current;
            var user = http.User;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
       
        public ActionResult Chat()
        {
            ViewBag.UserName = User.Identity.GetUserName();
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}