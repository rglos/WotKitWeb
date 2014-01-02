using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Services
{
    public class AdminService
    {
        public UpsertVehiclesStatus UpsertVehicles(Web.Services.WargamingApiService.ListOfVehicles listOfVehicles)
        {
            var upsertVehiclesStatus = new UpsertVehiclesStatus();

            foreach (var tank in listOfVehicles.Tanks)
            {
                // TODO: Insert them into a table
            }

            return upsertVehiclesStatus;
        }

        public class UpsertVehiclesStatus
        {
            public int VehiclesUpdated { get; set; }
            public int VehiclesInserted { get; set; }
            public int VehiclesWithNoUpdates { get; set; }
            public int Total { get; set; }
        }
    }
}