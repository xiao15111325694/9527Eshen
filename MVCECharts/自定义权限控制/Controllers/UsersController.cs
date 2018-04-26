using mshtml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Script.Serialization;
using 自定义权限控制.Manager;
using 自定义权限控制.Models;

namespace 自定义权限控制.Controllers
{
    public class UsersController : ApiController
    {
        private ModelDBContext db = new ModelDBContext();

        [RoleAuthorize.SupportFilter]
        // GET: api/Users
        public IHttpActionResult GetUser(PublicModel publicModel)
        {
            SHDocVw.InternetExplorer ie = new SHDocVw.InternetExplorer();

            ie.Navigate("https://login.taobao.com/member/login.jhtml?redirectURL=https%3A%2F%2Fai.taobao.com%2F%3Fpid%3Dmm_112599953_14918079_77950911");
            ie.Visible = true;

            IHTMLDocument3 doc = (IHTMLDocument3)ie.Document;
            doc.getElementById("TPL_username_1").innerText = "肖梓璇35834616";
            doc.getElementById("TPL_password_1").innerText = "xiao4563515"; 
            doc.getElementById("J_SubmitStatic").click();


            ie.Navigate("http://i.taobao.com/my_taobao.htm");
            ie.Visible = true;

            ie.Navigate(" https://buyertrade.taobao.com/trade/itemlist/list_bought_items.htm?spm=a1z02.1.972272805.d4919663.73d2782d6TTeTm&action=itemlist/BoughtQueryAction&event_submit_do_query=1&tabCode=waitRate");
            ie.Visible = true;
            doc.getElementById("rateOrder").click();
            var ss = doc.documentElement.outerText = "1111";
      

            var userList = db.User.ToList();
            var data = Newtonsoft.Json.JsonConvert.SerializeObject(userList);
            return Json(data);
        }

        [RoleAuthorize.SupportFilter]
        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> GetUser(int id)
        {
            User user = await db.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [RoleAuthorize.SupportFilter]
        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.ID)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        //
        // POST: api/Users
        [HttpPost]
        [RoleAuthorize.SupportFilter]
        public IHttpActionResult appClick(PublicModel publicModel)
        {
            SHDocVw.InternetExplorer ie = new SHDocVw.InternetExplorer();

            ie.Navigate(publicModel.Url);
            ie.Visible = true;

            IHTMLDocument3 doc = (IHTMLDocument3)ie.Document;
            doc.getElementById(publicModel.InputText1).innerText = "hello world";
            doc.getElementById(publicModel.SavaId).click();
            return Json("yes");
        }

        //[RoleAuthorize.SupportFilter]
        // POST: api/Users
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.User.Add(user);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = user.ID }, user);
        }


        public static T GetJsonObject<T>(string value)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();

            value = HttpContext.Current.Server.HtmlDecode(value);
            value = value.Replace("[and]", "&");
            return js.Deserialize<T>(value);
        }

        //[RoleAuthorize.SupportFilter]
        // DELETE: api/Users/delete
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> DeleteUser(int id)
        {
            User user = await db.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            db.User.Remove(user);
            await db.SaveChangesAsync();

            return Ok(user);
        }

        [RoleAuthorize.SupportFilter]
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [RoleAuthorize.SupportFilter]
        private bool UserExists(int id)
        {
            return db.User.Count(e => e.ID == id) > 0;
        }

    }
}