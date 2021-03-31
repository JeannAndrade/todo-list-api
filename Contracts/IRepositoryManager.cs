using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        ICategoriaRepository Categoria { get; }
        ITarefaRepository Tarefa { get; }
        Task SaveAsync();
    }
}