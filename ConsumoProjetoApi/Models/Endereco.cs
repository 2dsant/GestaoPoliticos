using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsumoProjetoApi.Models
{
    public class Endereco
    {
        public int Id { get; set; }
        public string Cep { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Rua { get; set; }
        public string Bairro { get; set; }
    }
}