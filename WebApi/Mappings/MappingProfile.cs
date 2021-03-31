using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace WebApi.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Tarefa, TarefaDto>();
            CreateMap<TarefaForCreationDto, Tarefa>();
            CreateMap<TarefaForUpdateDto, Tarefa>();
            CreateMap<Categoria, CategoriaDto>();
            CreateMap<CategoriaForCreationDto, Categoria>();
            CreateMap<CategoriaForUpdateDto, Categoria>();
        }
    }
}