using Microsoft.AspNet.Identity;
using MongoDB.AspNet.Identity;
using Repositories.Database;

namespace Repositories.Identity
{
    public static class MyUserStoreFactory
    {
        public static IUserStore<T> Create<T>() where T : IdentityUser 
        {
            return new UserStore<T>(Connection.String);
        }
    }
}
