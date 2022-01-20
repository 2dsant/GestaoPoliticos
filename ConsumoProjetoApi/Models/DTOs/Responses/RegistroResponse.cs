using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsumoProjetoApi.Models.DTOs.Responses
{
    public class RegistroResponse
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
    }
}