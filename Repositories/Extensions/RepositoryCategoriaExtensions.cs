using Entities.Models;
using Repositories.Extensions.Utility;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Repositories.Extensions
{
    public static class RepositoryCategoriaExtensions
    {
        public static IQueryable<Categoria> Sort(this IQueryable<Categoria> categorias, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return categorias.OrderBy(e => e.Nome);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Categoria>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return categorias.OrderBy(e => e.Nome);

            return categorias.OrderBy(orderQuery);
        }
    }
}