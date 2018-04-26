using Repository_基础结构层.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOC.Server
{
    public interface IShopingServer
    {
        IQueryable<ShopingInfo> GetAll();

        string Placeholderfill(string htmlContext);
    }

}
