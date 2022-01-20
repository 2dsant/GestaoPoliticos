using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetoApi.Models.Enums;

namespace ConsumoProjetoApi.Models
{
    public class ProjetoLei
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public TiposProjetosLei TiposProjetosLei { get; set; }
        public string Ementa { get; set; }
        public Politico Autor { get; set; }
        public StatusProjetoLei StatusProjetoLei { get; set; }
        public bool Deletado { get; set; }
    }
}