using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Web.Services;

namespace Web.Tests.Services
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

        [TestMethod]
        public void Exercise_GetClanDetails()
        {
            // Arrange
            var target = new WargamingApiService();
            var clanId = 1000000017; // RELIC

            // Act
            var actual = target.GetClanDetails(clanId);

            // Assert
            Assert.AreEqual("ok", actual.Status);
            Assert.IsTrue(actual.Count > 0);
            Assert.AreEqual(actual.Count, actual.Members.Count);
        }

        [TestMethod]
        public void Exercise_GetPlayerPersonalData()
        {
            // Arrange
            var target = new WargamingApiService();
            var accountId = 1001268969; // RJ_

            // Act
            var actual = target.GetPlayerPersonalData(accountId);

            // Assert
            Assert.AreEqual("ok", actual.Status);

        }
    }
}
