using Repository_基础结构层.BaseServer;
using Repository_基础结构层.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_基础结构层.Server
{
    public class UserServer: BaseServerApp<UserBaseModel>
    {
        public UserBaseModel GetByUserName(string userName)
        {
            var entity = RepositoryBase.FindSingle(x => x.UserName == userName);
            return entity;
        }

        public IQueryable<UserBaseModel> GetAll()
        {
            return RepositoryBase.FindAll();
        }

    }
}
