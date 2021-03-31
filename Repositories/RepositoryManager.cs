using System.Threading.Tasks;
using Contracts;
using Entities;

namespace Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;
        private ITarefaRepository _tarefaRepository;
        private ICategoriaRepository _catgoriaRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
        public ITarefaRepository Tarefa
        {
            get
            {
                if (_tarefaRepository == null)
                    _tarefaRepository = new TarefaRepository(_repositoryContext);
                return _tarefaRepository;
            }
        }

        public ICategoriaRepository Categoria
        {
            get
            {
                if (_catgoriaRepository == null)
                    _catgoriaRepository = new CategoriaRepository(_repositoryContext);
                return _catgoriaRepository;
            }
        }

        public Task SaveAsync() => _repositoryContext.SaveChangesAsync();
    }
}