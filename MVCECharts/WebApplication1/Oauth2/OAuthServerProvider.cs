using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using WebApplication1.Oauth2.Repositories;
using System.Security.Claims;
using System.Security.Principal;

namespace WebApplication1.Oauth2
{
    public class DemoOAuthAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        /// <summary>
        /// ● 尝试从Http请求header或者请求body中获取Client信息，包含Id和密码。
        /// ● 如果没有Client的Id信息，那么直接判断为不通过验证，如果有Client的密码信息则保存到Owin上下文中，供后续处理使用。
        /// ● 使用获得的ClientId在Client仓储中查询，判断是否是一个合法的Client，如不是则判断为不通过验证
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId;
            string clientSecret;
            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            if (context.ClientId == null)
            {
                context.SetError("invalid_clientId", "client_Id is not set");
                return Task.FromResult<object>(null);
            }
            if (!string.IsNullOrEmpty(clientSecret))
            {
                context.OwinContext.Set("clientSecret", clientSecret);
            }
            var client = ClientRepository.Clients.Where(c => c.Id == clientId).FirstOrDefault();
            if (client != null)
            {
                context.Validated();
            }
            else
            {
                context.SetError("invalid_clientId", string.Format("Invalid client_id '{0}'", context.ClientId));
                return Task.FromResult<object>(null);
            }
            return Task.FromResult<object>(null);
        }
        /// <summary>
        /// 验证完成后设置该Client的重定向Url(注：该方法仍旧是重载OAuthAuthorizationServerProvider类型中的方法)
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            var client = ClientRepository.Clients.Where(c => c.Id == context.ClientId).FirstOrDefault();
            if (client != null)
            {
                context.Validated(client.RedirectUrl);
            }
            return base.ValidateClientRedirectUri(context);
        }

        public override Task GrantClientCredentials(OAuthGrantClientCredentialsContext context)
        {
            var secret = context.OwinContext.Get<string>("clientSecret");
            var client = ClientRepository.Clients.Where(c => c.Id == context.ClientId && c.Secret == secret).FirstOrDefault();
            if (client != null)
            {
                var identity = new ClaimsIdentity(
                    new GenericIdentity(context.ClientId, 
                    OAuthDefaults.AuthenticationType), 
                    context.Scope.Select(x => new Claim("urn:oauth:scope", x)));
                context.Validated(identity);
            }
            return Task.FromResult(0);
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var userManager = context.OwinContext
                .Get<ApplicationUserManager>("AspNet.Identity.Owin:" + typeof(ApplicationUserManager).AssemblyQualifiedName);
            if (userManager != null)
            {
                var user = userManager.FindAsync(context.UserName, context.Password).Result;
                if (user == null)
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect");
                    //return;
                    return Task.FromResult<object>(null);
                }
                var identity = new ClaimsIdentity(
                    new GenericIdentity(context.UserName,
                    OAuthDefaults.AuthenticationType), 
                    context.Scope.Select(x => new Claim("urn:oauth:scope", x)));
                context.Validated(identity);
            }
            return Task.FromResult(0);
        }

    }
}