using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetoApi.Models.Enums;

namespace ProjetoApi.Models.DTOs
{
    public class ProcessoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public PoliticoDto Politico { get; set; }
        public string Descricao { get; set; }
        public StatusProcesso Status { get; set; }
        public bool Deletado { get; set; } = false;
    }
}