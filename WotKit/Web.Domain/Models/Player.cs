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
    
    public partial class Player
    {
        public Player()
        {
            this.PlayerBattles = new HashSet<PlayerBattle>();
        }
    
        public int PlayerId { get; set; }
        public int AccountDBID { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<PlayerBattle> PlayerBattles { get; set; }
    }
}
