using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using safe_web_app;
using safe_web_app.Controllers;
using safe_web_app.Models;

namespace safe_web_app {
    [TestClass]
    public class ControllerTests {
        [TestMethod]
        public void CSE201Data()
        {
            CSE201Entities db = new CSE201Entities();
            Assert.IsNotNull(db);
        }

        [TestMethod]
        public void Index() {
            //Arrange 
            HomeController homeController = new HomeController();
            //Act
            ViewResult result = homeController.Index() as ViewResult;
            //Assert

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Catalogue()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Catalogue() as ViewResult;

            // Assert
            Assert.AreEqual("Catalogue", result.ViewBag.Title);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void Search()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            string var = "Instagram";
            ViewResult result = controller.Search(var) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }


    
}