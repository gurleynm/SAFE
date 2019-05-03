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
    public class ModeratorControllerTests
    {

        [TestMethod]
        public void ModeratorControllerTest()
        {
            ModeratorController mc = new ModeratorController();
            Assert.IsNotNull(mc);
        }

        [TestMethod()]
        public void ManageRequestsTest()
        {
            ModeratorController mc = new ModeratorController();
            ViewResult vw = mc.ManageRequests() as ViewResult;
            Assert.IsNotNull(vw);
        }

        //[TestMethod()]
        //public void DenyRequestTest()
        //{
        //    ModeratorController mc = new ModeratorController();
        //    ViewResult vw = mc.DenyRequest(1) as ViewResult;
        //    Assert.IsNotNull(vw);
        //}
        //[TestMethod()]
        //public void ApproveRequestTest()
        //{
        //    ModeratorController mc = new ModeratorController();
        //    ViewResult vw = mc.ApproveRequest(1) as ViewResult;
        //    Assert.IsNotNull(vw);

        //}
    }
}
