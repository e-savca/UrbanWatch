using MongoDB.Bson;
using Newtonsoft.Json;

namespace UrbanWatch.Worker.Models;

public class Agency
{
    [JsonProperty("agency_id")]
    public int AgencyId { get; set; }
    [JsonProperty("agency_name")]
    public string? AgencyName { get; set; }
    [JsonProperty("agency_url")]
    public string? AgencyUrl { get; set; }
    [JsonProperty("agency_timezone")]
    public string? AgencyTimezone { get; set; }
    [JsonProperty("agency_lang")]
    public string? AgencyLang { get; set; }
    [JsonProperty("agency_fare_url")]
    public string? AgencyFareUrl { get; set; }
}

