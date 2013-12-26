using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class RecentBattlesModel
    {
        public int AccountDBID { get; set; }
        public string Name { get; set; }
        public IList<RecentBattleModel> Battles { get; set; }
    }

    public class RecentBattleModel
    {
        public DateTime BattleDate { get; set; }
        public int Tier { get; set; }
        public string Tank { get; set; }
        public int DamageDealt { get; set; }
        public int DamageReceived { get; set; }
    }
}