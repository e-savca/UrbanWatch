using UrbanWatchAPI.Domain.Entities.PublicTransportEntities;
using UrbanWatchAPI.Domain.Interfaces;
using UrbanWatchAPI.Infrastructure.Mongo.Documents;

namespace UrbanWatchAPI.Infrastructure.Mongo.Mappers;

public class VehicleSnapshotMapper : IDocumentMapper<VehicleSnapshot, VehicleSnapshotDocument>
{

    public VehicleSnapshotDocument ToDocument(VehicleSnapshot agency)
    {
        return new VehicleSnapshotDocument();
    }

    public VehicleSnapshot ToDomain(VehicleSnapshotDocument agency)
    {
        return new VehicleSnapshot();
    }
}