using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Web.Services
{
    public interface IWargamingApiService
    {
        WargamingApiService.ClanDetails GetClanDetails(int clanId);
        WargamingApiService.ListOfVehicles GetListOfVehicles();
        WargamingApiService.PlayerPersonalData GetPlayerPersonalData(int accountId);
    }

    public class WargamingApiService : IWargamingApiService
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

        public ClanDetails GetClanDetails(int clanId)
        {
            var clanDetails = new ClanDetails();

            var url = string.Format(@"https://api.worldoftanks.com/wot/clan/info/?application_id={0}&clan_id={1}", ApplicationId, clanId);
            using (var webClient = new WebClient())
            {
                var json = webClient.DownloadString(url);
                JObject jobject = JObject.Parse(json);
                clanDetails.Status = (string)jobject["status"];
                
                var data = (JObject)jobject["data"];
                var entityData = data[clanId.ToString()];
                var members = entityData["members"];

                clanDetails.Count = (int)entityData["members_count"];
                clanDetails.Abbreviation = (string)entityData["abbreviation"];
                clanDetails.Name = (string)entityData["name"];
                clanDetails.EmblemBWTank = (string)entityData["emblems"]["bw_tank"];
                clanDetails.EmblemLarge = (string)entityData["emblems"]["large"];
                clanDetails.EmblemMedium = (string)entityData["emblems"]["medium"];
                clanDetails.EmblemSmall = (string)entityData["emblems"]["small"];

                UpdateAuditProperties(clanDetails, entityData);

                foreach (var dataEntry in members.Children())
                {
                    var dataEntryJProperty = dataEntry as JProperty;
                    var accountId = dataEntryJProperty.Name;

                    var accountData = dataEntryJProperty.Value as JObject;

                    var accountDataAccountId = (int)accountData["account_id"];
                    var createdAt = (long)accountData["created_at"];
                    var updatedAt = (long)accountData["updated_at"];
                    var accountName = (string)accountData["account_name"];
                    var role = (string)accountData["role"];
                    var rolei18n = (string)accountData["role_i18n"];

                    var member = new Member();
                    member.AccountId = accountDataAccountId;
                    member.AccountName = accountName;
                    member.Role = role;
                    member.Rolei18n = rolei18n;

                    UpdateAuditProperties(member, accountData);

                    clanDetails.Members.Add(member);
                }
            }

            return clanDetails;
        }

        public PlayerPersonalData GetPlayerPersonalData(int accountId)
        {
            var playerPersonalData = new PlayerPersonalData();

            var url = string.Format(@"https://api.worldoftanks.com/wot/account/info/?application_id={0}&account_id={1}", ApplicationId, accountId);
            using (var webClient = new WebClient())
            {
                var json = webClient.DownloadString(url);
                JObject jobject = JObject.Parse(json);
                playerPersonalData.Status = (string)jobject["status"];

                var data = (JObject)jobject["data"];
                var dataEntity = data[accountId.ToString()];

                playerPersonalData.Clan = GetPlayerStatistics(dataEntity["statistics"]["clan"]);
                playerPersonalData.All = GetPlayerStatistics(dataEntity["statistics"]["all"]);
                playerPersonalData.Company = GetPlayerStatistics(dataEntity["statistics"]["company"]);

                UpdateAuditProperties(playerPersonalData, dataEntity);
            }

            return playerPersonalData;
        }

        // This helper is to populate the CreatedAt, CreatedAtDate, UpdatedAt, and UpdatedAtDate that seem to be hanging off many of the entities from the API
        private void UpdateAuditProperties(AuditObject auditObject, JToken dataEntity)
        {
            auditObject.CreatedAt = (long)dataEntity["created_at"];
            auditObject.CreatedAtDate = Utils.ConvertUnixTime(auditObject.CreatedAt);
            auditObject.UpdatedAt = (long)dataEntity["updated_at"];
            auditObject.UpdatedAtDate = Utils.ConvertUnixTime(auditObject.UpdatedAt);
        }

        // There's a lot more statistics available but since we are focusing on who plays clan wars, I'm just grabbing battles for now.
        private PlayerStatistics GetPlayerStatistics(JToken jToken)
        {
            var playerStatistics = new PlayerStatistics();

            var battles = (int)jToken["battles"];

            playerStatistics.Battles = battles;

            return playerStatistics;
        }

        public class PlayerPersonalData : AuditObject
        {
            public string Status { get; set; }
            public PlayerStatistics Clan { get; set; }
            public PlayerStatistics All { get; set; }
            public PlayerStatistics Company { get; set; }
        }

        public class PlayerStatistics
        {
            public int Battles { get; set; }
        }

        public class ClanDetails : AuditObject
        {
            public ClanDetails()
            {
                Members = new List<Member>();
            }
            public string Status { get; set; }
            public int Count { get; set; }
            public string Abbreviation { get; set; }
            public string Name { get; set; }
            public string EmblemLarge { get; set; }
            public string EmblemSmall { get; set; }
            public string EmblemBWTank { get; set; }
            public string EmblemMedium { get; set; }
            public List<Member> Members { get; set; }
        }

        public class Member : AuditObject
        {
            public int AccountId { get; set; }
            public string AccountName { get; set; }
            public string Role { get; set; }
            public string Rolei18n { get; set; }
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

        // Since many of the wargaming entities have created_at and updated_at I assume those are some sort of audit properties - we
        // can use this base class so we don't repeat these all over.  I also added an actual DateTime representation 
        // (CreatedAtDate, UpdatedAtDate) of those long dataype values.  Use the Utils.ConvertUnixTime to get at UTC datetime.
        public class AuditObject
        {
            public long CreatedAt { get; set; }
            public DateTime CreatedAtDate { get; set; }
            public long UpdatedAt { get; set; }
            public DateTime UpdatedAtDate { get; set; }
        }
    }
}