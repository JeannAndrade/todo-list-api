using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;
using Entities.RequestFeatures;

namespace Contracts
{
    public interface ITarefaRepository
    {
        Task<IEnumerable<Tarefa>> GetAllAsync(TarefaParameters tarefaParameters, bool trackChanges);
        Task<Tarefa> GetByIdAsync(Guid tarefaId, bool trackChanges);
        void Create(Tarefa tarefa);
        void Update(Tarefa tarefa);
        void Delete(Tarefa tarefa);
    }
}