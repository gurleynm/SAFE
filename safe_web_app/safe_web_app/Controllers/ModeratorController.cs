using safe_web_app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace safe_web_app.Controllers
{
    [Authorize(Roles = "Admin, Moderator")]
    public class ModeratorController : Controller
    {
        public CSE201Entities db;

        // Connection to the database
        public ModeratorController()
        {
            this.db = new CSE201Entities();
        }

        // GET: Moderator
        public ActionResult ManageRequests()
        {
            var Model = new ManageRequestsViewModel();
            Model.requests = db.Applications.Where(x => x.approved == false).ToList();
            return View(Model);
        }

        // Allows the admin to deny an application request at a specific ID
        public ActionResult DenyRequest(int id)
        {
            var application = db.Applications.Find(id);
            if (application != null)
            {
                db.Applications.Remove(application);
                db.SaveChanges();
            }
            return RedirectToAction("ManageRequests", "Moderator");
        }

        // Allows the admin to deny a request at a specific ID
        public ActionResult ApproveRequest(int id)
        {
            var application = db.Applications.Find(id);
            if (application != null)
            {
                application.approved = true;                
                db.SaveChanges();
            }
            return RedirectToAction("ManageRequests", "Moderator");
        }
    }
}