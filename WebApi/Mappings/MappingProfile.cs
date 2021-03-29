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
        }
    }
}