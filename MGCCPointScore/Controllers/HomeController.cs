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
        public ActionResult Index()
        {
            ViewBag.Message = "Big string for the report.\nTabs\tTabs\tTabs\nYay\ty\tYayyy\n";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "I should remove this!";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "I should remove this!";
            return View();
        }

        public ActionResult ScoreManagement()
        {
            return View();
        }
    }
}
