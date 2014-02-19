using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Web.Controllers;

namespace Web.Tests.Controllers
{
    [TestClass]
    public class BattleControllerTest
    {
        [TestMethod]
        public void Exercise_PostBattle()
        {
            // Arrange
            var battle = new Web.Domain.Models.Battle() { ArenaCreateTime = DateTime.Now, ArenaTypeIcon = "123", ArenaTypeId = 0, ArenaTypeName = "xxxyyyzzz", ArenaUniqueId = 62985022473974194, BonusType = 1, BonusTypeName = "regular", Duration = 275.80000000001746, FinishReason = 1, FinishReasonName = "extermination", GameplayId = 0, GameplayName = "ctf", ParserVersion = "0.8.10.0", Result = "ok", VehLockMode = 0, WinnerTeam = 2 };
            var controller = new BattleController();

            // Act
            var actual = controller.PostBattle(battle);

            // Assert
            Assert.Inconclusive();
        }
    }
}
