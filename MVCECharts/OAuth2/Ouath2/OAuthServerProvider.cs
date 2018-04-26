using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Security.Principal;
using OAuth2.Ouath2.Models;
using OAuth2.Ouath2.Repositories;
using OAuth2.Server;

namespace OAuth2.Ouath2
{
    public class OAuthServerProvider : OAuthAuthorizationServerProvider
    {

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

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            var client = ClientRepository.Clients.Where(c => c.Id == context.ClientId).FirstOrDefault();
            if (client != null)
            {
                context.Validated(client.RedirectUrl);
            }
            return base.ValidateClientRedirectUri(context);
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var userManager = context.OwinContext
                .Get<ApplicationUserManager>("AspNet.Identity.Owin:" + typeof(ApplicationUserManager).AssemblyQualifiedName);
            if (userManager != null)
            {
                AccountServer acc= new AccountServer();
                var user = acc.ValidUserByUserNameAndPwd(context.UserName, context.Password);
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