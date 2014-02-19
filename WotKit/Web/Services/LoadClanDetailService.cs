using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Domain.Models;

namespace Web.Services
{
    public class LoadClanDetailService
    {
        private readonly IWargamingApiService _wargamingApiService;

        public LoadClanDetailService(IWargamingApiService wargamingApiService)
        {
            this._wargamingApiService = wargamingApiService;
        }

        public void LoadClanDetails(int clanId)
        {
            using (WotKitEntities db = new WotKitEntities())
            {
                // Get the clan and load it
                var clan = _wargamingApiService.GetClanDetails(clanId);

                // TODO: var clanInDb = Upsert(clan); // Insert or update the clan
                var clanInDb = db.Clans.Where(x => x.ClanId == clanId).SingleOrDefault();
                if (clanInDb == null)
                {
                    // Add the clan
                    clanInDb = new Clan()
                    {
                        ClanId = clanId,
                        Abbreviation = clan.Abbreviation,
                        Name = clan.Name,
                        EmblemBWTank = clan.EmblemBWTank,
                        EmblemLarge = clan.EmblemLarge,
                        EmblemMedium = clan.EmblemMedium,
                        EmblemSmall = clan.EmblemSmall,
                        CreatedAtDate = clan.CreatedAtDate,
                        UpdatedAtDate = clan.UpdatedAtDate
                    };
                    db.Clans.Add(clanInDb);
                    db.SaveChanges();
                }
                else
                {
                    // Update the clan?
                    clanInDb.UpdatedAtDate = clan.UpdatedAtDate;
                }

                // Now using the members load them
                DateTime now = DateTime.UtcNow;
                foreach (var member in clan.Members)
                {
                    // Is this dude even in the database?
                    var player = db.Players.Where(x => x.AccountDBID == member.AccountId).SingleOrDefault();
                    if (player == null)
                    {
                        // Add em
                        player = new Player() {
                            AccountDBID = member.AccountId,
                            Name = member.AccountName,
                        };
                        db.Players.Add(player);
                    }
                    db.SaveChanges();

                    // Go get details for this player
                    var playerPersonalData = _wargamingApiService.GetPlayerPersonalData(player.AccountDBID);

                    // Now we add a record for this batch taking a count of their battles
                    db.ClanDetails.Add(new ClanDetail()
                    {
                        AsOfDate = now,
                        AllBattles = playerPersonalData.All.Battles,
                        ClanId = clanInDb.ClanId,
                        ClanBattles = playerPersonalData.Clan.Battles,
                        PlayerId = player.PlayerId,
                        CompanyBattles = playerPersonalData.Company.Battles,
                        Role = member.Role,
                        Rolei18n = member.Rolei18n
                    });
                }
                db.SaveChanges();
            }
        }
    }
}