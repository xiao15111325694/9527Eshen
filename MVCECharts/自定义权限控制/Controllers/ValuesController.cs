using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace 自定义权限控制.Controllers
{
    public class ValuesController : ApiController
    {
        [RoleAuthorize.SupportFilter(Roles ="Admin")]
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        [RoleAuthorize.SupportFilter]
        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        [RoleAuthorize.SupportFilter]
        // POST api/values?Vvar 
        public void Post([FromBody]string value)
        {
        }

        [RoleAuthorize.SupportFilter]
        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        [RoleAuthorize.SupportFilter]
        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
