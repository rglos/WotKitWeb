using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class WN8ExpectedTankValuesModel
    {
        public WN8ExpectedTankValuesModel()
        {
            Rows = new List<WN8ExpectedTankValuesRowModel>();
        }
        public List<WN8ExpectedTankValuesRowModel> Rows { get; set; }
    }

    public class WN8ExpectedTankValuesRowModel
    {
        public string Name { get; set; }
        public double Frag { get; set; }
        public int Damage { get; set; }
        public double Spot { get; set; }
        public double Defense { get; set; }
        public double Win { get; set; }
        public int Tier { get; set; }
        public string Nation { get; set; }
        public string Class { get; set; }
        public int IDNum { get; set; }
    }
}