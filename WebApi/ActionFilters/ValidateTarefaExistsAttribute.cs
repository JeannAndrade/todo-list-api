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
            var tarefa = await _repository.Tarefa.GetByIdAsync(id, trackChanges);
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