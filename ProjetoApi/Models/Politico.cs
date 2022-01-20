using System.Collections.Generic;
using ProjetoApi.Models.Enums;

namespace ProjetoApi.Models
{
    public class Politico
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public Cargo Cargo { get; set; }
        public virtual Endereco Endereco { get; set; }
        public virtual List<Telefone> Telefone { get; set; } = new List<Telefone>();
        public virtual Partido Partido { get; set; }
        public virtual List<ProjetoLei> ProjetosLei { get; set; } = new List<ProjetoLei>();
        public virtual List<Processo> Processos { get; set; } = new List<Processo>();
        public virtual Representante Representante { get; set; }
        public string Foto { get; set; }
        public bool Deletado { get; set; }
    }
}