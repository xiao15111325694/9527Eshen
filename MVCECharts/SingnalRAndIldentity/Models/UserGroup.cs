using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR.Infrastructure;

namespace SingnalRAndIldentity.Models
{
    public class UserGroup
    {
        public int ID { get; set; }

        public List<RoomGroup> Room { get; set; }
    }
}