using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using 自定义权限控制.Models;

namespace 自定义权限控制.Repository
{
    public class LoginRepository
    {
        public ModelDBContext _context;

        public LoginRepository(ModelDBContext context)
        {
            _context = context;
        }
    }
}