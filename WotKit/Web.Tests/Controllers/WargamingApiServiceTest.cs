using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Web.Services;

namespace Web.Tests.Controllers
{
    [TestClass]
    public class WargamingApiServiceTest
    {
        [TestMethod]
        public void Exercise_GetListOfVehicles()
        {
            // Arrange
            var target = new WargamingApiService();
            
            // Act
            var actual = target.GetListOfVehicles();

            // Assert
            Assert.AreEqual("ok", actual.Status);
            Assert.IsTrue(actual.Count > 0);
            Assert.AreEqual(actual.Count, actual.Tanks.Count);
        }
    }
}
