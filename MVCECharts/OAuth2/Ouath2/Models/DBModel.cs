using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using OAuth2.Models;

namespace OAuth2.Ouath2.Models
{
    public class DBModel : DbContext
    {
        public DbSet<User> User { get; set; }

    }
}