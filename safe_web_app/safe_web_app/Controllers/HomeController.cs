using Microsoft.AspNet.Identity;
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
            var Model = db.Applications.Where(x => x.approved == true).OrderByDescending(x => x.rating).ToList();
            return View(Model);
        }

        public ActionResult Catalogue()
        {
            var Model = new CatalogueViewModel()
            {
                applications = db.Applications.Where(x => x.approved == true).ToList(),
                genres = new List<string>(),
                developers = new List<string>()
            };

            foreach (var app in Model.applications)
            {
                if (!Model.genres.Contains(app.genre))
                {
                    Model.genres.Add(app.genre);
                }
                if (!Model.developers.Contains(app.developer))
                {
                    Model.developers.Add(app.developer);
                }
            }

            return View(Model);
        }

        public ActionResult Comment(int appId)
        {
            CommentViewModel Model = new CommentViewModel();
            Model.application = db.Applications.Find(appId);
            Model.comments = db.Comments.Where(x => x.appId == appId).ToList();
            return View(Model);
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FilterCatalogue(string genre, string developer, int rating)
        {
            var Model = new CatalogueViewModel();
            Model.applications = db.Applications.Where(x => x.approved == true).ToList();
            Model.genres = new List<string>();
            Model.developers = new List<string>();

            foreach (var app in Model.applications)
            {
                if (!Model.genres.Contains(app.genre))
                {
                    Model.genres.Add(app.genre);
                }
                if (!Model.developers.Contains(app.developer))
                {
                    Model.developers.Add(app.developer);
                }
            }

            //Filter the results by the provided filters
            if (genre != "Any")
            {
                Model.applications = Model.applications.Where(x => x.genre == genre).ToList();
            }
            if (developer != "Any")
            {
                Model.applications = Model.applications.Where(x => x.developer == developer).ToList();
            }
            if (rating != 0)
            {
                Model.applications = Model.applications.Where(x => x.rating >= rating).ToList();
            }

            return View("~/Views/Home/Catalogue.cshtml", Model);
        }

        public ActionResult SubmitRequest()
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");
            ViewBag.Submitted = false;
            return View();
        }

        public ActionResult Search(string input)
        {
            //If the input is blank, return a blank result
            if (string.IsNullOrEmpty(input)) return View(new List<Application>());

            //Otherwise, search the DB and return the result
            var Model = db.Applications.Where(x => x.title.Contains(input) || x.genre.Contains(input)).ToList();
            return View(Model);
        }


        [HttpPost]
        public ActionResult SubmitComment(int appId, string comment, double rate)
        {
            var application = db.Applications.Find(appId);
            application.sumOfRates += rate;
            application.numOfPeopleWhoRated += 1;
            application.rating = application.sumOfRates / application.numOfPeopleWhoRated;
            if (application != null)
            {
                if (comment != "")
                {
                    var c = new Comment()
                    {
                        appId = appId,
                        comment1 = comment,
                        name = User.Identity.GetUserName(),
                        rating = rate
                    };
                    db.Comments.Add(c);
                }
                db.SaveChanges();
            }
            return RedirectToAction("Comment", "Home", new { appId = appId });
        }


        // POST: /Home/SubmitRequest
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitRequest(RequestViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Submitted = false;
                return View(model);
            }

            //Create the new Application object, flag it as not approved
            var request = new Application()
            {
                title = model.Title,
                genre = model.Genre,
                app_desc = model.Description,
                price = model.Price,
                rating = 0,
                imageUrl = model.ImageUrl,
                url = model.Url,
                approved = false,
                sumOfRates = 0,
                numOfPeopleWhoRated = 0
            };

            //Save the Application to the DB
            db.Applications.Add(request);
            db.SaveChanges();

            ViewBag.Submitted = true;
            return View();
        }

        public ActionResult DeleteComment(int commentId, int appId)
        {
            var comment = db.Comments.Find(commentId);
            if (comment != null)
            {
                db.Comments.Remove(comment);
                db.SaveChanges();
            }

            return RedirectToAction("Comment", "Home", new { appId = appId });
        }
    }
}