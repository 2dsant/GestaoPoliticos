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
        
        [Required(ErrorMessage = "Nome é obrigatório.")]
        [MaxLength(100, ErrorMessage = "Nome muito grande. Máximo de 100 caracteres")]
        [MinLength(3, ErrorMessage = "Nome muito pequeno. Mínimo de 2 caracteres.")]
        public string Nome { get; set; }

        [MaxLength(14, ErrorMessage = "Cpf inválido. Cpf deve possuir 14 caracteres.")]
        [MinLength(14, ErrorMessage = "Cpf inválido. Cpf deve possuir 14 caracteres.")]        
        public string Cpf { get; set; }
        
        [Required(ErrorMessage = "Cargo é obrigatório.")]
        public Cargo Cargo { get; set; }

        public EnderecoDto Endereco { get; set; }

        public List<TelefoneDto> Telefone { get; set; } = new List<TelefoneDto>();
        
        [Required(ErrorMessage = "Partido é obrigatório.")]
        public int PartidoId { get; set; }

        [Required(ErrorMessage = "Representante é obrigatório.")]
        public int RepresentanteId { get; set; }

        [Required(ErrorMessage = "Foto é obrigatório.")]
        public string Foto { get; set; }
    }
}