using MGCCPointScore.Database;
using MGCCPointScore.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MGCCPointScore.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
            //var connectionstring = ConfigurationManager.AppSettings.Get("MONGOLAB_URI");
            //var url = new MongoUrl(connectionstring);
            //var client = new MongoClient(url);
            //var server = client.GetServer();
            //database = server.GetDatabase(url.DatabaseName);

            //var collection = database.GetCollection<MongoTestObject>("TestTableQuestionMark");

            //// insert object
            //collection.Insert(new MongoTestObject { Name = "This came from mongo!" });
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            // fetch all objects
            var database = MyMongoDB.Database;
            var collection = MyMongoDB.UserCollection;
            
            var thingies = collection.FindAll();

            StringBuilder bob = new StringBuilder();

            foreach(var thing in thingies)
            {
                bob.AppendFormat("{0} : {1}\n", thing.UserName, thing.Id);
            }

            ViewBag.Message = bob.ToString();

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}