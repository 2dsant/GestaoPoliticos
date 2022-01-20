using System.Collections.Generic;

namespace ProjetoApi.Models
{
    public class Representante
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public virtual List<Telefone> Telefone { get; set; }
        public bool Deletado { get; set; } = false;
    }
}