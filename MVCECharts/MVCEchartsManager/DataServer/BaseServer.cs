using Repository_基础结构层;
using Repository_基础结构层.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCEchartsManager.DataServer
{
    public class BaseServer<T> where T: EntityBase
    {
        /// <summary>
        /// 用于普通数据操作
        /// </summary>
        public IRepositoryBase<T> RepositoryBase { get; set; }

        /// <summary>
        /// 按id批量删除
        /// </summary>
        /// <param name="ids"></param>
        public void Delete(int[] ids)
        {
            RepositoryBase.Delete(u => ids.Contains(u.ID));
        }

        public T Get(int id)
        {
            return RepositoryBase.FindSingle(u => u.ID == id);
        }
    }
}