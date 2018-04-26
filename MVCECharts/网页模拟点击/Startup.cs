using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(网页模拟点击.Startup))]

namespace 网页模拟点击
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {

        }
    }
}
