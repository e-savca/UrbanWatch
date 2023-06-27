using AutoMapper;

namespace UrbanWatchMVCWebApp;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // Mapping Models.ApiModels.TranzyV1Models to Models.DataModels
        CreateMap<Models.ApiModels.TranzyV1Models.Route, Models.DataModels.Route>();
        CreateMap<Models.ApiModels.TranzyV1Models.Shape, Models.DataModels.Shape>();
        CreateMap<Models.ApiModels.TranzyV1Models.Stop, Models.DataModels.Stop>();
        CreateMap<Models.ApiModels.TranzyV1Models.StopTimes, Models.DataModels.StopTimes>();
        CreateMap<Models.ApiModels.TranzyV1Models.Trip, Models.DataModels.Trip>();
        CreateMap<Models.ApiModels.TranzyV1Models.Vehicle, Models.DataModels.Vehicle>();

        // Mapping IQueryable<Models.ApiModels.TranzyV1Models> to IQueryable<Models.DataModels>
        CreateMap<IQueryable<Models.ApiModels.TranzyV1Models.Route>, IQueryable<Models.DataModels.Route>>();
        CreateMap<IQueryable<Models.ApiModels.TranzyV1Models.Shape>, IQueryable<Models.DataModels.Shape>>();
        CreateMap<IQueryable<Models.ApiModels.TranzyV1Models.Stop>, IQueryable<Models.DataModels.Stop>>();
        CreateMap<IQueryable<Models.ApiModels.TranzyV1Models.StopTimes>, Models.DataModels.StopTimes>();
        CreateMap<IQueryable<Models.ApiModels.TranzyV1Models.Trip>, IQueryable<Models.DataModels.Trip>>();
        CreateMap<IQueryable<Models.ApiModels.TranzyV1Models.Vehicle>, IQueryable<Models.DataModels.Vehicle>>();

        // Mapping Models.ApiModels.TranzyV1Models to Models.UiModels
        CreateMap<Models.ApiModels.TranzyV1Models.Route, Models.UiModels.Route>();
        CreateMap<Models.ApiModels.TranzyV1Models.Shape, Models.UiModels.Shape>();
        CreateMap<Models.ApiModels.TranzyV1Models.Stop, Models.UiModels.Stop>();
        CreateMap<Models.ApiModels.TranzyV1Models.StopTimes, Models.UiModels.StopTimes>();
        CreateMap<Models.ApiModels.TranzyV1Models.Trip, Models.UiModels.Trip>();
        CreateMap<Models.ApiModels.TranzyV1Models.Vehicle, Models.UiModels.Vehicle>();

        // Mapping IQueryable<Models.ApiModels.TranzyV1Models> to IQueryable<Models.UiModels>
        CreateMap<IQueryable<Models.ApiModels.TranzyV1Models.Route>, IQueryable<Models.UiModels.Route>>();
        CreateMap<IQueryable<Models.ApiModels.TranzyV1Models.Shape>, IQueryable<Models.UiModels.Shape>>();
        CreateMap<IQueryable<Models.ApiModels.TranzyV1Models.Stop>, IQueryable<Models.UiModels.Stop>>();
        CreateMap<IQueryable<Models.ApiModels.TranzyV1Models.StopTimes>, IQueryable<Models.UiModels.StopTimes>>();
        CreateMap<IQueryable<Models.ApiModels.TranzyV1Models.Trip>, IQueryable<Models.UiModels.Trip>>();
        CreateMap<IQueryable<Models.ApiModels.TranzyV1Models.Vehicle>, IQueryable<Models.UiModels.Vehicle>>();

        // Mapping Models.DataModels to Models.UiModels
        CreateMap<Models.DataModels.Route, Models.UiModels.Route>();
        CreateMap<Models.DataModels.Shape, Models.UiModels.Shape>();
        CreateMap<Models.DataModels.Stop, Models.UiModels.Stop>();
        CreateMap<Models.DataModels.StopTimes, Models.UiModels.StopTimes>();
        CreateMap<Models.DataModels.Trip, Models.UiModels.Trip>();
        CreateMap<Models.DataModels.Vehicle, Models.UiModels.Vehicle>();

        // Mapping IQueryable<Models.DataModels> to IQueryable<Models.UiModels>
        CreateMap<IQueryable<Models.DataModels.Route>, IQueryable<Models.UiModels.Route>>();
        CreateMap<IQueryable<Models.DataModels.Shape>, IQueryable<Models.UiModels.Shape>>();
        CreateMap<IQueryable<Models.DataModels.Stop>, IQueryable<Models.UiModels.Stop>>();
        CreateMap<IQueryable<Models.DataModels.StopTimes>, IQueryable<Models.UiModels.StopTimes>>();
        CreateMap<IQueryable<Models.DataModels.Trip>, IQueryable<Models.UiModels.Trip>>();
        CreateMap<IQueryable<Models.DataModels.Vehicle>, IQueryable<Models.UiModels.Vehicle>>();

        // Mapping Models.UiModels to Models.DataModels
        CreateMap<Models.UiModels.Route, Models.DataModels.Route>();
        CreateMap<Models.UiModels.Shape, Models.DataModels.Shape>();
        CreateMap<Models.UiModels.Stop, Models.DataModels.Stop>();
        CreateMap<Models.UiModels.StopTimes, Models.DataModels.StopTimes>();
        CreateMap<Models.UiModels.Trip, Models.DataModels.Trip>();
        CreateMap<Models.UiModels.Vehicle, Models.DataModels.Vehicle>();

        // Mapping IQueryable<Models.UiModels> to IQueryable<Models.DataModels>
        CreateMap<IQueryable<Models.UiModels.Route>, IQueryable<Models.DataModels.Route>>();
        CreateMap<IQueryable<Models.UiModels.Shape>, IQueryable<Models.DataModels.Shape>>();
        CreateMap<IQueryable<Models.UiModels.Stop>, IQueryable<Models.DataModels.Stop>>();
        CreateMap<IQueryable<Models.UiModels.StopTimes>, IQueryable<Models.DataModels.StopTimes>>();
        CreateMap<IQueryable<Models.UiModels.Trip>, IQueryable<Models.DataModels.Trip>>();
        CreateMap<IQueryable<Models.UiModels.Vehicle>, IQueryable<Models.DataModels.Vehicle>>();
    }
}
