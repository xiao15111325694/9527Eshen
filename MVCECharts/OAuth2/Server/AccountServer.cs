using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using OAuth2.Ouath2.Models;

namespace OAuth2.Server
{
    public class AccountServer
    {
        public User ValidUserByUserNameAndPwd(string userName,string Pwd)
        {
            DBModel db= new DBModel();
            var List = from u in db.User
                where u.Email == userName && u.Password == Pwd
                select u;
            return List.FirstOrDefault();
        }
    }
}