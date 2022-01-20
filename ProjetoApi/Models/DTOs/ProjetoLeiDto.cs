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
        
        [MaxLength(100, ErrorMessage = "Nome inválido. Nome deve possuir no máximo 100 caracteres.")]
        [MinLength(5, ErrorMessage = "Nome inválido. Nome deve possuir no mínimo 5 caracteres.")]
        public string Nome { get; set; }

        [Required]
        public TiposProjetosLei TiposProjetosLei { get; set; }

        [MaxLength(500, ErrorMessage = "Ementa inválida. Ementa deve possuir no máximo 500 caracteres.")]
        [MinLength(20, ErrorMessage = "Ementa inválido. Ementa deve possuir no mínimo 20 caracteres.")]
        public string Ementa { get; set; }

        [Required]
        public int AutorId { get; set; }

        [Required]
        public StatusProjetoLei StatusProjetoLei { get; set; }
        public bool Deletado { get; set; } = false;
    }
}