using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class ClanModel
    {
        public int ClanId { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public string EmblemLarge { get; set; }
        public string EmblemSmall { get; set; }
        public string EmblemBWTank { get; set; }
        public string EmblemMedium { get; set; }
        public DateTime CreatedAtDate { get; set; }
        public DateTime UpdatedAtDate { get; set; }
    }

    public class ClanDetailModel
    {
        public ClanDetailModel()
        {
            ClanMembers = new List<ClanMemberDetailModel>();
        }

        public int ClanId { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public string EmblemSmall { get; set; }

        public DateTime AsOfDate { get; set; }

        public List<ClanMemberDetailModel> ClanMembers { get; set; }
    }

    public class ClanMemberDetailModel
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public int ClanBattles { get; set; }
        public int AllBattles { get; set; }
        public int CompanyBattles { get; set; }
    }
}