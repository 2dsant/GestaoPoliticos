using System.Collections.Generic;

namespace ProjetoApi.Models
{
    public class Partido
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public virtual Representante Representante { get; set; }
        public virtual List<Politico> Politicos { get; set; }
        public bool Deletado { get; set; } = false;
    }
}