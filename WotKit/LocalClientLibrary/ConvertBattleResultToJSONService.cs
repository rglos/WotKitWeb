using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LocalClientLibrary
{
    public class ConvertBattleResultToJSONService
    {
        public static string Convert(string path)
        {
            var fileInfo = new FileInfo(path);

            // this bit of code taken from here: http://forum.worldoftanks.eu/index.php?/topic/156599-convert-battle-results-to-json/page__pid__2921060#entry2921060
            // converted to C# here: http://converter.telerik.com/
            // and manually entered by me
            var webClient = new WebClient();
            System.Net.ServicePointManager.Expect100Continue = false;
            webClient.Headers.Clear();
            webClient.Headers.Add("Content-Type", "application/octet-stream");
            webClient.Headers.Add("Referer", fileInfo.Name);
            webClient.Headers.Add("User-Agent", "WoTKit " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());
            webClient.Proxy.Credentials = CredentialCache.DefaultCredentials;

            var uri = new Uri("http://www.vbaddict.net/br2json.php");
            var uploadFileResult = webClient.UploadFile(uri, "PUT", fileInfo.FullName);
            var resultAsString = System.Text.Encoding.Default.GetString(uploadFileResult);

            return resultAsString;
        }
    }
}
