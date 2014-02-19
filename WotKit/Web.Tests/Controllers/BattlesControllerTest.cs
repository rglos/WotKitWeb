using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Web.Controllers;
using System.Web.Mvc;
using Web.Models;

namespace Web.Tests.Controllers
{
    [TestClass]
    public class BattlesControllerTest
    {
        [TestMethod]
        public void Exercise_Index()
        {
            // Arrange
            var target = new BattlesController();

            // Act
            var actual = target.Index();

            // Assert
            Assert.IsInstanceOfType(actual, typeof(ViewResult));
            var viewResult = (ViewResult)actual;
            Assert.IsNotNull(viewResult.Model);
            Assert.IsInstanceOfType(viewResult.Model, typeof(BattlesIndexModel));

        }
    }
}
