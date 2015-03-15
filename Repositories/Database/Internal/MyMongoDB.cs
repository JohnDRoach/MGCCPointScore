using MongoDB.Driver;
using Repositories.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Repositories.Database
{
    internal static class MyMongoDB
    {
        private const string UserCollectionString = "AspNetUsers";
        private static MongoDatabase database = null;

        // TODO: Can this be removed?
        public static MongoCollection<MyIdentityUser> UserCollection
        {
            get
            {
                return database.GetCollection<MyIdentityUser>(UserCollectionString);
            }
        }

        public static MongoDatabase Database
        {
            get 
            { 
                if(database == null)
                {
                    CreateDBConnection();
                }

                return database; 
            }
        }

        private static void CreateDBConnection()
        {
            var connectionstring = ConfigurationManager.AppSettings.Get("MONGOLAB_URI");
            var url = new MongoUrl(connectionstring);
            var client = new MongoClient(url);
            var server = client.GetServer();
            database = server.GetDatabase(url.DatabaseName);
        }
    }
}
