using System.Collections.Generic;
using System.Linq;
using AutoMapper.QueryableExtensions;
using TodoApi.App.DbContext;
using TodoApi.App.Dto;
using TodoApi.App.Dto.Mapping;

namespace TodoApi.App.Services;

public interface ITodoService
{
    IQueryable<TodoDto> GetAll();
}

public class TodoService : ITodoService
{
    private readonly TodoApiContext _todoApiContext;

    public TodoService(TodoApiContext todoApiContext)
    {
        _todoApiContext = todoApiContext;
    }

    public IQueryable<TodoDto> GetAll()
    {
        return _todoApiContext.Todos.ProjectTo<TodoDto>(Mapper.Instance.ConfigurationProvider);
    }
}