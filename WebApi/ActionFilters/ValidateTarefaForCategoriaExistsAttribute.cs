using System;
using System.Threading.Tasks;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.ActionFilters
{
    public class ValidateTarefaForCategoriaExistsAttribute : IAsyncActionFilter
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        public ValidateTarefaForCategoriaExistsAttribute(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var method = context.HttpContext.Request.Method;
            var trackChanges = (method.Equals("PUT") || method.Equals("PATCH")) ? true : false;
            var categoriaId = (Guid)context.ActionArguments["categoriaId"];
            var categoria = await _repository.Categoria.GetCategoriaAsync(categoriaId, false);
            if (categoria == null)
            {
                _logger.LogInfo($"Categoria with id: {categoriaId} doesn't exist in the database.");
                context.Result = new NotFoundResult();
                return;
            }
            var id = (Guid)context.ActionArguments["id"];
            var tarefa = await _repository.Tarefa.GetTarefaAsync(categoriaId, id, trackChanges);
            if (tarefa == null)
            {
                _logger.LogInfo($"Tarefa with id: {id} doesn't exist in the database.");
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("tarefa", tarefa);
                await next();
            }
        }
    }
}