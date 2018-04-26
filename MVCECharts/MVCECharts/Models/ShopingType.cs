using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCECharts.Models
{
    public class ShopingType
    {
        public int ID { get; set; }
        public string ShopingName { get; set; }
        public string ShopngingDesc { get; set; }
        public ICollection<ShopingInfo> ShopingInfo { get; set; }
    }
}
