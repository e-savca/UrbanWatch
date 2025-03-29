using UrbanWatchAPI.Domain.Entities.PublicTransportEntities;
using UrbanWatchAPI.Domain.Interfaces;
using UrbanWatchAPI.Infrastructure.Mongo.Documents;

namespace UrbanWatchAPI.Infrastructure.Mongo.Mappers;

public class VehicleMapper : IDocumentMapper<Vehicle, VehicleDocument>
{
    public VehicleDocument ToDocument(Vehicle vehicle)
    {
        return new VehicleDocument();
    }

    public Vehicle ToDomain(VehicleDocument vehicle)
    {
        return new Vehicle();
    }
}