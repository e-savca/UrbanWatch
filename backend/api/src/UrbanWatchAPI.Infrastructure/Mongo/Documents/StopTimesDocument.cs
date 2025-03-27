using MongoDB.Bson;
using Newtonsoft.Json;

namespace UrbanWatchAPI.Infrastructure.Mongo.Documents;

public class StopTimesDocument
{
    public ObjectId Id { get; set; }
    public string? TripId { get; set; }
    public int StopId { get; set; }
    public int StopSequence { get; set; }
}

