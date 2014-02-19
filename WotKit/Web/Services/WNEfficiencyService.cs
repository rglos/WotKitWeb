using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Web.Services
{
    public class WNEfficiencyService
    {
        private const string Url = "http://www.wnefficiency.net/exp/expected_tank_values_latest.json";

        public ExpectedTankValues GetExpectedTankValues()
        {
            var result = new ExpectedTankValues();

            using (var webClient = new WebClient())
            {
                var json = webClient.DownloadString(Url);
                JObject jobject = JObject.Parse(json);

                result.Version = (int)jobject["header"]["version"];

                var data = (JArray)jobject["data"];
                foreach (JObject dataEntry in data.Children())
                {
                    var idNum = (int)dataEntry["IDNum"];
                    var expFrag = (double)dataEntry["expFrag"];
                    var expDamage = (double)dataEntry["expDamage"];
                    var expSpot = (double)dataEntry["expSpot"];
                    var expDef = (double)dataEntry["expDef"];
                    var expWinRate = (double)dataEntry["expWinRate"];

                    result.Values.Add(new ExpectedTankValue()
                    {
                        IDNum = idNum,
                        expDamage = expDamage,
                        expDef = expDef,
                        expFrag = expFrag,
                        expSpot = expSpot,
                        expWinRate = expWinRate
                    });
                }
            }

            return result;
        }

        public class ExpectedTankValues
        {
            public ExpectedTankValues()
            {
                Values = new List<ExpectedTankValue>();
            }
            public int Version { get; set; }
            public List<ExpectedTankValue> Values { get; set; }
        }

        public class ExpectedTankValue
        {
            public int IDNum { get; set; }
            public double expFrag { get; set; }
            public double expDamage { get; set; }
            public double expSpot { get; set; }
            public double expDef { get; set; }
            public double expWinRate { get; set; }
        }
    }
}