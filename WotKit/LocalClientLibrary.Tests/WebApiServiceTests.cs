using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LocalClientLibrary.Tests
{
    [TestClass]
    public class WebApiServiceTests
    {
        [TestMethod]
        public void Exercise_PostPlayer()
        {
            // Arrange
            var player = new Web.Domain.Models.Player() { AccountDBID = 123456789, Name = "ElmerFudd" };

            // Act
            var actual = WebApiService.PostPlayer(player).Result;

            // Assert
            Assert.AreEqual(player.AccountDBID, actual.AccountDBID);
            Assert.AreEqual(player.Name, actual.Name);
            Assert.AreNotEqual(0, actual.PlayerId);

            // TODO: Cleanup the record out of the database?  Or will we just recreate the database prior to each test
        }

        [TestMethod]
        public void Exercise_PostBattle()
        {
            // Arrange
            var battle = new Web.Domain.Models.Battle() { ArenaCreateTime = DateTime.Now, ArenaTypeIcon = "123", ArenaTypeId = 0, ArenaTypeName = "xxxyyyzzz", ArenaUniqueId = 62985022473974194, BonusType = 1, BonusTypeName = "regular", Duration = 275.80000000001746, FinishReason = 1, FinishReasonName = "extermination", GameplayId = 0, GameplayName = "ctf", ParserVersion = "0.8.10.0", Result = "ok", VehLockMode = 0, WinnerTeam = 2 };

            // Act
            var actual = WebApiService.PostBattle(battle).Result;

            // Assert
            Assert.Inconclusive();
        }
    }
}
