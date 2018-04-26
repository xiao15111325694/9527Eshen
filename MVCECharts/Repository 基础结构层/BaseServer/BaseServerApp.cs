using Repository_基础结构层.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_基础结构层.BaseServer
{
    public class BaseServerApp<T> where T : EntityBase
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
