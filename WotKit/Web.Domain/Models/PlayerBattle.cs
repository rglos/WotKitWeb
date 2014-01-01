//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Web.Domain.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PlayerBattle
    {
        public int PlayerId { get; set; }
        public int BattleId { get; set; }
        public int DamageDealt { get; set; }
        public int DamageReceived { get; set; }
        public string TankName { get; set; }
        public int XP { get; set; }
        public int XPPenalty { get; set; }
        public int tmenXP { get; set; }
        public int freeXP { get; set; }
        public int eventXP { get; set; }
        public int eventTMenXP { get; set; }
        public int eventFreeXP { get; set; }
        public int credits { get; set; }
        public int autoRepairCost { get; set; }
        public int autoEquipCost { get; set; }
        public int autoLoadCost { get; set; }
        public bool won { get; set; }
        public int kills { get; set; }
        public int spotted { get; set; }
        public int capturePoints { get; set; }
        public int droppedCapturePoints { get; set; }
    
        public virtual Battle Battle { get; set; }
        public virtual Player Player { get; set; }
    }
}
