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

        [MaxLength(50, ErrorMessage = "Nome inválido. Nome deve possuir até 50 caracteres.")]
        [MinLength(5, ErrorMessage = "Nome inválido. Cep deve possuir pelo menos 5 caracteres.")]
        public string Nome { get; set; }

        [MaxLength(4, ErrorMessage = "Sigla inválida. Sigla deve possuir até 4 caracteres.")]
        [MinLength(2, ErrorMessage = "Sigla inválida. Sigla deve possuir pelo menos 2 caracteres.")]
        public string Sigla { get; set; }

        [Required]
        public int RepresentanteId { get; set; }
        public List<PoliticoDto> Politicos { get; set; }
        public bool Deletado { get; set; } = false;
    }
}