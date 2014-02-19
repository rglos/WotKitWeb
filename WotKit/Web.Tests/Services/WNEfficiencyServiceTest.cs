using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Web.Services;

namespace Web.Tests.Services
{
    [TestClass]
    public class WNEfficiencyServiceTest
    {
        [TestMethod]
        public void Exercise_GetExpectedTankValues()
        {
            // Arrange
            var target = new WNEfficiencyService();

            // Act
            var actual = target.GetExpectedTankValues();

            // Assert
            Assert.AreNotEqual(0, actual.Version);
            Assert.AreNotEqual(0, actual.Values.Count);
        }
    }
}
