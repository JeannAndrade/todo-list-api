using System;

namespace Entities.DataTransferObjects
{
    public class TarefaDto
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public bool Finalizada { get; set; }
    }
}