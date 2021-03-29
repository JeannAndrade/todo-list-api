using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public class TarefaForUpdateDto
    {
        [Required(ErrorMessage = "A descrição da tarefa é obrigatória.")]
        [MaxLength(150, ErrorMessage = "O tamanho máximo da descrição é 150 caracteres.")]
        public string Descricao { get; set; }
        public bool Finalizada { get; set; }
    }
}