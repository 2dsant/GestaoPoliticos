using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoApi.Models.DTOs.Responses
{
    public class ErrorResponse
    {
        public List<string> Errors { get; set; }
    }
}