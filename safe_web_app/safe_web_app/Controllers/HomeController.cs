using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
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

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        private RoleManager<IdentityRole> _roleManager;
        public RoleManager<IdentityRole> RoleManager
        {
            get
            {
                if (_roleManager == null)
                {
                    _roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
                }
                return _roleManager;
            }
            set { _roleManager = value; }
        }

        /// <summary>
        /// Connection to database
        /// </summary>
        public HomeController()
        {
            this.db = new CSE201Entities();
        }
        /// <summary>
        /// Pulls all applications from the database, ordered by rating in descending order
        /// </summary>
        /// <returns>
        /// Returns the list of appllications
        /// </returns>
        public ActionResult Index()
        {
            var Model = db.Applications.Where(x => x.approved == true).OrderByDescending(x => x.rating).ToList();
            return View(Model);
        }
        /// <summary>
        /// Pulls genres and developers from database for filtering
        /// </summary>
        /// <returns>
        /// Returns a list of genres and developers from database
        /// </returns>
        public ActionResult Catalogue()
        {
            var Model = new CatalogueViewModel()
            {
                applications = db.Applications.Where(x => x.approved == true).OrderByDescending(x => x.genre).ToList(),
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
        /// <summary>
        /// Pulls the comments for each application from the database
        /// </summary>
        /// <param name="appId">
        /// Only pulls comments for apps with the matching ID
        /// </param>
        /// <returns>
        /// Returns the comments from the database for the specific app
        /// </returns>
        public ActionResult Comment(int appId)
        {
            CommentViewModel Model = new CommentViewModel();
            Model.application = db.Applications.Find(appId);
            Model.comments = db.Comments.Where(x => x.appId == appId).ToList();
            return View(Model);
        }
        /// <summary>
        /// Generates the contact page
        /// </summary>
        /// <returns>
        /// Returns the contact view
        /// </returns>
        public ActionResult Contact()
        {
            return View();
        }


        /// <summary>
        /// Generates the user manager page
        /// </summary>
        /// <returns>
        /// Returns the user manager view
        /// </returns>
        public ActionResult ManageUsers()
        {
            var Model = new UserManagerViewModel();
            Model.users = UserManager.Users.ToList();
            Model.roles = RoleManager.Roles.ToList();
            return View(Model);
        }


        /// <summary>
        /// Facilitates the filter functionality
        /// </summary>
        /// <param name="genre">
        /// Filters by a specific genre
        /// </param>
        /// <param name="developer">
        /// Filters by a specific developer
        /// </param>
        /// <param name="rating">
        /// Filters by rating less than or equal to the rating specified
        /// </param>
        /// <returns>
        /// Returns the results of the filtering
        /// </returns>
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
        /// <summary>
        /// Allows user to submit request only if the user is logged in
        /// </summary>
        /// <returns>
        /// Returns to the view
        /// </returns>
        public ActionResult SubmitRequest()
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");
            ViewBag.Submitted = false;
            return View();
        }
        /// <summary>
        /// Search functionality
        /// </summary>
        /// <param name="input">
        /// Genre or title that the user wishes to search
        /// </param>
        /// <returns>
        /// Returns the applications that match the query
        /// </returns>
        public ActionResult Search(string input)
        {
            //If the input is blank, return a blank result
            if (string.IsNullOrEmpty(input)) return View(new List<Application>());

            //Otherwise, search the DB and return the result
            var Model = db.Applications.Where(x => x.title.Contains(input) || x.genre.Contains(input)).ToList();
            return View(Model);
        }

        /// <summary>
        /// Allows a logged in user to submit a comment regarding a specific application
        /// </summary>
        /// <param name="appId">
        /// The ID of the app the user is commenting on
        /// </param>
        /// <param name="comment">
        /// The comment the user makes on the app
        /// </param>
        /// <param name="rate">
        /// The rating the user gives the app
        /// </param>
        /// <returns>
        /// Returns the user to the app's details page
        /// </returns>
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


        /// <summary>
        /// Pulls all applications from the database, ordered by rating in descending order
        /// </summary>
        /// <returns>
        /// Returns the list of appllications
        /// </returns>
        public ActionResult FAQ()
        {
            var Model = new FAQViewModel();
            Model.Name = User.Identity.GetUserName();
            ViewBag.Submitted = false;
            return View(Model);
        }

        // POST: /Home/SubmitFAQ
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult FAQ(FAQViewModel model, string returnUrl)
        {
            //Create the new Application object, flag it as not approved
            var request = new FAQ()
            {
                name = User.Identity.GetUserName(),
                question = model.Question
            };

            //Save the Application to the DB
            db.FAQs.Add(request);
            db.SaveChanges();

            ViewBag.Submitted = true;
            return View();
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
        /// <summary>
        /// Allows a moderator to delete a comment made by a user
        /// </summary>
        /// <param name="commentId">
        /// The ID of the comment itself
        /// </param>
        /// <param name="appId">
        /// The ID of the comment's application
        /// </param>
        /// <returns>
        /// Returns the user to the app's detailed page
        /// </returns>
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


        /// <summary>
        /// Allows an admin to delete a user
        /// </summary>
        /// <param name="id">
        /// The ID of the user
        /// </param>        
        /// <returns>
        /// Returns the user to the user manager page
        /// </returns>
        public ActionResult DeleteUser(string id)
        {
            var user = UserManager.FindById(id);
            if (user != null)
            {
                UserManager.Delete(user);
            }

            return RedirectToAction("ManageUsers", "Home");
        }

        /// <summary>
        /// Adds a user to a role
        /// </summary>
        /// <param name="userId">
        /// The ID of the user
        /// </param>
        /// <param name="role">
        /// The name of the role
        /// </param>
        /// <returns>
        /// Returns the user to the user manager page
        /// </returns>
        public ActionResult AddUserToRole(string userId, string role)
        {
            var user = UserManager.FindById(userId);
            if (user != null)
            {
                UserManager.AddToRole(userId, role);                
            }

            return RedirectToAction("ManageUsers", "Home");
        }


        /// <summary>
        /// Removes a user to a role
        /// </summary>
        /// <param name="userId">
        /// The ID of the user
        /// </param>
        /// <param name="role">
        /// The name of the role
        /// </param>
        /// <returns>
        /// Returns the user to the user manager page
        /// </returns>
        public ActionResult RemoveUserFromRole(string userId, string role)
        {
            var user = UserManager.FindById(userId);
            if (user != null)
            {
                UserManager.RemoveFromRole(userId, role);
            }

            return RedirectToAction("ManageUsers", "Home");
        }
    }
}