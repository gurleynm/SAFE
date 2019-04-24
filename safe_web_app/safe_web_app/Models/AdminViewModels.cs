using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace safe_web_app.Models
{
    public class ManageUsersViewModel
    {
        public List<ApplicationUser> users { get; set; }
        public List<IdentityRole> roles { get; set; }
    }
}