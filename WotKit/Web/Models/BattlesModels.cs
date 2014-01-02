using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class BattlesIndexModel
    {
        public BattlesIndexModel()
        {
            Rows = new List<BattlesIndexRowModel>();
        }
        public List<BattlesIndexRowModel> Rows { get; set; }
    }

    public class BattlesIndexRowModel
    {
        public string Player { get; set; }
        public int RecentBattlesCount { get; set; }
    }

    public class RecentBattlesModel
    {
        public RecentBattlesModel()
        {
            Summary = new RecentBattleSummary();
            DisplayBattles = true;
        }
        public int AccountDBID { get; set; }
        public string Name { get; set; }
        public IList<RecentBattleModel> Battles { get; set; }
        public RecentBattleSummary Summary { get; set; }
        public bool DisplayBattles { get; set; }
        public string Message { get; set; }
    }

    public class RecentBattleModel
    {
        public DateTime BattleDate { get; set; }
        public int Tier { get; set; }
        public string Tank { get; set; }
        public string Map { get; set; }
        public int DamageDealt { get; set; }
        public int DamageReceived { get; set; }
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int XP { get; set; }
        public int XPPenalty { get; set; }
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int tmenXP { get; set; }
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int freeXP { get; set; }
        public int eventXP { get; set; }
        public int eventTMenXP { get; set; }
        public int eventFreeXP { get; set; }
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int credits { get; set; }
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int autoRepairCost { get; set; }
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int autoEquipCost { get; set; }
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int autoLoadCost { get; set; }
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int NetCredits { get; set; }
        public bool Won { get; set; }
        public string Status { get; set; }
        public string StatusClass { get; set; }
        public string StatusTextClass { get; set; }
        public string NetCreditsClass { get; set; }
        public int WinnerTeam { get; set; }
        public int Kills { get; set; }
        public int Spotted { get; set; }
        public int CapturePoints { get; set; }
        public int DroppedCapturePoints { get; set; }
    }

    public class RecentBattleSummary
    {
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int NetCredits { get; set; }
    }
}