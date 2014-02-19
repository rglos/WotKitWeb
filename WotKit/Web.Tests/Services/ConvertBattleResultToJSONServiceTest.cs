using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Web.Services;

namespace Web.Tests.Services
{
    [TestClass]
    public class ConvertBattleResultToJSONServiceTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var target = new ConvertBattleResultToJSONService();

            var actual = ConvertBattleResultToJSONService.Convert(@"D:\Data\WoT\Temp\78942037161538862.dat");

            Assert.IsNotNull(actual);
        }
    }
}
