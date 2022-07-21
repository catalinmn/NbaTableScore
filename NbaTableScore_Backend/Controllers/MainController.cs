using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NbaTableScore_Backend.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Net;

namespace NbaTableScore_Backend.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MainController(IHttpClientFactory httpClientFactory) =>      
            _httpClientFactory = httpClientFactory;
        
        [HttpGet]
        [Route("GetGames")]
        public async Task<string> GetGames()
        {
            var years = new List<string> { "2017", "2018", "2019" };
            var random = new Random();
            var randomYear = years[random.Next(years.Count)];

            var httpClient = _httpClientFactory.CreateClient("API");

            httpClient.BaseAddress = new Uri(httpClient.BaseAddress + $"?seasons[]={randomYear}&per_page=100");

            HttpResponseMessage response = await httpClient.GetAsync(httpClient.BaseAddress + $"?seasons[]={randomYear}&per_page=100");

            string outResponse = await response.Content.ReadAsStringAsync();

            try
            {
                var jsonResponse = JObject.Parse(outResponse);

                #region snippet
                /* int totalPages = Convert.ToInt32(jsonResponse["meta"]["total_pages"]);
                 int currentPage = Convert.ToInt32(jsonResponse["meta"]["current_page"]);
                 int pagesToGet = 5;

                 JArray listofResponses = new();
                 listofResponses.Add(jsonResponse["data"].Children());

                 while (currentPage <= totalPages && currentPage <= pagesToGet)
                 {       
                     response = await httpClient.GetAsync(httpClient.BaseAddress + $"&page={currentPage++}");
                     outResponse = await response.Content.ReadAsStringAsync();

                     jsonResponse = JObject.Parse(outResponse);

                     listofResponses.Concat(jsonResponse["data"]);
                 }*/
                #endregion

                List<Game> games = new();

                foreach (var game in jsonResponse["data"])
                {
                    string id = game["id"].ToString();

                    string date = game["date"].ToString();
                    var dateParser = DateTime.ParseExact(date, "dd-MMM-yy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    date = dateParser.ToString("dd-MM-yyyy", null);

                    string season = game["season"].ToString();
                    string homeTeam = game["home_team"]["full_name"].ToString();
                    string visitorTeam = game["visitor_team"]["full_name"].ToString();
                    string homeTeamScore = game["home_team_score"].ToString();
                    string visitorTeamScore = game["visitor_team_score"].ToString();

                    string homeTeamAbv = game["home_team"]["abbreviation"].ToString();
                    string visitorTeamAbv = game["visitor_team"]["abbreviation"].ToString();

                    var homeTeamLogo = $"http://i.cdn.turner.com/nba/nba/.element/img/1.0/teamsites/logos/teamlogos_500x500/{homeTeamAbv.ToLower()}.png";
                    var visitorTeamLogo = $"http://i.cdn.turner.com/nba/nba/.element/img/1.0/teamsites/logos/teamlogos_500x500/{visitorTeamAbv.ToLower()}.png";

                    games.Add(new Game(id, date, season, homeTeam, visitorTeam, homeTeamScore, visitorTeamScore, homeTeamLogo, visitorTeamLogo));
                }            

                games = games.OrderBy(x => x.HomeTeam).ThenByDescending(x => Convert.ToInt32(x.HomeTeamScore)).Take(10).ToList();

                var jsonGames = JsonConvert.SerializeObject(games);

                return jsonGames.ToString();
            }
            catch(Exception ex)
            {
                JObject jObject = new JObject()
                {
                    ["Message"] = ex.ToString(),
                };

                return jObject.ToString();
            }

            #region snippet
            /*  using (var client = new HttpClient())
              {
                  client.BaseAddress = new Uri("https://free-nba.p.rapidapi.com/players");
                  client.DefaultRequestHeaders.Add("X-RapidAPI-Key", "335aabf663msh43452f55e991660p143e3djsn1d663ce75b3c");
                  client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "free-nba.p.rapidapi.com");

                  var response = await client.GetAsync(client.BaseAddress);

                  string outResponse = await response.Content.ReadAsStringAsync();

                  return response;
              }*/
            #endregion
        }

    }
}
