using UrbanWatchAPI.Domain.Entities.PublicTransportEntities;
using UrbanWatchAPI.Domain.Interfaces;
using UrbanWatchAPI.Infrastructure.Mongo.Documents;

namespace UrbanWatchAPI.Infrastructure.Mongo.Mappers;

public class TripMapper : IDocumentMapper<Trip, TripDocument>
{
    public TripDocument ToDocument(Trip trip)
    {
        return new TripDocument(){
            Id = trip.Id,
            RouteId = trip.RouteId,
            TripId = trip.TripId,
            TripHeadsign = trip.TripHeadsign,
            DirectionId = trip.DirectionId,
            BlockId = trip.BlockId,
            ShapeId = trip.ShapeId
        };
    }

    public Trip ToDomain(TripDocument trip)
    {
        return new Trip(){
            Id = trip.Id,
            RouteId = trip.RouteId,
            TripId = trip.TripId,
            TripHeadsign = trip.TripHeadsign,
            DirectionId = trip.DirectionId,
            BlockId = trip.BlockId,
            ShapeId = trip.ShapeId
        };
    }
}