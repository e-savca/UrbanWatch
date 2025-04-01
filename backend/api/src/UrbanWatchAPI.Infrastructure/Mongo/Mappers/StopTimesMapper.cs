using UrbanWatchAPI.Domain.Entities.PublicTransportEntities;
using UrbanWatchAPI.Domain.Interfaces;
using UrbanWatchAPI.Infrastructure.Mongo.Documents;

namespace UrbanWatchAPI.Infrastructure.Mongo.Mappers;

public class StopTimesMapper : IDocumentMapper<StopTimes, StopTimesDocument>
{
    public StopTimesDocument ToDocument(StopTimes stopTimes)
    {
        return new StopTimesDocument(){
            Id = stopTimes.Id,
            TripId = stopTimes.TripId,
            StopId = stopTimes.StopId,
            StopSequence = stopTimes.StopSequence
        };
    }

    public StopTimes ToDomain(StopTimesDocument stopTimes)
    {
        return new StopTimes(){
            Id = stopTimes.Id,
            TripId = stopTimes.TripId,
            StopId = stopTimes.StopId,
            StopSequence = stopTimes.StopSequence
        };
    }
}