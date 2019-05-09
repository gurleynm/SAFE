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
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private RoleManager<IdentityRole> _roleManager;
        
        // Utilizes ASP's sign-in manager to verify user roles
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        // Verifies authenticity of passwords etc
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

        //GET: Index
        public ActionResult Index()
        {
            return View();
        }
        
        //GET: ManageUsers
        public ActionResult ManageUsers()
        {
            //If not Admin, redirect to login page
            if (!User.IsInRole("Admin")) return RedirectToAction("Login", "Account");

            //Otherwise, load the view data
            var model = new ManageUsersViewModel();
            model.users = UserManager.Users.ToList();
            model.roles = RoleManager.Roles.ToList();
            return View(model);
        }
    }
}