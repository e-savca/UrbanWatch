﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using UrbanWatchMVCWebApp.Models.DataModels;

namespace UrbanWatchMVCWebApp.Services;
public class MappingService
{
    private readonly IMapper _mapper;
    public MappingService(IMapper mapper)
    {
        _mapper = mapper;
    }
    public TDestination DoMapping<TDestination>(object? source)
            where TDestination : class
    {
        var mappedObject = _mapper.Map<TDestination>(source);
        return mappedObject;
    }

    public IQueryable<TDestination> DoMapping<TDestination>(IQueryable<Stop?> source)
        where TDestination : class
    {
        var mappedQuery = source.ProjectTo<TDestination>(_mapper.ConfigurationProvider);
        return mappedQuery;
    }
}
