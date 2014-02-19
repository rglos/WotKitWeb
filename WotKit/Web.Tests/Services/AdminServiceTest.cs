using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Web.Services;

namespace Web.Tests.Services
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
            var wnEfficiencyService = new WNEfficiencyService();
            var expectedTankValues = wnEfficiencyService.GetExpectedTankValues();
            var target = new AdminService();

            // Act
            var actual = target.UpsertVehicles(listOfVehicles, expectedTankValues);

            // Assert
            Assert.IsNotNull(actual);
            var total = actual.VehiclesInserted + actual.VehiclesUpdated;
            Assert.AreEqual(total, actual.Total);
            // TODO: We should check the database to ensure there are tanks in the tank table
        }
    }
}
