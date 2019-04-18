using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using safe_web_app;
using safe_web_app.Controllers;

namespace safe_web_app.Controllers.Tests
{
    [TestClass()]
    public class ManageControllerTests
    {
        [TestMethod()]
        public void ManageControllerTest()
        {
            ManageController manageController = new ManageController();
            Assert.IsNotNull(manageController);
        }

        [TestMethod()]
        public void ChangePasswordTest()
        {
            ManageController manageController = new ManageController();
            ViewResult vw = manageController.ChangePassword() as ViewResult;
            Assert.IsNotNull(vw);
        }
    }
}
