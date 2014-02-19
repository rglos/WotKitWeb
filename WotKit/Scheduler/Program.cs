using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Services;

namespace Scheduler
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO: Call methods in Web to run at the command prompt so that they can be scheduled

            var clanId = 1000000017; // RELIC
            var wargamingApiService = new WargamingApiService();
            var target = new LoadClanDetailService(wargamingApiService);
            target.LoadClanDetails(clanId);
        }
    }
}
