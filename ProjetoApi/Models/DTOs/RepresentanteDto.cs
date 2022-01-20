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

        [MaxLength(100, ErrorMessage = "Nome muito grande. Máximo de 100 caracteres")]
        [MinLength(3, ErrorMessage = "Nome muito pequeno. Mínimo de 3 caracteres.")]
        public string Nome { get; set; }

        [MaxLength(14, ErrorMessage = "Cpf inválido. Cpf deve possuir 14 caracteres.")]
        [MinLength(14, ErrorMessage = "Cpf inválido. Cpf deve possuir 14 caracteres.")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Telefone é obrigatório.")]
        public List<TelefoneDto> Telefone { get; set; }
        public bool Deletado { get; set; } = false;
    }
}