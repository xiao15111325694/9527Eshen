using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using SingnalRAndIldentity.Models;

namespace SingnalRAndIldentity.Manager
{
    public class LoginManager
    {
        public LoginState SignIn(string Email, string password)
        {
            ModelDBContext db = new ModelDBContext();
            
            if (db.User.Any(x => x.Email == Email) && db.User.Any(x => x.Password == password))
            {
                return LoginState.Success;
            }

            //var user= db.User.Where(x=>x.Email== DbFunctions.AsNonUnicode(Email)).FirstOrDefault();
            //if (user != null)
            //{
            //    if (user.Password==password)
            //    {
            //        return LoginState.Success;
            //    }
            //}

            return LoginState.Defeated;
        }
    }
}