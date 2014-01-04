using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Web.Services;

namespace Web.Tests.Controllers
{
    [TestClass]
    public class ConvertBattleResultToJSONServiceTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var target = new ConvertBattleResultToJSONService();

            var actual = ConvertBattleResultToJSONService.Convert(@"D:\Data\WoT\Temp\56453343585227702.dat");

            Assert.IsNotNull(actual);
        }
    }
}
