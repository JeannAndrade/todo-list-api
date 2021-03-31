using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Categoria
    {
        [Column("CategoriaId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo categoria é obrigatório.")]
        [MaxLength(60, ErrorMessage = "Tamanho máximo para a catagoria é 60 caracteres.")]
        public string Nome { get; set; }

        public ICollection<Tarefa> Tarefas { get; set; }
    }
}