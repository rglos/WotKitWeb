using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Services
{
    public class BattleResultsParser
    {
        private const string Personal = "personal";

        public static Web.Domain.Models.Player ParsePlayer(string json)
        {
            JObject jobject = JObject.Parse(json);
            var personal = (JObject)jobject[Personal];
            var accountDBID = (int)personal["accountDBID"];
            var name = (string)jobject["players"][accountDBID.ToString()]["name"];

            return new Web.Domain.Models.Player()
            {
                AccountDBID = accountDBID,
                Name = name
            };
        }

        public static Web.Domain.Models.Battle ParseBattle(string json)
        {
            JObject jobject = JObject.Parse(json);
            var arenaUniqueId = (long)jobject["arenaUniqueID"];
            var parserTime = (long)jobject["parsertime"];
            var parserDateTime = convertUnixTime(parserTime);
            var parserversion = (string)jobject["parserversion"];

            var common = (JObject)jobject["common"];
            var arenaCreateTime = convertUnixTime((long)common["arenaCreateTime"]);
            var arenaTypeID = (int)common["arenaTypeID"];
            var arenaTypeIcon = (string)common["arenaTypeIcon"];
            var arenaTypeName = (string)common["arenaTypeName"];
            var bonusType = (int)common["bonusType"];
            var bonusTypeName = (string)common["bonusTypeName"];
            var duration = (double)common["duration"];
            var finishReason = (int)common["finishReason"];
            var finishReasonName = (string)common["finishReasonName"];
            var gameplayID = (int)common["gameplayID"];
            var gameplayName = (string)common["gameplayName"];
            var result = (string)common["result"];
            var vehLockMode = (int)common["vehLockMode"];
            var winnerTeam = (int)common["winnerTeam"];

            return new Web.Domain.Models.Battle()
            {
                ArenaUniqueId = arenaUniqueId,
                ArenaCreateTime = arenaCreateTime,
                ArenaTypeIcon = arenaTypeIcon,
                ArenaTypeName = arenaTypeName,
                ArenaTypeId = arenaTypeID,
                BonusType = bonusType,
                BonusTypeName = bonusTypeName,
                Duration = duration,
                FinishReason = finishReason,
                FinishReasonName = finishReasonName,
                GameplayId = gameplayID,
                GameplayName = gameplayName,
                Result = result,
                VehLockMode = vehLockMode,
                WinnerTeam = winnerTeam,
                ParserVersion = parserversion
            };
        }

        public static Web.Domain.Models.PlayerBattle ParsePlayerBattle(string json, Web.Domain.Models.Player player, Web.Domain.Models.Battle battle)
        {
            JObject jobject = JObject.Parse(json);

            var damageDealt = (int)jobject[Personal]["damageDealt"];
            var damageReceived = (int)jobject[Personal]["damageReceived"];
            var tankName = (string)jobject[Personal]["tankName"];
            // var tankId = (string)jobject[Personal]["tankId"]; // this tankId doesn't match the tank_id from the WoT API... because its typeCompDescr
            var typeCompDescr = (int)jobject[Personal]["typeCompDescr"];
            var xp = (int)jobject[Personal]["xp"];
            var xpPenalty = (int)jobject[Personal]["xpPenalty"];
            var tmenXP = (int)jobject[Personal]["tmenXP"];
            var freeXP = (int)jobject[Personal]["freeXP"];
            var eventXP = (int)jobject[Personal]["eventXP"];
            var eventTMenXP = (int)jobject[Personal]["eventTMenXP"];
            var eventFreeXP = (int)jobject[Personal]["eventFreeXP"];
            var credits = (int)jobject[Personal]["credits"];
            var autoRepairCost = (int)jobject[Personal]["autoRepairCost"];
            var autoLoadCost = (int)jobject[Personal]["autoLoadCost"][0];
            var autoEquipCost = (int)jobject[Personal]["autoEquipCost"][0];
            var won = (bool)jobject[Personal]["won"];
            var kills = (int)jobject[Personal]["kills"];
            var spotted = (int)jobject[Personal]["spotted"];
            var capturePoints = (int)jobject[Personal]["capturePoints"];
            var droppedCapturePoints = (int)jobject[Personal]["droppedCapturePoints"];

            return new Web.Domain.Models.PlayerBattle()
            {
                BattleId = battle.BattleId,
                PlayerId = player.PlayerId,
                DamageDealt = damageDealt,
                DamageReceived = damageReceived,
                TankName = tankName,
                XP = xp,
                XPPenalty = xpPenalty,
                tmenXP = tmenXP,
                freeXP = freeXP,
                eventXP = eventXP,
                eventTMenXP = eventTMenXP,
                eventFreeXP = eventFreeXP,
                credits = credits,
                autoEquipCost = autoEquipCost,
                autoLoadCost = autoLoadCost,
                autoRepairCost = autoRepairCost,
                won = won,
                kills = kills,
                spotted = spotted,
                capturePoints = capturePoints,
                droppedCapturePoints = droppedCapturePoints,
                typeCompDescr = typeCompDescr
            };
        }

        // http://code.google.com/p/wotstats/source/browse/trunk/WoTStats/WoTStats/Objects/BattleResultsObject.cs
        private static DateTime convertUnixTime(long time)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(time);
        }
    }
}