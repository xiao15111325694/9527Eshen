using Repository_基础结构层.BaseServer;
using Repository_基础结构层.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository_基础结构层.Server
{
    public class ShopingServer:BaseServerApp<ShopingInfo>
    {
        public List<ShopingInfo> GetAll()
        {
            return RepositoryBase.FindAll().ToList();
        }

        public ShopingInfo GetById(int id)
        {
            return RepositoryBase.Find(id);
        }

        public IQueryable<ShopingInfo> GetByExp(Expression<Func<ShopingInfo,bool>> exp)
        {
            return RepositoryBase.Find(exp);
        }

        public void Add(ShopingInfo entity)
        {
            RepositoryBase.Add(entity);
        }

        public void AddBatch(ShopingInfo[] entitys)
        {
            RepositoryBase.AddBatch(entitys);
        }

        public void AddBatch(List<ShopingInfo> entitys)
        {
            RepositoryBase.AddBatch(entitys);
        }

        public void Update(ShopingInfo entity)
        {
            RepositoryBase.Update(entity);
        }

        public ShopingInfo Update(ShopingInfo entity,int id)
        {
            return RepositoryBase.Update(entity, id);
        }

        public void Delete(ShopingInfo entity)
        {
            RepositoryBase.Delete(entity);
        }


    }
}
