using safe_web_app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace safe_web_app.Controllers
{
    public class HomeController : Controller
    {

        public CSE201Entities db;

        public HomeController()
        {
            this.db = new CSE201Entities();
        }

        public ActionResult Index()
        {
            var Model = db.Applications.ToList();
            return View(Model);
        }

        public ActionResult About()
        {
            var Model = db.Applications.ToList();
            return View(Model);
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Search(string input)
        {
            //If the input is blank, return a blank result
            if (string.IsNullOrEmpty(input)) return View(new List<Application>());

            //Otherwise, search the DB and return the result
            var Model = db.Applications.Where(x => x.title.Contains(input)).ToList();
            return View(Model);
        }
    }
}