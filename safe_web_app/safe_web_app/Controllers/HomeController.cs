﻿using safe_web_app.Models;
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
            var Model = db.Applications.Where(x => x.approved == true).ToList();
            return View(Model);
        }

        public ActionResult Catalogue()
        {
            var Model = db.Applications.Where(x => x.approved == true).ToList();
            return View(Model);
        }

        public ActionResult Comment()
        {
            CommentViewModel Model = new CommentViewModel();
            Model.applications = db.Applications.ToList();
            Model.comments = db.Comments.ToList();
            return View(Model);
        }

        public ActionResult Contact()
        {
            return View();
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
            var Model = db.Applications.Where(x => x.title.Contains(input)).ToList();
            return View(Model);
        }

        // POST: /Home/SubmitRequest
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Comment(CommentViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            List<Comment> m = model.comments.ToList();

            //Create the new Application object, flag it as not approved
            var request = new Comment()
            {
                appId = 1,
                comment1 = m[0].comment1
            };

            //Save the Comment to the DB
            db.Comments.Add(request);
            db.SaveChanges();
            
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
                url = model.Url,
                approved = false
            };

            //Save the Application to the DB
            db.Applications.Add(request);
            db.SaveChanges();

            ViewBag.Submitted = true;
            return View();
        }
    }
}