using MVCECharts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Repository_基础结构层.Models
{
    public class ShopingInfoMap : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<ShopingInfo>
    {
        public ShopingInfoMap()
        {
            ToTable("ShopingInfo", "dbo");
            // keys
            HasKey(t => t.ID);

            Property(t => t.ID)
               .HasColumnName("ID")
               .IsRequired();
            Property(t => t.ShopingName)
                .HasColumnName("ShopingName")
                .HasMaxLength(255);
            Property(t => t.ShopingPric)
                .HasColumnName("ShopingPric");
            Property(t => t.Stock)
                .HasColumnName("Stock");
            Property(t => t.ShopingTypeId)
                .HasColumnName("ShopingTypeId");
        }
    }
}