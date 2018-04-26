using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OAuth2.Models;
using OAuth2.Ouath2.Models;

namespace OAuth2.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login(string returnUrl)
        {

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.LoginViewModel model,string ReturnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            DBModel db= new DBModel();
            var user = db.User.Where(x => x.Email == model.Email).ToList().FirstOrDefault();
            if (user != null)
            {
                if (user.Password == model.Password)
                {
                    return RedirectToLocal(ReturnUrl);
                }
            }
            return View(model);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterBindingModel model )
        {
            DBModel db = new DBModel();
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            db.User.Add(new User {Email = model.Email, Password = model.Password});
            db.SaveChanges();
            return View(model);
        }
    }
}