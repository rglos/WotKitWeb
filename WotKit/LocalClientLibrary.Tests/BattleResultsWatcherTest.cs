using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LocalClientLibrary.Tests
{
    [TestClass]
    public class BattleResultsWatcherTest
    {
        [TestMethod]
        public void Exercise_ProcessFile()
        {
            // Arrange
            string path = @"C:\Users\rglos\AppData\Roaming\Wargaming.net\WorldOfTanks\battle_results\KJFF6OZRGYYDMNQ=\1307999318362707.dat";
            var target = new BattleResultsWatcher();

            // Act
            target.ProcessFile(path);

            // Assert
            //??
        }
    }
}
