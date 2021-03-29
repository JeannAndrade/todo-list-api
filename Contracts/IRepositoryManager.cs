using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        ITarefaRepository Tarefa { get; }
        Task SaveAsync();
    }
}