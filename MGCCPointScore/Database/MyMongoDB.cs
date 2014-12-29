using MGCCPointScore.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace MGCCPointScore.Database
{
    public static class MyMongoDB
    {
        private const string UserCollectionString = "User";
        private static MongoDatabase database = null;

        public static MongoCollection<ApplicationUser> UserCollection
        {
            get
            {
                return database.GetCollection<ApplicationUser>(UserCollectionString);
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
