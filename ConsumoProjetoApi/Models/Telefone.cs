using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetoApi.Models.Enums;

namespace ConsumoProjetoApi.Models
{
    public class Telefone
    {
        public int Id { get; set; }
        public string Numero { get; set; }
        public TipoTelefone Tipo { get; set; }
        public bool Deletado { get; set; }
    }
}