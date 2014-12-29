using MGCCPointScore.Models;
using Microsoft.AspNet.Identity;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MGCCPointScore.Database
{
    public class MongoUserStore : IUserStore<ApplicationUser>
    {
        private MongoCollection<ApplicationUser> UserCollection
        {
            get
            {
                return MyMongoDB.UserCollection;
            }
        }

        public System.Threading.Tasks.Task CreateAsync(ApplicationUser user)
        {
            return new Task(() => UserCollection.Insert(user));
        }

        public System.Threading.Tasks.Task DeleteAsync(ApplicationUser user)
        {
            return new Task(() => UserCollection.Remove(Query.EQ("UserName", user.UserName)));
        }

        public System.Threading.Tasks.Task<ApplicationUser> FindByIdAsync(string userId)
        {
            return new Task<ApplicationUser>(() => UserCollection.FindOne(Query.EQ("Id", userId)));
        }

        public System.Threading.Tasks.Task<ApplicationUser> FindByNameAsync(string userName)
        {
            return new Task<ApplicationUser>(() => UserCollection.FindOne(Query.EQ("UserName", userName)));
        }

        public System.Threading.Tasks.Task UpdateAsync(ApplicationUser user)
        {
            return Task.Delay(1);
        }

        public void Dispose()
        {
        }
    }
}
