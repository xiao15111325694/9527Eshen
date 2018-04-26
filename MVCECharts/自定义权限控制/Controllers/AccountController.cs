using mshtml;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using 自定义权限控制.Manager;
using 自定义权限控制.Models;
using 自定义权限控制.Models.ViewModel;
using 自定义权限控制.Until.VerifyCode;

namespace SingnalRAndIldentity.Controllers
{
    public class AccountController : Controller
    {
        private LoginManager loginManager = new LoginManager();
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            string returnurl = HttpContext.Request.QueryString["RetrunUrl"];
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpGet]
        public ActionResult GetAuthCode()
        {
            return File(new VerifyCode().GetVerifyCode(), @"image/Gif");
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            LoginManager loginManager = new LoginManager();
            var result = loginManager.SignIn(model.Email, model.Password);
            if (result == LoginState.Success)
            {
                var role = loginManager.GetUserRoleByEmailAsync(model.Email);

                //通过用户名(邮箱) 密码生成一个ticket令牌
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(0, model.Email, DateTime.Now,
                    DateTime.Now.AddMinutes(30), true, string.Format("{0}&{1}", model.Email, model.Password));
                
                //加密
                var Token = FormsAuthentication.Encrypt(ticket);

                System.Web.HttpContext.Current.Session["UserName"]=model.Email;
                System.Web.HttpContext.Current.Session["Role"] = role;

                //将Token加入到cookie  并设置为5天过期
                var cookie = new HttpCookie("Token", Token)
                {
                    Expires = DateTime.Now.AddHours(2.5)
                };
                Response.Cookies.Add(cookie);

                if (returnUrl != null)
                {
                    return Redirect(returnUrl);
                }
                return Redirect("/Home/Chat");
            }

            return View();

            // 这不会计入到为执行帐户锁定而统计的登录失败次数中
            // 若要在多次输入错误密码的情况下触发帐户锁定，请更改为 shouldLockout: true
            //var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            //switch (result)
            //{
            //    case SignInStatus.Success:
            //        return RedirectToLocal(returnUrl);
            //    case SignInStatus.LockedOut:
            //        return View("Lockout");
            //    case SignInStatus.RequiresVerification:
            //        return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
            //    case SignInStatus.Failure:
            //    default:
            //        ModelState.AddModelError("", "无效的登录尝试。");
            //        return View(model);
            //}
        }





        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(UserRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User {FullName = model.Email, Email = model.Email};
                RegistManager regsterManager = new RegistManager();
                var result = await regsterManager.CreateAsync(user, model.Password);
                if (result == RegisterState.Success)
                {
                    // 有关如何启用帐户确认和密码重置的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=320771
                    // 发送包含此链接的电子邮件
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "确认你的帐户", "请通过单击 <a href=\"" + callbackUrl + "\">這裏</a>来确认你的帐户");

                    return RedirectToAction("Index", "Home");
                }
            }

            // 如果我们进行到这一步时某个地方出错，则重新显示表单
            return View();
        }

    }
}