using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApi.ActionFilters;

namespace WebApi.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/categorias/{categoriaId}/tarefas")]
    [ApiController]
    public class TarefaV2Controller : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public TarefaV2Controller(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetTarefasForCategoria(Guid categoriaId, [FromQuery] TarefaParameters tarefaParameters)
        {
            var caetgoria = await _repository.Categoria.GetCategoriaAsync(categoriaId, trackChanges: false);
            if (caetgoria == null)
            {
                _logger.LogInfo($"Categoria with id: {categoriaId} doesn't exist in the database.");
                return NotFound();
            }
            var tarefasFromDb = await _repository.Tarefa.GetTarefasAsync(categoriaId, tarefaParameters, trackChanges: false);

            var employeesDto = _mapper.Map<IEnumerable<TarefaDto>>(tarefasFromDb);

            return Ok(employeesDto);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateEmployeeForCompany(Guid categoriaId, [FromBody] TarefaForCreationDto tarefa)
        {
            var categoria = await _repository.Categoria.GetCategoriaAsync(categoriaId, trackChanges: false);
            if (categoria == null)
            {
                _logger.LogInfo($"Categoria with id: {categoriaId} doesn't exist in the database.");
                return NotFound();
            }
            var tarefaEntity = _mapper.Map<Tarefa>(tarefa);
            _repository.Tarefa.CreateTarefaForCategoria(categoriaId, tarefaEntity);
            await _repository.SaveAsync();

            var employeeToReturn = _mapper.Map<TarefaDto>(tarefaEntity);
            return CreatedAtRoute(
                "GetEmployeeForCompany",
                 new
                 {
                     categoriaId,
                     id = employeeToReturn.Id
                 },
                employeeToReturn);
        }
    }
}