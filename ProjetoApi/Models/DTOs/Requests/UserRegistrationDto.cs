using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoApi.Models.DTOs.Requests
{
    public class UserRegistrationDto
    {
        [Required(ErrorMessage = "Nome é obrigatório.")]
        [MaxLength(80, ErrorMessage = "Este campo deve conter entre 3 a 80 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 a 80 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Username é obrigatório.")]
        [MaxLength(60, ErrorMessage = "Este campo deve conter entre 3 a 60 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 a 60 caracteres")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email é obrigatório.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatório.")]
        public string Password { get; set; }

    }
}