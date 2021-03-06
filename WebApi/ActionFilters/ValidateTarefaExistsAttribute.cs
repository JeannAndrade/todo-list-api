using System;
using System.Threading.Tasks;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.ActionFilters
{
    public class ValidateTarefaExistsAttribute : IAsyncActionFilter
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        public ValidateTarefaExistsAttribute(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var trackChanges = context.HttpContext.Request.Method.Equals("PUT");
            var id = (Guid)context.ActionArguments["id"];

            var categoria = await _repository.Categoria.GetCategoriaDefaultAsync();

            if (categoria == null)
            {
                _logger.LogInfo($"Não foi possível recuperar a categoria default.");
                context.Result = new NotFoundResult();
            }
            else
            {
                var tarefa = await _repository.Tarefa.GetTarefaAsync(categoria.Id, id, trackChanges);
                if (tarefa == null)
                {
                    _logger.LogInfo($"Tarefa com id: {id} não existe na base de dados.");
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
}