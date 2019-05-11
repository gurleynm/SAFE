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
              
    }
}
