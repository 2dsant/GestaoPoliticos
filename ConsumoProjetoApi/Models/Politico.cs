using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetoApi.Models.Enums;

namespace ConsumoProjetoApi.Models
{
    public class Politico
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public Cargo Cargo { get; set; }
        public Endereco Endereco { get; set; }
        public List<Telefone> Telefone { get; set; } = new List<Telefone>();
        public Partido Partido { get; set; }
        public List<ProjetoLei> ProjetosLei { get; set; } = new List<ProjetoLei>();
        public List<Processo> Processos { get; set; } = new List<Processo>();
        public Representante Representante { get; set; }
        public string Foto { get; set; }
        public bool Deletado { get; set; }
        public List<string> links = new List<string>();
    }
}