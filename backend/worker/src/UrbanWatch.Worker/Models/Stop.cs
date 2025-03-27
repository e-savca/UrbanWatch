using MongoDB.Bson;
using Newtonsoft.Json;

namespace UrbanWatch.Worker.Models;

public class Stop
{
    [JsonProperty("stop_id")]
    public int StopId { get; set; }
    [JsonProperty("stop_name")]
    public string? StopName { get; set; }
    [JsonProperty("stop_lat")]
    public double Lat { get; set; }
    [JsonProperty("stop_lon")]
    public double Lon { get; set; }
    [JsonProperty("location_type")]
    public int LocationType { get; set; }
    [JsonProperty("stop_code")]
    public string? Code { get; set; }
}

