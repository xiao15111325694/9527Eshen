using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository_基础结构层
{
    public interface IRepositoryBase<T> where T:class
    {

        IQueryable<T> FindAll();

        T FindSingle(Expression<Func<T, bool>> exp = null);

        T Find(int id);

        IQueryable<T> Find(Expression<Func<T, bool>> exp = null);

        /// <summary>
        /// Linq表达式树查询分页
        /// </summary>
        /// <returns></returns>
        IQueryable<T> Find(int pageindex = 1, int pagesize = 10, Expression<Func<T, bool>> exp = null);

        /// <summary>
        /// 得到受影响条数
        /// </summary>
        /// <returns></returns>
        int GetCount(Expression<Func<T, bool>> exp = null);

        void Add(T entity);

        void AddBatch(T[] entitys);

        void AddBatch(List<T> entitys);

        /// <summary>
        /// 更新实体所有属性
        /// </summary>
        /// <returns></returns>
        void Update(T entity);

        void Delete(T entity);


        /// <summary>
        /// 按指定的ID进行批量更新
        /// </summary>
        void Update(Expression<Func<T, object>> identityExp,T entity);

        T Update(T entity,int id);

        /// <summary>
        /// 批量删除
        /// </summary>
        void Delete(Expression<Func<T, bool>> exp);

        void Save();

        int ExecuteSql(string sql);
    }
}
