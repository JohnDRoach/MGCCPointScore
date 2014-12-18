using MGCCPointScore.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MGCCPointScore.Controllers
{
    public class HomeController : Controller
    {
        private readonly MongoDatabase database;

        public HomeController()
        {
            var connectionstring = ConfigurationManager.AppSettings.Get("MONGOLAB_URI");
            var url = new MongoUrl(connectionstring);
            var client = new MongoClient(url);
            var server = client.GetServer();
            database = server.GetDatabase(url.DatabaseName);

            var collection = database.GetCollection<MongoTestObject>("TestTableQuestionMark");

            // insert object
            collection.Insert(new MongoTestObject { Name = "This came from mongo!" });
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            // fetch all objects
            var collection = database.GetCollection<MongoTestObject>("TestTableQuestionMark");
            var thingies = collection.FindAll();

            ViewBag.Message = thingies.First().Name;

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}