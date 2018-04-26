using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCEchartsManager.DataServer
{
    public interface ICache
    {
       object GetCache(string cacheKey);
    }
}