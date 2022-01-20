using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ConsumoProjetoApi.Models;
using ConsumoProjetoApi.Models.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjetoApi.Models.DTOs;

namespace ConsumoProjetoApi.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class ProcessoController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                string urlLogin = "https://localhost:5001/v1/Processo/";
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                HttpClient client = new HttpClient(clientHandler);
                using (var cliente = client)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtToken.Token);
                    var resultado = cliente.GetAsync(urlLogin).Result;
                    var response = await resultado.Content.ReadAsStringAsync();
                    List<ProcessoContainer> processos = new List<ProcessoContainer>();
                    processos = JsonConvert.DeserializeObject<List<ProcessoContainer>>(response);
                    Response.StatusCode = (int)resultado.StatusCode;
                    return new ObjectResult(processos);
                }
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorResponse()
                {
                    Errors = new List<string>()
                        {
                            e.Message
                        }
                });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            try
            {
                string urlLogin = "https://localhost:5001/v1/Processo/" + id;
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                HttpClient client = new HttpClient(clientHandler);
                using (var cliente = client)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtToken.Token);
                    var resultado = cliente.GetAsync(urlLogin).Result;
                    var response = await resultado.Content.ReadAsStringAsync();
                    ProcessoContainer processo = new ProcessoContainer();
                    processo = JsonConvert.DeserializeObject<ProcessoContainer>(response);
                    Response.StatusCode = (int)resultado.StatusCode;
                    return new ObjectResult(processo);
                }
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorResponse()
                {
                    Errors = new List<string>()
                        {
                            e.Message
                        }
                });
            }
        }
    }

    public class ProcessoContainer
    {
        public ProcessoDto Processo { get; set; }
        public Link[] Links { get; set; }
    }
}