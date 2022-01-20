using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ProjetoApi.Models.Enums;

namespace ProjetoApi.Models.DTOs
{
    public class ProjetoLeiDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public TiposProjetosLei TiposProjetosLei { get; set; }
        public string Ementa { get; set; }
        public PoliticoDto Autor { get; set; }
        public StatusProjetoLei StatusProjetoLei { get; set; }
        public bool Deletado { get; set; } = false;
    }
}