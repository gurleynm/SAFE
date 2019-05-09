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
        public void SubmitComment()
        {
            var appId = 2;
            var comment = "this is a test";
            var rate = 3.5;
            HomeController controller = new HomeController();
            ViewResult vw = controller.SubmitComment(appId, comment, rate) as ViewResult;
            Assert.IsNotNull(vw);
        }
        [TestMethod]
        public void DeleteComment()
        {
            var commentId = 1;
            var appId = 1;
            HomeController controller = new HomeController();
            ViewResult vw = controller.DeleteComment(commentId, appId) as ViewResult;
            Assert.IsNotNull(vw);
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
            Assert.IsNotNull(result);
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

        [TestMethod]
        public void filterCat() {
            var genre = "Social";
            var developer = "Instagram";
            var rate = 1;
            HomeController controller = new HomeController();
            ViewResult vw = controller.FilterCatalogue(genre, developer, rate) as ViewResult;
            Assert.IsNotNull(vw);
        }
    }


    
}
