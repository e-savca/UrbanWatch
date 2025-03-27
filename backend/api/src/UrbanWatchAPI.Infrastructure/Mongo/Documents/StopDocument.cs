using MongoDB.Bson;
using Newtonsoft.Json;

namespace UrbanWatchAPI.Infrastructure.Mongo.Documents;

public class StopDocument
{
    public ObjectId Id { get; set; }
    public int StopId { get; set; }
    public string? StopName { get; set; }
    public double Lat { get; set; }
    public double Lon { get; set; }
    public int LocationType { get; set; }
    public string? Code { get; set; }
}

