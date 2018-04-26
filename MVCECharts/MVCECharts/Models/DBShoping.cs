using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCECharts.Models
{
    public class DBShoping : DbContext
    {
        public DbSet<ShopingInfo> ShopingInfo { get; set; }

        public DbSet<ShopingType> ShopingType { get; set; }
    }
}