using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SingnalRAndIldentity.Models
{
    public class UserRole
    {
        public int ID { get; set; }

        public int UserID { get; set; }

        public int RoleID { get; set; }
    }
}