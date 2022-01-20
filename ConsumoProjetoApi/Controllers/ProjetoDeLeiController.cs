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
    public class ProjetoDeLeiController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                string urlLogin = "https://localhost:5001/v1/ProjetoDeLei/";
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                HttpClient client = new HttpClient(clientHandler);
                using (var cliente = client)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtToken.Token);
                    var resultado = cliente.GetAsync(urlLogin).Result;
                    var response = await resultado.Content.ReadAsStringAsync();
                    List<ProjetoLeiContainer> projetos = new List<ProjetoLeiContainer>();
                    projetos = JsonConvert.DeserializeObject<List<ProjetoLeiContainer>>(response);
                    Response.StatusCode = (int)resultado.StatusCode;
                    return new ObjectResult(projetos);
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
                string urlLogin = "https://localhost:5001/v1/ProjetoDeLei/" + id;
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                HttpClient client = new HttpClient(clientHandler);
                using (var cliente = client)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtToken.Token);
                    var resultado = cliente.GetAsync(urlLogin).Result;
                    var response = await resultado.Content.ReadAsStringAsync();
                    ProjetoLeiContainer projeto = new ProjetoLeiContainer();
                    projeto = JsonConvert.DeserializeObject<ProjetoLeiContainer>(response);
                    Response.StatusCode = (int)resultado.StatusCode;
                    return new ObjectResult(projeto);
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

    public class ProjetoLeiContainer
    {
        public ProjetoLeiDto ProjetoLei { get; set; }
        public Link[] Links { get; set; }
    }
}