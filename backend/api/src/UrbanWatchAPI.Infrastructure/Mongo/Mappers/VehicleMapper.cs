using UrbanWatchAPI.Domain.Entities.PublicTransportEntities;
using UrbanWatchAPI.Infrastructure.Mongo.Documents;

namespace UrbanWatchAPI.Infrastructure.Mongo.Mappers;

public class VehicleMapper
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