using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using System.Threading.Tasks;
using WebApi.ActionFilters;
using Entities.RequestFeatures;

namespace WebApi.Controllers
{
    [Route("api/tarefas")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public TarefaController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetTarefasAsync([FromQuery] TarefaParameters tarefaParameters)
        {
            var tarefas = await _repository.Tarefa.GetAllAsync(tarefaParameters, trackChanges: false);

            var tarefasDto = _mapper.Map<IEnumerable<TarefaDto>>(tarefas);
            return Ok(tarefasDto);
        }

        [HttpGet("{id}", Name = "TarefaById")]
        [ServiceFilter(typeof(ValidateTarefaExistsAttribute))]
        public IActionResult GetTarefa(Guid id)
        {
            var tarefa = HttpContext.Items["tarefa"] as Tarefa;

            return Ok(_mapper.Map<TarefaDto>(tarefa));
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateTarefaAsync([FromBody] TarefaForCreationDto tarefa)
        {
            var tarefaEntity = _mapper.Map<Tarefa>(tarefa);
            _repository.Tarefa.Create(tarefaEntity);
            await _repository.SaveAsync();
            var tarefaToReturn = _mapper.Map<TarefaDto>(tarefaEntity);
            return CreatedAtRoute("TarefaById", new { id = tarefaToReturn.Id }, tarefaToReturn);
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateTarefaExistsAttribute))]
        public async Task<IActionResult> DeleteTarefaAsync(Guid id)
        {
            var tarefa = HttpContext.Items["tarefa"] as Tarefa;
            _repository.Tarefa.Delete(tarefa);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateTarefaExistsAttribute))]
        public async Task<IActionResult> UpdateTarefa(Guid id, [FromBody] TarefaForUpdateDto tarefa)
        {
            var tarefaEntity = HttpContext.Items["tarefa"] as Tarefa;
            _mapper.Map(tarefa, tarefaEntity);
            await _repository.SaveAsync();

            return NoContent();
        }
    }
}