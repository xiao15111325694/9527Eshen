using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace 自定义权限控制.Models.ViewModel
{
    public class UserViewModel
    {
        public int ID { get; set; }

        public string FullName { get; set; }

        public DateTime? CreateTime { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}