using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;
using Entities.RequestFeatures;

namespace Contracts
{
    public interface ICategoriaRepository
    {
        Task<IEnumerable<Categoria>> GetAllAsync(CategoriaParameters categoriaParameters, bool trackChanges);
        Task<Categoria> GetCategoriaAsync(Guid categoriaId, bool trackChanges);
        Task<Categoria> GetCategoriaDefaultAsync();
        void CreateCategoria(Categoria categoria);
        void DeleteCategoria(Categoria categoria);
    }
}