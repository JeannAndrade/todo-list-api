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
    public class TarefaRepository : RepositoryBase<Tarefa>, ITarefaRepository
    {
        public TarefaRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        { }

        public async Task<IEnumerable<Tarefa>> GetAllAsync(TarefaParameters tarefaParameters, bool trackChanges) =>
            await FindAll(trackChanges)
            .Sort(tarefaParameters.OrderBy)
            .ToListAsync();

        public async Task<Tarefa> GetByIdAsync(Guid tarefaId, bool trackChanges) =>
            await FindByCondition(c => c.Id.Equals(tarefaId), trackChanges).SingleOrDefaultAsync();
    }
}