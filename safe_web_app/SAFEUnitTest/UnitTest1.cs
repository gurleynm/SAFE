using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using safe_web_app;

namespace SAFEUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Class1 cs = new Class1();
            Assert.IsNotNull(cs);
        }

        [TestMethod]
        public void TestMethod2()
        {
            safe_web_app.Controllers.AccountController ac = new safe_web_app.Controllers.AccountController();

            Assert.IsNotNull(ac);
        }
    }
}
