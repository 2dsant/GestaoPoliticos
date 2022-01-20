using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ProjetoApi.Models.Enums;

namespace ProjetoApi.Models.DTOs
{
    public class PoliticoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Cargo Cargo { get; set; }
        public PartidoDto Partido { get; set; }
        public RepresentanteDto Representante { get; set; }
        public string Foto { get; set; }
    }
}