using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Cache
{
    public abstract class ICacheContext
    {
        /// <summary>
        /// 获取缓存项
        /// </summary>
        /// <typeparam name="T">缓存对象类型</typeparam>
        /// <param name="key">键</param>
        /// <returns>缓存对象</returns>
        public abstract T GetCache<T>(string key);

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <typeparam name="T">缓存对象类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="t">缓存对象</param>
        /// <returns></returns>
        public abstract bool SetCache<T>(string key, T t);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">缓存对象类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="t">缓存对象</param>
        /// <param name="time">缓存过期时间</param>
        /// <returns></returns>
        public abstract bool SetCache<T>(string key, T t, DateTime time);
        
        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public abstract bool RemoveCache(string key);

    }
}
