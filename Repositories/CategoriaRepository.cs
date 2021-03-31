using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Extensions;

namespace Repositories
{
    public class CategoriaRepository : RepositoryBase<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        { }

        public async Task<IEnumerable<Categoria>> GetAllAsync(CategoriaParameters categoriaParameters, bool trackChanges) =>
            await FindAll(trackChanges)
            .Sort(categoriaParameters.OrderBy)
            .ToListAsync();

        public async Task<Categoria> GetCategoriaAsync(Guid categoriaId, bool trackChanges) =>
            await FindByCondition(c => c.Id.Equals(categoriaId), trackChanges)
                .SingleOrDefaultAsync();
        public void CreateCategoria(Categoria categoria) => Create(categoria);

        public void DeleteCategoria(Categoria categoria) => Delete(categoria);

        public async Task<Categoria> GetCategoriaDefaultAsync() =>
            await FindByCondition(c => c.Nome.Equals("Default"), false)
                .SingleOrDefaultAsync();
    }
}