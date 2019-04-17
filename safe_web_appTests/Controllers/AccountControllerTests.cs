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
    public class AccountControllerTests
    {

        [TestMethod]
        public void AccountControllerTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void RegisterTest()
        {
            AccountController accountController = new AccountController();
            ViewResult vw = accountController.Register() as ViewResult;
            Assert.IsNotNull(vw);
        }

        [TestMethod()]
        public void LoginTest()
        {
            string url = "";
            AccountController accountController = new AccountController();
            ViewResult vw = accountController.Login(url) as ViewResult;
            Assert.IsNotNull(vw);
        }
    }
}

