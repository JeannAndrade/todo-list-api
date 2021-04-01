using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using WebApi.ActionFilters;

namespace WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/categorias")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public CategoriaController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetTarefasAsync([FromQuery] CategoriaParameters categoriaParameters)
        {
            var categorias = await _repository.Categoria.GetAllAsync(categoriaParameters, trackChanges: false);

            var categoriasDto = _mapper.Map<IEnumerable<CategoriaDto>>(categorias);
            return Ok(categoriasDto);
        }

        [HttpGet("{id}", Name = "CategoriaById")]
        public async Task<IActionResult> GetCategoria(Guid id)
        {
            var categoria = await _repository.Categoria.GetCategoriaAsync(id, trackChanges: false);
            if (categoria == null)
            {
                _logger.LogInfo($"Company with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var categoriaDto = _mapper.Map<CategoriaDto>(categoria);
                return Ok(categoriaDto);
            }
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateCompany([FromBody] CategoriaForCreationDto categoria)
        {
            var categoriaEntity = _mapper.Map<Categoria>(categoria);
            _repository.Categoria.CreateCategoria(categoriaEntity);
            await _repository.SaveAsync();
            var categoriaToReturn = _mapper.Map<CategoriaDto>(categoriaEntity);
            return CreatedAtRoute("CategoriaById", new { id = categoriaToReturn.Id }, categoriaToReturn);
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateCategoriaExistsAttribute))]
        public async Task<IActionResult> DeleteCategoria(Guid id)
        {
            var categoria = HttpContext.Items["categoria"] as Categoria;

            _repository.Categoria.DeleteCategoria(categoria);
            await _repository.SaveAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateCategoriaExistsAttribute))]
        public async Task<IActionResult> UpdateCategoria(Guid id, [FromBody] CategoriaForUpdateDto categoria)
        {
            var categoriaEntity = HttpContext.Items["categoria"] as Categoria;
            _mapper.Map(categoria, categoriaEntity);
            await _repository.SaveAsync();
            return NoContent();
        }
    }
}