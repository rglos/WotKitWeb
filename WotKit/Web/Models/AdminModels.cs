using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class TanksIndexModel
    {
        public TanksIndexModel()
        {
            Tanks = new List<TanksIndexRowModel>();
        }
        public List<TanksIndexRowModel> Tanks { get; set; }
    }

    public class TanksIndexRowModel
    {
        public int TankId { get; set; }
        public int Level { get; set; }
        public string Name { get; set; }
        public string Nation { get; set; }
        public string TankType { get; set; }
    }
}