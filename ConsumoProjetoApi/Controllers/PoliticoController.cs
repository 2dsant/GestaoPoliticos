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
    public class PoliticoController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                string urlLogin = "https://localhost:5001/v1/Politico/";
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                HttpClient client = new HttpClient(clientHandler);
                using (var cliente = client)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtToken.Token);
                    var resultado = cliente.GetAsync(urlLogin).Result;
                    var response = await resultado.Content.ReadAsStringAsync();
                    List<PoliticoContainer> politicos = new List<PoliticoContainer>();
                    politicos = JsonConvert.DeserializeObject<List<PoliticoContainer>>(response);
                    Response.StatusCode = (int)resultado.StatusCode;
                    return new ObjectResult(politicos);
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

        [HttpGet]
        [Route("Asc")]
        public async Task<IActionResult> GetAscAsync()
        {
            try
            {
                string urlLogin = "https://localhost:5001/v1/Politico/Asc";
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                HttpClient client = new HttpClient(clientHandler);
                using (var cliente = client)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtToken.Token);
                    var resultado = cliente.GetAsync(urlLogin).Result;
                    var response = await resultado.Content.ReadAsStringAsync();
                    List<PoliticoContainer> politicos = new List<PoliticoContainer>();
                    politicos = JsonConvert.DeserializeObject<List<PoliticoContainer>>(response);
                    Response.StatusCode = (int)resultado.StatusCode;
                    return new ObjectResult(politicos);
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

        [HttpGet]
        [Route("Desc")]
        public async Task<IActionResult> GetDescAsync()
        {
            try
            {
                string urlLogin = "https://localhost:5001/v1/Politico/Desc";
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                HttpClient client = new HttpClient(clientHandler);
                using (var cliente = client)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtToken.Token);
                    var resultado = cliente.GetAsync(urlLogin).Result;
                    var response = await resultado.Content.ReadAsStringAsync();
                    List<PoliticoContainer> politicos = new List<PoliticoContainer>();
                    politicos = JsonConvert.DeserializeObject<List<PoliticoContainer>>(response);
                    Response.StatusCode = (int)resultado.StatusCode;
                    return new ObjectResult(politicos);
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
                string urlLogin = "https://localhost:5001/v1/Politico/" + id;
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                HttpClient client = new HttpClient(clientHandler);
                using (var cliente = client)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtToken.Token);
                    var resultado = cliente.GetAsync(urlLogin).Result;
                    var response = await resultado.Content.ReadAsStringAsync();
                    PoliticoContainer politico = new PoliticoContainer();
                    politico = JsonConvert.DeserializeObject<PoliticoContainer>(response);
                    Response.StatusCode = (int)resultado.StatusCode;
                    return new ObjectResult(politico);
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

    public class PoliticoContainer
    {
        public PoliticoDto Politico { get; set; }
        public Link[] Links { get; set; }
    }
}