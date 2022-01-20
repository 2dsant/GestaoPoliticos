using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoApi.Models.DTOs
{
    public class RepresentanteDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Deletado { get; set; } = false;
    }
}