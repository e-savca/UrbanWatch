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
    public TDestination DoMapping<TSource, TDestination>(TSource source)
            where TSource : class
            where TDestination : class
    {
        TDestination mappedObject = _mapper.Map<TDestination>(source);
        return mappedObject;
    }

    public IQueryable<TDestination> DoMapping<TSource, TDestination>(IQueryable<TSource> source)
        where TSource : class
        where TDestination : class
    {
        IQueryable<TDestination> mappedQuery = source.ProjectTo<TDestination>(_mapper.ConfigurationProvider);
        return mappedQuery;
    }
}
