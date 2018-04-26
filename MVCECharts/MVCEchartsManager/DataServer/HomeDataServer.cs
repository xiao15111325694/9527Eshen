using System.Collections.Generic;
using System.Linq;
using MVCECharts.Models;
using System.Web.Caching;
using Repository_基础结构层.Models;
using AutoMapper;

namespace MVCEchartsManager.DataServer
{

    public class HomeDataServer
    {
        OpenSPDBContext dbShoping = new OpenSPDBContext();


        public HomeDataServer()
        {

        }


        public List<ShopingInfo> GetAllShopingInfo()
        {

            return dbShoping.ShopingInfo.ToList();

        }


        public List<ShopingType> GetAllShopingType()
        {
            return dbShoping.ShopingType.ToList();
        }

        public List<ShopingInfoViewModel> GetInfoViewModels()
        {
            
            var cache = CacheHelper.GetCache("ShopingInfo");//先读取  
            if (cache == null) //如果没有该缓存  
            {
                SqlCacheDependency dep = new SqlCacheDependency("TestCache", "ShopingInfoes");
                var shopinfo = (from sp in GetAllShopingInfo()
                    join st in GetAllShopingType() on sp.ShopingTypeId equals st.ID into temp
                    from a in temp.DefaultIfEmpty()
                    select new ShopingInfoViewModel
                    {
                        ShopingName = sp.ShopingName,
                        ShopingCount = sp.ShopingCount,
                        ShopingPric = sp.ShopingPric,
                        Stock = sp.Stock,
                        Volumeofvolume = sp.Volumeofvolume,
                        ShopingTypeName = a.ShopingName
                    }).ToList();

                CacheHelper.AddobjectToCache("ShopingInfo", shopinfo, dep);//添加缓存  
                return shopinfo;
            }

            var result = (List<ShopingInfoViewModel>)cache;//有就直接返回该缓存  

            return result;
        }

        public bool SelectShopingInfoIsRepetition(ShopingInfo info)
        {
            var result = dbShoping.ShopingInfo.Where(x => x.ShopingName == info.ShopingName).ToList();
            if (result.Count >0)
            {
                return false;
            }
            return true;
        }

        public void DeleteShopingInfo(int id)
        {
            var result = dbShoping.ShopingInfo.Where(x => x.ID == id);
            dbShoping.ShopingInfo.RemoveRange(result);
            dbShoping.SaveChanges();
        }
    }
}