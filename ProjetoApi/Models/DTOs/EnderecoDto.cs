using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoApi.Models.DTOs
{
    public class EnderecoDto
    {
        [MaxLength(9, ErrorMessage = "Cep inválido. Cep deve possuir 9 caracteres.")]
        [MinLength(9, ErrorMessage = "Cep inválido. Cep deve possuir 9 caracteres.")]
        public string Cep { get; set; }

        [MaxLength(100, ErrorMessage = "Cidade inválida. Máximo de 100 caracteres.")]
        [MinLength(3, ErrorMessage = "Cidade inválida. Mínimo de 3 caracteres.")]
        public string Cidade { get; set; }

        [MaxLength(2, ErrorMessage = "Estado inválido. Estado deve possuir 2 caracteres.")]
        [MinLength(2, ErrorMessage = "Estado inválido. Estado deve possuir 2 caracteres.")]
        public string Estado { get; set; }
        
        [MaxLength(100, ErrorMessage = "Cidade inválida. Máximo de 100 caracteres.")]
        [MinLength(3, ErrorMessage = "Cidade inválida. Mínimo de 3 caracteres.")]
        public string Rua { get; set; }

        [MaxLength(100, ErrorMessage = "Cidade inválida. Máximo de 100 caracteres.")]
        [MinLength(3, ErrorMessage = "Cidade inválida. Mínimo de 3 caracteres.")]
        public string Bairro { get; set; }
    }
}