using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Repository_基础结构层.Models
{
    public class ShopingInfo:EntityBase
    {

        public string ShopingName { get; set; }

        public int ShopingCount { get; set; }

        public decimal ShopingPric { get; set; }

        public int Stock { get; set; }

        public int Volumeofvolume { get; set; }

        public int ShopingTypeId { get; set; }

        public virtual ShopingType ShopingType { get; set; }
    }
}