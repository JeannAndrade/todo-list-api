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

        public async Task<IEnumerable<Tarefa>> GetTarefasAsync(Guid categoriaId, TarefaParameters tarefaParameters, bool trackChanges)
        {
            var tarefas = await FindByCondition(e => e.CategoriaId.Equals(categoriaId), trackChanges)
                .Sort(tarefaParameters.OrderBy)
                .ToListAsync();

            return tarefas;
        }

        public async Task<Tarefa> GetTarefaAsync(Guid categoriaId, Guid id, bool trackChanges) =>
            await FindByCondition(e => e.CategoriaId.Equals(categoriaId) && e.Id.Equals(id), trackChanges)
                .SingleOrDefaultAsync();

        public void CreateTarefaForCategoria(Guid categoriaId, Tarefa tarefa)
        {
            tarefa.CategoriaId = categoriaId;
            Create(tarefa);
        }

        public void DeleteTarefa(Tarefa tarefa)
        {
            Delete(tarefa);
        }
    }
}