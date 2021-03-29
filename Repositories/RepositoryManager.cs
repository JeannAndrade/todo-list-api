using System.Threading.Tasks;
using Contracts;
using Entities;

namespace Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;
        private ITarefaRepository _todoRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
        public ITarefaRepository Tarefa
        {
            get
            {
                if (_todoRepository == null)
                    _todoRepository = new TarefaRepository(_repositoryContext);
                return _todoRepository;
            }
        }
        public Task SaveAsync() => _repositoryContext.SaveChangesAsync();
    }
}