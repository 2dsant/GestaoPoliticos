using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsumoProjetoApi.Models
{
    public class Partido
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public Representante Representante { get; set; }
        public List<Politico> Politicos { get; set; }
        public bool Deletado { get; set; }
    }
}