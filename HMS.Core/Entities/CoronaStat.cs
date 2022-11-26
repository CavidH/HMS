using Newtonsoft.Json;

namespace HMS.Core.Entities;

public class CoronaStat
{
    [JsonProperty("Active Cases_text")]
    public string ActiveCases_text { get; set; }
    public string Country_text { get; set; }

    [JsonProperty("Last Update")]
    public string LastUpdate { get; set; }

    [JsonProperty("New Cases_text")]
    public string NewCases_text { get; set; }

    [JsonProperty("New Deaths_text")]
    public string NewDeaths_text { get; set; }

    [JsonProperty("Total Cases_text")]
    public string TotalCases_text { get; set; }

    [JsonProperty("Total Deaths_text")]
    public string TotalDeaths_text { get; set; }

    [JsonProperty("Total Recovered_text")]
    public string TotalRecovered_text { get; set; }
}