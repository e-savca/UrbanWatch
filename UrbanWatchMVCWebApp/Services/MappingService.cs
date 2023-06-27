using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace UrbanWatchMVCWebApp.Services;
public class MappingService
{
    private readonly IMapper _mapper;
    public MappingService(IMapper mapper)
    {
        _mapper = mapper;
    }
    public TDestination DoMapping<TDestination>(object source)
            where TDestination : class
    {
        TDestination mappedObject = _mapper.Map<TDestination>(source);
        return mappedObject;
    }

    public IQueryable<TDestination> DoMapping<TDestination>(IQueryable<object> source)
        where TDestination : class
    {
        IQueryable<TDestination> mappedQuery = source.ProjectTo<TDestination>(_mapper.ConfigurationProvider);
        return mappedQuery;
    }
}
