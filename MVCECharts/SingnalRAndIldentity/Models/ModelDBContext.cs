using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SingnalRAndIldentity.Models
{
    public class ModelDBContext:DbContext
    {
        public DbSet<User> User { get; set; }

        public DbSet<Role> Role { get; set; }

        public DbSet<UserRole> UserRole { get; set; }

        public DbSet<UserGroup> UserGroup { get; set; }

        public DbSet<RoomGroup> RoomGroup { get; set; }
    }
}