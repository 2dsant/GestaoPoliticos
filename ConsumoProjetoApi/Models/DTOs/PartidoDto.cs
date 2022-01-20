using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoApi.Models.DTOs
{
    public class PartidoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public RepresentanteDto Representante { get; set; }
        public List<PoliticoDto> Politicos { get; set; }
        public bool Deletado { get; set; }
    }
}