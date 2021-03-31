using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;
using Entities.RequestFeatures;

namespace Contracts
{
    public interface ITarefaRepository
    {
        Task<IEnumerable<Tarefa>> GetTarefasAsync(Guid categoriaId, TarefaParameters tarefaParameters, bool trackChanges);
        Task<Tarefa> GetTarefaAsync(Guid categoriaId, Guid id, bool trackChanges);
        void CreateTarefaForCategoria(Guid categoriaId, Tarefa tarefa);
        void DeleteTarefa(Tarefa tarefa);
    }
}