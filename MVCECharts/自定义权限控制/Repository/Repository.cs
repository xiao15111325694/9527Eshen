using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using 自定义权限控制.Models;


namespace 自定义权限控制.Repository
{
    interface Repository<T> where T:class
    {
        IEnumerable<T> FindAll(Func<T, bool> exp);
        void Add(T entity);
        void Delete(T entity);
        void Save();

    }
}