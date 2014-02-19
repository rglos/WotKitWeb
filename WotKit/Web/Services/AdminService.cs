using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Domain.Models;

namespace Web.Services
{
    public class AdminService
    {
        private WotKitEntities db = new WotKitEntities();

        public UpsertVehiclesStatus UpsertVehicles(Web.Services.WargamingApiService.ListOfVehicles listOfVehicles, Web.Services.WNEfficiencyService.ExpectedTankValues expectedTankValues)
        {
            var upsertVehiclesStatus = new UpsertVehiclesStatus();

            foreach (var tank in listOfVehicles.Tanks)
            {
                // There's some duplicates in the ExpectedTankValues - they appear to have the same value so we'll just take the first
                var expectedEfficiencyValues = expectedTankValues.Values.Where(x => x.IDNum == tank.TankId).First();
                
                var tankInDb = db.Tanks.Where(x => x.TankId == tank.TankId).SingleOrDefault();
                if (tankInDb == null)
                {
                    tankInDb = new Tank() { 
                        IsPremium = tank.IsPremium,
                        Level = tank.Level,
                        Name = tank.Name,
                        Namei18n = tank.Namei18n,
                        Nation = tank.Nation,
                        Nationi18n = tank.Nationi18n,
                        TankId = tank.TankId,
                        TankType = tank.TankType,
                        ExpectedDamage = expectedEfficiencyValues.expDamage,
                        ExpectedDefense = expectedEfficiencyValues.expDef,
                        ExpectedFrag = expectedEfficiencyValues.expFrag,
                        ExpectedSpot = expectedEfficiencyValues.expSpot,
                        ExpectedWin = expectedEfficiencyValues.expWinRate
                    };
                    db.Tanks.Add(tankInDb);

                    upsertVehiclesStatus.VehiclesInserted += 1;
                }
                else
                {
                    tankInDb.Name = tank.Name;

                    upsertVehiclesStatus.VehiclesUpdated += 1;
                }                

                upsertVehiclesStatus.Total += 1;
            }

            db.SaveChanges();

            return upsertVehiclesStatus;
        }

        public class UpsertVehiclesStatus
        {
            public int VehiclesUpdated { get; set; }
            public int VehiclesInserted { get; set; }
            public int Total { get; set; }
        }
    }
}