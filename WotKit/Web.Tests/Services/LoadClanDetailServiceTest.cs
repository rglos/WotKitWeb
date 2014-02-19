using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Web.Services;

namespace Web.Tests.Services
{
    [TestClass]
    public class LoadClanDetailServiceTest
    {
        [TestMethod]
        public void Exercise_LoadClanDetails()
        {
            // Arrange
            // TODO: Add an attribute to note this is a integration test
            var clanId = 1000000017; // RELIC
            var wargamingApiService = new WargamingApiService();
            var target = new LoadClanDetailService(wargamingApiService);

            // Act
            target.LoadClanDetails(clanId);

            // Assert
            // what do we assert here?
        }
    }
}
