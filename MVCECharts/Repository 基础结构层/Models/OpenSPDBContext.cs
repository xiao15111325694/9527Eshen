using Repository_基础结构层.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCECharts.Models
{
    public partial class OpenSPDBContext : DbContext
    {
        static OpenSPDBContext()
        {
            Database.SetInitializer<OpenSPDBContext>(null);
        }
        public OpenSPDBContext() : base("Name=OpenSPDBContext")
        {

        }
        public OpenSPDBContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        { }
        
        public DbSet<ShopingInfo> ShopingInfo { get; set; }

        public DbSet<ShopingType> ShopingType { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Configurations.Add(new ShopingInfoMap());
        //    modelBuilder.Configurations.Add(new ShopingTypeMap());
      
        //}
    }
}