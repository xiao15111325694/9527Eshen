using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Repository_基础结构层.Models
{
    public class ShopingType : EntityBase
    {
        public string ShopingName { get; set; }
        public string ShopngingDesc { get; set; }
        public ICollection<ShopingInfo> ShopingInfo { get; set; }
    }
}
