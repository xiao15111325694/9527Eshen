using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Repository_基础结构层.Models
{
    public class ShopingInfoViewModel
    {
        [DisplayName("编号")]
        public int ID { get; set; }
       
        [DisplayName("商品名称")]
        public string ShopingName { get; set; }
        [DisplayName("商品数量")]
        public int ShopingCount { get; set; }
        [DisplayName("商品价格")]
        public decimal ShopingPric { get; set; }
        [DisplayName("商品销量")]
        public int Stock { get; set; }
        [DisplayName("商品库存")]
        public int Volumeofvolume { get; set; }
        [DisplayName("商品类别")]
        public string ShopingTypeName { get; set; }
    }
}