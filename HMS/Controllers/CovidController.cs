using HMS.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;

namespace HMS.Controllers
{
    public class CovidController : Controller
    {
         public async Task<IActionResult> Index()
         {
             List<CoronaStat> coronaStats;
             try
             {
                 coronaStats = await GetCoronaStats();
             }
             catch (Exception e)
             {
                 Log.Error("Too Many Requests");
                 return View();

             }
            return View(coronaStats);
        }

        private async Task<List<CoronaStat>> GetCoronaStats()
        {
            string body;
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://covid-19-tracking.p.rapidapi.com/v1"),
                Headers =
                {
                    { "X-RapidAPI-Key", "f100cd0d6bmsh68b63cc9239197bp1ebdccjsndd10776ebb7e" },
                    { "X-RapidAPI-Host", "covid-19-tracking.p.rapidapi.com" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                body = await response.Content.ReadAsStringAsync();
            }

            return JsonConvert.DeserializeObject<List<CoronaStat>>(body);
        }
    }
}