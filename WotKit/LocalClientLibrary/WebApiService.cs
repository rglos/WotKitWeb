using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LocalClientLibrary
{
    public class WebApiService
    {
        static HttpClient _client = new HttpClient();

        static WebApiService()
        {
            _client.BaseAddress = new Uri("http://localhost:61240/");
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static async Task<Web.Domain.Models.Player> PostPlayer(Web.Domain.Models.Player player)
        {
            var response = await _client.PostAsJsonAsync("api/player", player);
            
            response.EnsureSuccessStatusCode(); // Throw on error code

            var domainObject = await response.Content.ReadAsAsync<Web.Domain.Models.Player>();

            return domainObject;
        }

        public static async Task<Web.Domain.Models.Battle> PostBattle(Web.Domain.Models.Battle battle)
        {
            var response = await _client.PostAsJsonAsync("api/battle", battle);

            response.EnsureSuccessStatusCode(); // Throw on error code

            var domainObject = await response.Content.ReadAsAsync<Web.Domain.Models.Battle>();

            return domainObject;
        }

        public static async Task<Web.Domain.Models.PlayerBattle> PostPlayerBattle(Web.Domain.Models.PlayerBattle playerBattle)
        {
            var response = await _client.PostAsJsonAsync("api/playerbattle", playerBattle);

            response.EnsureSuccessStatusCode(); // Throw on error code

            var domainObject = await response.Content.ReadAsAsync<Web.Domain.Models.PlayerBattle>();

            return domainObject;
        }
    }
}
