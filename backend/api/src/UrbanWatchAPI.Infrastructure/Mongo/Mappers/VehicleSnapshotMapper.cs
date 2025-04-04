using UrbanWatchAPI.Domain.Entities.PublicTransportEntities;
using UrbanWatchAPI.Domain.Interfaces;
using UrbanWatchAPI.Infrastructure.Mongo.Documents;

namespace UrbanWatchAPI.Infrastructure.Mongo.Mappers;

public class VehicleSnapshotMapper : IDocumentMapper<VehicleSnapshot, VehicleSnapshotDocument>
{

    public VehicleSnapshotDocument ToDocument(VehicleSnapshot vehicleSnapshot)
    {
        return new VehicleSnapshotDocument(){
            Id = vehicleSnapshot.Id,
            Timestamp = vehicleSnapshot.Timestamp,
            Vehicles = vehicleSnapshot.Vehicles.Select(v => new VehicleMapper().ToDocument(v)).ToList()
        };
    }

    public VehicleSnapshot ToDomain(VehicleSnapshotDocument vehicleSnapshot)
    {
        return new VehicleSnapshot(){
            Id = vehicleSnapshot.Id,
            Timestamp = vehicleSnapshot.Timestamp,
            Vehicles = vehicleSnapshot.Vehicles.Select(v => new VehicleMapper().ToDomain(v)).ToList()
        };
    }
    
    public List<VehicleSnapshot> ToDomain(List<VehicleSnapshotDocument> vehicleSnapshot)
    {
        return vehicleSnapshot.Select(d => ToDomain(d)).ToList();
    }
}