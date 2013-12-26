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
            string path = @"C:\Users\rglos\AppData\Roaming\wargaming.net\WorldOfTanks\battle_results\KJFF6OZRGYYDMNI=\78091706649274713.dat";
            var target = new BattleResultsWatcher();

            // Act
            target.ProcessFile(path);

            // Assert
            //??
        }
    }
}
