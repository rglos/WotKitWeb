using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LocalClientLibrary.Tests
{
    [TestClass]
    public class BattleResultsParserTests
    {
        [TestMethod]
        public void Exercise_ParsePlayer()
        {
            // Arrange
            var json = ResourceService.GetStringFromResources("LocalClientLibrary.Tests.BattleResult.json");

            // Act
            var actual = BattleResultsParser.ParsePlayer(json);

            // Assert
            Assert.AreEqual("RJ_", actual.Name);
            Assert.AreEqual(1001268969, actual.AccountDBID);
            Assert.AreEqual(0, actual.PlayerId);
        }

        [TestMethod]
        public void Exercise_ParseBattle()
        {
            // Arrange
            var json = ResourceService.GetStringFromResources("LocalClientLibrary.Tests.BattleResult.json");

            // Act
            var actual = BattleResultsParser.ParseBattle(json);

            // Assert
            Assert.Inconclusive("Lazy and I didn't bother to check the fields - needs to be done yet");
        }

        [TestMethod]
        public void Exercise_ParsePlayerBattle()
        {
            // Arrange
            var json = ResourceService.GetStringFromResources("LocalClientLibrary.Tests.BattleResult.json");

            // Act
            var actual = BattleResultsParser.ParsePlayerBattle(json, new Web.Domain.Models.Player() { PlayerId = -1 }, new Web.Domain.Models.Battle() { BattleId = -2 });

            // Assert
            Assert.AreEqual(-1, actual.PlayerId);
            Assert.AreEqual(-2, actual.BattleId);

        }
    }
}
