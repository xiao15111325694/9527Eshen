using MVCECharts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Repository_基础结构层.Models{

    public class ShopingTypeMap : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<ShopingType>

    {
        public ShopingTypeMap()
        {
            ToTable("ShopingType", "dbo");
            // keys
            HasKey(t => t.ID);

            Property(t => t.ID)
               .HasColumnName("ID")
               .IsRequired();
            Property(t => t.ShopingName)
                .HasColumnName("ShopingName");
            Property(t => t.ShopngingDesc)
                .HasColumnName("ShopingPric");
        }
    }
}