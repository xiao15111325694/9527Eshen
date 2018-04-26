using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace 自定义权限控制.Models
{
    public class UserGroup
    {
        public int ID { get; set; }

        public List<RoomGroup> Room { get; set; }
    }
}