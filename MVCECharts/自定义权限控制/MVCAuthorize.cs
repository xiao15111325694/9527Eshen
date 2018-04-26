using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using System.Web.Security;
using System.Xml.Linq;
using 自定义权限控制.Models.Enum;
using AllowAnonymousAttribute = System.Web.Http.AllowAnonymousAttribute;

namespace 自定义权限控制
{
    public class MVCAuthorize : AuthorizeAttribute
    {
        public new string Roles { get; set; } //这个是从Action中传过来的角色
        public override void OnAuthorization(AuthorizationContext actionContext)
        {

            var token = actionContext.HttpContext.Request.Params["Token"];

            if (!string.IsNullOrEmpty(token))
            {
                string controllerName = actionContext.ActionDescriptor.ControllerDescriptor.ControllerName; //得到控制器名称
                string actionName = actionContext.ActionDescriptor.ActionName;//得到Action名称
                string roles = GetActionRoles(actionName, controllerName); // 在Xml文件中根据控制器 Action 找到对应的访问角色权限
                if (!string.IsNullOrWhiteSpace(roles))
                {
                    this.Roles = Roles + "," + roles;
                    var isAuthValidate = ValidateTicket(token, Roles);
                    if (isAuthValidate != AuthorizeState.ValidateSuucss)
                    {
                        base.HandleUnauthorizedRequest(actionContext);
                    }

                }
            }
            else
            {
                base.HandleUnauthorizedRequest(actionContext);
            }


        }

        //获取当前Action的访问角色
        public static string GetActionRoles(string action, string controller)
        {
            XElement rootElement = XElement.Load(HttpContext.Current.Server.MapPath("/") + "/XML/RoleAction.xml");
            XElement controllerElement = findElementByAttribute(rootElement, "Controller", controller);
            if (controllerElement != null)
            {
                XElement actionElement = findElementByAttribute(controllerElement, "Action", action);
                if (actionElement != null)
                {
                    return actionElement.Value;
                }
            }
            return "";
        }

        public static XElement findElementByAttribute(XElement xElement, string tagName, string attribute)
        {
            return xElement.Elements(tagName).FirstOrDefault(x => x.Attribute("name").Value.Equals(attribute, StringComparison.OrdinalIgnoreCase));
        }

        //校验用户名密码（对Session匹配，或数据库数据匹配）
        private AuthorizeState ValidateTicket(string encryptToken,string role)
        {
            if (encryptToken == null)
            {
                return AuthorizeState.TokenErro;
            }
            //解密Ticket
            var strTicket = FormsAuthentication.Decrypt(encryptToken).UserData;

            //从Ticket里面获取用户名和密码
            var index = strTicket.IndexOf("&");
            string userName = strTicket.Substring(0, index);
            string password = strTicket.Substring(index + 1);
            ArrayList arrayList = new ArrayList(role.Split(','));
            var roleName = HttpContext.Current.Session["Role"].ToString();
            var name = HttpContext.Current.Session["UserName"].ToString();
            //取得session，不通过说明用户退出，或者session已经过期
            if (arrayList.Contains(roleName) && name == userName)  //获取对应控制器 对应方法的访问角色权限 如果包含说明符合访问 否则返回权限错误
            {
                return AuthorizeState.ValidateSuucss;
            }
            else
            {
                return AuthorizeState.UserValidateErro;
            }
        }
    }
}