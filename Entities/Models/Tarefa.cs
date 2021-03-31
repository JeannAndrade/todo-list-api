using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Tarefa
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "A descrição da tarefa é obrigatória.")]
        [MaxLength(150, ErrorMessage = "O tamanho máximo da descrição é 150 caracteres.")]
        public string Descricao { get; set; }
        public bool Finalizada { get; set; }

        [ForeignKey(nameof(Categoria))]
        public Guid CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
    }
}