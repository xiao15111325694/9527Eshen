using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using 自定义权限控制.Models;
using 自定义权限控制.Models.ViewModel;

namespace 自定义权限控制.Manager
{
    public class LoginManager
    {
        ModelDBContext db = new ModelDBContext();
        public LoginState SignIn(string Email, string password)
        {
           
            var ss = db.User.ToList();
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

        public string GetUserRoleByEmailAsync(string email)
        {
           var userEntity= db.User.Where(x => x.Email == email).Select(u => 
            new UserViewModel()
            {
                ID = u.ID,
                FullName = u.FullName,
                CreateTime = u.CreateTime,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber
            }).FirstOrDefault();
            var roleId = db.UserRole.Where(x => x.UserID == userEntity.ID).FirstOrDefault().RoleID;
            var roleName = db.Role.Where(x => x.ID == roleId).FirstOrDefault().Name;
            return roleName;
        }
    }
}