using AutoMapper;

namespace UrbanWatchMVCWebApp;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // Models.ApiModels.TranzyV1Models mapping to Models.DataModels
        CreateMap<Models.ApiModels.TranzyV1Models.Route, Models.DataModels.Route>();
        CreateMap<Models.ApiModels.TranzyV1Models.Shape, Models.DataModels.Shape>();
        CreateMap<Models.ApiModels.TranzyV1Models.Stop, Models.DataModels.Stop>();
        CreateMap<Models.ApiModels.TranzyV1Models.StopTimes, Models.DataModels.StopTimes>();
        CreateMap<Models.ApiModels.TranzyV1Models.Trip, Models.DataModels.Trip>();
        CreateMap<Models.ApiModels.TranzyV1Models.Vehicle, Models.DataModels.Vehicle>();

        // Models.ApiModels.TranzyV1Models mapping to Models
        CreateMap<Models.ApiModels.TranzyV1Models.Route, Models.Route>();
        CreateMap<Models.ApiModels.TranzyV1Models.Shape, Models.Shape>();
        CreateMap<Models.ApiModels.TranzyV1Models.Stop, Models.Stop>();
        CreateMap<Models.ApiModels.TranzyV1Models.StopTimes, Models.StopTimes>();
        CreateMap<Models.ApiModels.TranzyV1Models.Trip, Models.Trip>();
        CreateMap<Models.ApiModels.TranzyV1Models.Vehicle, Models.Vehicle>();

        // Models.DataModels mapping to Models
        CreateMap<Models.DataModels.Route, Models.Route>();
        CreateMap<Models.DataModels.Shape, Models.Shape>();
        CreateMap<Models.DataModels.Stop, Models.Stop>();
        CreateMap<Models.DataModels.StopTimes, Models.StopTimes>();
        CreateMap<Models.DataModels.Trip, Models.Trip>();
        CreateMap<Models.DataModels.Vehicle, Models.Vehicle>();

        // Models mapping to Models.DataModels
        CreateMap<Models.Route, Models.DataModels.Route>();
        CreateMap<Models.Shape, Models.DataModels.Shape>();
        CreateMap<Models.Stop, Models.DataModels.Stop>();
        CreateMap<Models.StopTimes, Models.DataModels.StopTimes>();
        CreateMap<Models.Trip, Models.DataModels.Trip>();
        CreateMap<Models.Vehicle, Models.DataModels.Vehicle>();        
    }
}
