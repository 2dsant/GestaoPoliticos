using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsumoProjetoApi.Models
{
    public class Representante
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public List<Telefone> Telefone { get; set; }
        public bool Deletado { get; set; }
    }
}