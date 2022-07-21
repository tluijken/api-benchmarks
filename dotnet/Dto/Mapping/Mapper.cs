using System;
using AutoMapper;

namespace TodoApi.App.Dto.Mapping;

internal static class Mapper
{
    internal static IMapper Instance =>
        new Lazy<IMapper>(() =>
            new MapperConfiguration(mc =>
                mc.AddProfile(new MappingProfile())).CreateMapper()).Value;
}