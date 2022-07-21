using AutoMapper;
using TodoApi.App.DbContext;

namespace TodoApi.App.Dto.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Todo, TodoDto>();
    }
}