using System.Threading.Tasks;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.ActionFilters
{
    public class GenerateDefaultCategoriaAttribute : IAsyncActionFilter
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        public GenerateDefaultCategoriaAttribute(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var categoria = await _repository.Categoria.GetCategoriaDefaultAsync();

            if (categoria == null)
            {
                _logger.LogInfo($"Não foi possível recuperar a categoria default.");
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("categoria", categoria);
                await next();
            }
        }
    }
}