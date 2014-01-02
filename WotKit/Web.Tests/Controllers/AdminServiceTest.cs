using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Web.Services;

namespace Web.Tests.Controllers
{
    [TestClass]
    public class AdminServiceTest
    {
        [TestMethod]
        public void Exercise_UpdateVehicles()
        {
            // Arrange
            var wargamingApiService = new WargamingApiService();
            var listOfVehicles = wargamingApiService.GetListOfVehicles();
            var target = new AdminService();

            // Act
            var actual = target.UpsertVehicles(listOfVehicles);

            // Assert
            Assert.IsNotNull(actual);

        }
    }
}
