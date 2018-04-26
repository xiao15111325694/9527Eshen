using MVCECharts.Models;
using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;
using EntityFramework.Extensions;
using System.Data.Entity.Migrations;
using System.Collections.Generic;

namespace Repository_基础结构层.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T:class
    {
        protected OpenSPDBContext openSPDBContext = new OpenSPDBContext();

         public void Add(T entity)
        {
            openSPDBContext.Set<T>().Add(entity);
            Save();
        }

        public void AddBatch(T[] entitys)
        {
            openSPDBContext.Set<T>().AddRange(entitys);
            Save();
        }

        public void Delete(Expression<Func<T, bool>> exp)
        {
           var entitys= openSPDBContext.Set<T>().Where(exp);
            openSPDBContext.Set<T>().RemoveRange(entitys);
        }

        public void Delete(T entity)
        {
            openSPDBContext.Set<T>().Remove(entity);
            Save();
        }

        public int ExecuteSql(string sql)
        {
            return openSPDBContext.Database.ExecuteSqlCommand(sql);
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> exp = null)
        {
            return Filter(exp);
        }

        public T Find(int id)
        {
          return openSPDBContext.Set<T>().Find(id);
        }

        public IQueryable<T> Find(int pageindex, int pagesize, Expression<Func<T, bool>> exp = null)
        {

            return Filter(exp).Skip(pagesize * (pageindex - 1)).Take(pagesize);
        }


        public IQueryable<T> FindAll()
        {
            return openSPDBContext.Set<T>();
        }

        public T FindSingle(Expression<Func<T, bool>> exp = null)
        {
            return openSPDBContext.Set<T>().AsNoTracking().FirstOrDefault(exp);
        }

        public int GetCount(Expression<Func<T, bool>> exp = null)
        {
            return Filter(exp).Count();
        }

        public void Update(T entity)
        {
            openSPDBContext.Entry(entity).State = EntityState.Modified;
            Save();
        }

        /// <summary>
        /// 按指定id更新实体,会更新整个实体
        /// </summary>
        /// <param name="identityExp">The identity exp.</param>
        /// <param name="entity">The entity.</param>
        public void Update(Expression<Func<T, object>> identityExp, T entity)
        {
            openSPDBContext.Set<T>().AddOrUpdate(identityExp, entity);
        }

        public IQueryable<T> Filter(Expression<Func<T,bool>> exp)
        {
            var dbset = openSPDBContext.Set<T>().AsQueryable();
            if (exp != null)
            dbset = dbset.Where(exp);

            return dbset;

        }

        public void Save()
        {
            try
            {
                openSPDBContext.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                throw new Exception(e.EntityValidationErrors.First().ValidationErrors.First().ErrorMessage);
                throw;
            }
        }

        public void AddBatch(List<T> entitys)
        {
            openSPDBContext.Set<T>().AddRange(entitys);
        }

        public T Update(T entity, int id)
        {
            openSPDBContext.Entry(entity).State = EntityState.Modified;
            Save();
            return openSPDBContext.Set<T>().Find(id);
        }   
    }
}
