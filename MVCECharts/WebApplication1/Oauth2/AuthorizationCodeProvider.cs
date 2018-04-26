using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace WebApplication1.Oauth2
{
    public class AuthorizationCodeProvider : IAuthenticationTokenProvider
    {
        private readonly ConcurrentDictionary<string, string> _authorizationCodes =
            new ConcurrentDictionary<string, string>(StringComparer.Ordinal);
        /// <summary>
        /// 这里的创建实际就是用户完成登录后授权码的生成以及授权码和用户登录身份信息的关联，
        /// 而解析实际就是根据授权码获得对应用户身份信息并生成Access Token的过程。
        /// </summary>
        /// <param name="context"></param>
        public void Create(AuthenticationTokenCreateContext context)
        {
            context.SetToken(Guid.NewGuid().ToString("n") + Guid.NewGuid().ToString("n"));
            _authorizationCodes[context.Token] = context.SerializeTicket();
        }

        public Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            return Task.Run(() =>
            {
                this.Create(context);
            });
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            string value;
            if (_authorizationCodes.TryRemove(context.Token, out value))
            {
                context.DeserializeTicket(value);
            }
        }

        public Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            return Task.Run(() =>
            {
                this.Receive(context);
            });
        }
    }
}