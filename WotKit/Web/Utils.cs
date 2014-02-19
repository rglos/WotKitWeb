using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web
{
    public static class Utils
    {
        // http://code.google.com/p/wotstats/source/browse/trunk/WoTStats/WoTStats/Objects/BattleResultsObject.cs
        public static DateTime ConvertUnixTime(long time)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(time);
        }
    }
}