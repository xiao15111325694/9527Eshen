using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace 自定义权限控制.Controllers
{
    [RoutePrefix("api/shop")]
    public class ShopController : ApiController
    {
        [RoleAuthorize.SupportFilter]
        [Route("")]
        public string GetValue()
        {
            return "123456";
        }

        [Route("sum")]
        public int Sum(int i, int j)
        {
            i = 5;
            j = 5;
            return i + 5;

        }
    }
}
