using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Web.Controllers;
using System.Net.Http;
using System.IO;

namespace Web.Tests.Controllers
{
    [TestClass]
    public class BattleResultFileControllerTest
    {
        [TestMethod]
        public void Exercise_PostBattleResultFileData()
        {
            // Arrange
            var target = new BattleResultFileController();
            var requestContent = new MultipartFormDataContent();
            var filepath = @"C:\Users\rglos\AppData\Roaming\Wargaming.net\WorldOfTanks\battle_results\KJFF6OZRGYYDOMQ=\4821677704077969.dat";
            var name = Path.GetFileNameWithoutExtension(filepath);
            var fileName = Path.GetFileName(filepath);
            requestContent.Add(new StreamContent(new FileStream(filepath, FileMode.Open)), name, fileName);
            target.Request = new HttpRequestMessage(HttpMethod.Post, "http://server.com/foos");
            target.Request.Content = requestContent;
            target.Configuration = new System.Web.Http.HttpConfiguration(new System.Web.Http.HttpRouteCollection());


            // Act
            var actual = target.PostBattleResultFileData().Result;

            // Assert
            //var test = actual.Content.ReadAsStringAsync().Result;
            Assert.AreEqual(true, actual.IsSuccessStatusCode);
            //System.Diagnostics.Trace.WriteLine(test);
        }

        [TestMethod]
        public void Exercise_PostMultipleBattleResults()
        {
            // Arrange
            var target = new BattleResultFileController();
            var requestContent = new MultipartFormDataContent();
            var files = Directory.GetFiles(@"D:\Data\WoT\BattleResults");
            foreach (var file in files)
            {
                var filepath = file;
                var name = Path.GetFileNameWithoutExtension(filepath);
                var fileName = Path.GetFileName(filepath);
                requestContent.Add(new StreamContent(new FileStream(filepath, FileMode.Open)), name, fileName);
            }
            target.Request = new HttpRequestMessage(HttpMethod.Post, "http://server.com/foos");
            target.Request.Content = requestContent;
            target.Configuration = new System.Web.Http.HttpConfiguration(new System.Web.Http.HttpRouteCollection());

            // Act
            var actual = target.PostBattleResultFileData().Result;

            // Assert
            Assert.AreEqual(true, actual.IsSuccessStatusCode);
        }
    }
}
