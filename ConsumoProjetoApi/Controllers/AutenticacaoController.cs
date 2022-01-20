using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ConsumoProjetoApi.Models.DTOs.Requests;
using ConsumoProjetoApi.Models.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConsumoProjetoApi.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]

    public class AutenticacaoController : ControllerBase
    {
        [HttpPost]
        [Route("Login")]
        //Faz a requisição para a api principal com os dados de login
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginRequest user)
        {
            try
            {
                string urlLogin = "https://localhost:5001/v1/AuthManagement/Login";
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                HttpClient client = new HttpClient(clientHandler);

                using (var cliente = client)
                {

                    string jsonObject = JsonConvert.SerializeObject(user);
                    var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                    var resultado = cliente.PostAsync(urlLogin, content).Result;
                    var result = await resultado.Content.ReadAsStringAsync();
                    string jsonString = result.ToString();
                    var response = new AutenticacaoResponse();
                    response = JsonConvert.DeserializeObject<AutenticacaoResponse>(jsonString);

                    Response.StatusCode = (int)resultado.StatusCode;
                    JwtToken.Token = response.Token;

                    return new ObjectResult(response);
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

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] UserRegistrationDto user)
        {
            try
            {
                string urlLogin = "https://localhost:5001/v1/AuthManagement/Register";
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                HttpClient client = new HttpClient(clientHandler);

                using (var cliente = client)
                {
                    string jsonObject = JsonConvert.SerializeObject(user);
                    var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                    var resultado = cliente.PostAsync(urlLogin, content).Result;
                    var result = await resultado.Content.ReadAsStringAsync();
                    string jsonString = result.ToString();

                    if (resultado.IsSuccessStatusCode)
                    {
                        return Ok("Usuário registrado com sucesso!");
                    }
                    else
                    {
                        var response = new RegistroResponse();
                        response = JsonConvert.DeserializeObject<RegistroResponse>(jsonString);
                        Response.StatusCode = (int)resultado.StatusCode;
                        return new ObjectResult(response);
                    }
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
}