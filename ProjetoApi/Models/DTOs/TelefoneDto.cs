using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ProjetoApi.Models.Enums;

namespace ProjetoApi.Models.DTOs
{
    public class TelefoneDto
    {
        [Required]
        [MaxLength(15, ErrorMessage = "Número inválido. Máximo de 15 caracteres")]
        [MinLength(10, ErrorMessage = "Número inválido. Mínimo de 10 caracteres.")]
        public string Numero { get; set; }

        [Required]
        public TipoTelefone Tipo { get; set; }
        public bool Deletado { get; set; } = false;
    }
}