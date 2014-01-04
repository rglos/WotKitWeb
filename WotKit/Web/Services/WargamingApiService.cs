using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Web.Services
{
    public class WargamingApiService
    {
        // TODO: put this in the app config and push it on at deployment time instead of storing it in source
        private const string ApplicationId = "435e719eb132e07707821b7ca489e38f";

        public ListOfVehicles GetListOfVehicles()
        {
            var listOfVehicles = new ListOfVehicles();

            var url = string.Format(@"https://api.worldoftanks.com/wot/encyclopedia/tanks/?application_id={0}", ApplicationId);
            using (var webClient = new WebClient())
            {
                var json = webClient.DownloadString(url);
                JObject jobject = JObject.Parse(json);
                listOfVehicles.Status = (string)jobject["status"];
                listOfVehicles.Count = (int)jobject["count"];

                var data = (JObject)jobject["data"];
                foreach (var dataEntry in data.Children())
                {
                    var dataEntryJProperty = dataEntry as JProperty;
                    var tankId = dataEntryJProperty.Name;

                    var tankdata = dataEntryJProperty.Value as JObject;

                    var nationi18n = (string)tankdata["nation_i18n"];
                    var name = (string)tankdata["name"];
                    var level = (int)tankdata["level"];
                    var nation = (string)tankdata["nation"];
                    var isPremium = (bool)tankdata["is_premium"];
                    var namei18n = (string)tankdata["name_i18n"];
                    var tankType = (string)tankdata["type"];
                    var tankDataId = (int)tankdata["tank_id"];

                    // name is a concantination of nation and tank ala: "#ussr_vehicles:T-34"
                    // so we want just the stuff on the right of the colon

                    var tankNameOnly = name.Split(':')[1];

                    listOfVehicles.Tanks.Add(new Tank() { 
                        IsPremium = isPremium,
                        Level = level,
                        Name = tankNameOnly,
                        Namei18n = namei18n,
                        Nation = nation,
                        Nationi18n = nationi18n,
                        TankId = tankDataId,
                        TankType = tankType
                    });
                }
            }

            return listOfVehicles;
        }

        public class ListOfVehicles
        {
            public ListOfVehicles()
            {
                Tanks = new List<Tank>();
            }
            public string Status { get; set; }
            public int Count { get; set; }
            public List<Tank> Tanks { get; set; }
        }

        public class Tank
        {
            public int TankId { get; set; }
            public string Nation { get; set; }
            public string Name { get; set; }
            public int Level { get; set; }
            public bool IsPremium { get; set; }
            public string Namei18n { get; set; }
            public string Nationi18n { get; set; }
            public string TankType { get; set; }
        }
    }
}