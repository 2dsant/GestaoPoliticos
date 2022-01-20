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
    public class RepresentanteController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                string urlLogin = "https://localhost:5001/v1/Representante/";
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                HttpClient client = new HttpClient(clientHandler);
                using (var cliente = client)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtToken.Token);
                    var resultado = cliente.GetAsync(urlLogin).Result;
                    var response = await resultado.Content.ReadAsStringAsync();
                    List<RepresentanteContainer> representantes = new List<RepresentanteContainer>();
                    representantes = JsonConvert.DeserializeObject<List<RepresentanteContainer>>(response);
                    Response.StatusCode = (int)resultado.StatusCode;
                    return new ObjectResult(representantes);
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
                string urlLogin = "https://localhost:5001/v1/Representante/" + id;
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                HttpClient client = new HttpClient(clientHandler);
                using (var cliente = client)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtToken.Token);
                    var resultado = cliente.GetAsync(urlLogin).Result;
                    var response = await resultado.Content.ReadAsStringAsync();
                    RepresentanteContainer representante = new RepresentanteContainer();
                    representante = JsonConvert.DeserializeObject<RepresentanteContainer>(response);
                    Response.StatusCode = (int)resultado.StatusCode;
                    return new ObjectResult(representante);
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

    public class RepresentanteContainer
    {
        public RepresentanteDto Representante { get; set; }
        public Link[] Links { get; set; }
    }
}