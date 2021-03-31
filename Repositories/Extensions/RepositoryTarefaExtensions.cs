using Entities.Models;
using Repositories.Extensions.Utility;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Repositories.Extensions
{
    public static class RepositoryTarefaExtensions
    {
        public static IQueryable<Tarefa> Sort(this IQueryable<Tarefa> tarefas, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return tarefas.OrderBy(e => e.Finalizada).ThenBy(e => e.Descricao);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Tarefa>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return tarefas.OrderBy(e => e.Finalizada).ThenBy(e => e.Descricao);

            return tarefas.OrderBy(orderQuery);
        }

    }
}