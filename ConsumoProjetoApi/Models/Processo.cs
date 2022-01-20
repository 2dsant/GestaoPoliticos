using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetoApi.Models.Enums;

namespace ConsumoProjetoApi.Models
{
    public class Processo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Politico Politico { get; set; }
        public string Descricao { get; set; }
        public StatusProcesso Status { get; set; }
        public bool Deletado { get; set; }
    }
}