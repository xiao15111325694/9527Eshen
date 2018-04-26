using SingnalRAndIldentity.Models;
using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;


namespace SingnalRAndIldentity.Manager
{
    public class RegistManager
    {
        private ModelDBContext db = new ModelDBContext();
        public async Task<RegisterState> CreateAsync(User user,string password)
        {
            user.Password = password;
            using (var dbContextTransaction =  db.Database.BeginTransaction())
            {
                 db.User.Add(user);
                 await db.SaveChangesAsync();

                dbContextTransaction.Commit();
                return RegisterState.Success;
            }
        }
    }
}