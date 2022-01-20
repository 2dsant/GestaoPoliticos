using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoApi.Data;
using ProjetoApi.Models;
using ProjetoApi.Models.DTOs;
using ProjetoApi.Models.DTOs.Responses;
using ProjetoApi.Services;
using static ValidaCpf.ValidaCpfExtension;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using ProjetoApi.HATEOAS;
using Microsoft.AspNetCore.Authorization;

namespace ProjetoApi.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    [Authorize(Roles = "admin")]
    public class PoliticoController : ControllerBase
    {
        private readonly ApplicationDbContext _database;
        public readonly IWebHostEnvironment _environment;
        private HATEOAS.HATEOAS _HATEOAS;


        public PoliticoController(ApplicationDbContext database, IWebHostEnvironment environment)
        {
            _database = database;
            _environment = environment;
            _HATEOAS = new HATEOAS.HATEOAS("localhost:5001/api/v1/Politico");
            _HATEOAS.AddAction("GET_INFO", "GET");
            _HATEOAS.AddAction("DELETAR_POLITICO", "DELETE");
            _HATEOAS.AddAction("EDITAR_POLITICO", "PATCH");
        }

        [HttpGet]
        public IActionResult Get()
        {
            var politicos = _database.Politicos.Where(x => x.Deletado == false).ToList();

            List<PoliticoContainer> politicosHATEOAS = new List<PoliticoContainer>();
            foreach (var politico in politicos)
            {
                PoliticoContainer politicoHATEOAS = new PoliticoContainer();
                politicoHATEOAS.Politico = politico;
                politicoHATEOAS.Links = _HATEOAS.GetActions(politico.Id.ToString());
                politicosHATEOAS.Add(politicoHATEOAS);
            }
            return Ok(politicosHATEOAS);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var politico = _database.Politicos.First(x => x.Deletado == false && x.Id == id);

                PoliticoContainer politicoHATEOAS = new PoliticoContainer();
                politicoHATEOAS.Politico = politico;
                politicoHATEOAS.Links = _HATEOAS.GetActions(politico.Id.ToString());
                return Ok(politicoHATEOAS);

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
        public IActionResult GetAsc()
        {
            var politicos = _database.Politicos.Where(x => x.Deletado == false).ToList();

            List<PoliticoContainer> politicosHATEOAS = new List<PoliticoContainer>();
            foreach (var politico in politicos)
            {
                PoliticoContainer politicoHATEOAS = new PoliticoContainer();
                politicoHATEOAS.Politico = politico;
                politicoHATEOAS.Links = _HATEOAS.GetActions(politico.Id.ToString());
                politicosHATEOAS.Add(politicoHATEOAS);
            }
            Comparison<PoliticoContainer> comp = (p1, p2) => p1.Politico.Nome.CompareTo(p2.Politico.Nome);
            politicosHATEOAS.Sort(comp);
            return Ok(politicosHATEOAS);
        }

        [HttpGet]
        [Route("Desc")]
        public IActionResult GetDesc()
        {
            var politicos = _database.Politicos.Where(x => x.Deletado == false).ToList();

            List<PoliticoContainer> politicosHATEOAS = new List<PoliticoContainer>();
            foreach (var politico in politicos)
            {
                PoliticoContainer politicoHATEOAS = new PoliticoContainer();
                politicoHATEOAS.Politico = politico;
                politicoHATEOAS.Links = _HATEOAS.GetActions(politico.Id.ToString());
                politicosHATEOAS.Add(politicoHATEOAS);
            }
            politicosHATEOAS.Sort((p1, p2) => p1.Politico.Nome.CompareTo(p2.Politico.Nome));
            politicosHATEOAS.Reverse();
            return Ok(politicosHATEOAS);
        }

        [HttpPatch]
        public async Task<IActionResult> Patch([FromBody] PoliticoDto politicoDto)
        {
            Politico dtPolitico = _database.Politicos.FirstOrDefault(x => x.Id == politicoDto.Id);
            if (dtPolitico != null)
            {
                try
                {
                    var cpfIsValid = Validate(politicoDto.Cpf);
                    if (cpfIsValid)
                    {
                        //Preparando o telefone
                        List<Telefone> telefones = new List<Telefone>();
                        foreach (var telefone in politicoDto.Telefone)
                        {
                            Telefone tel = new Telefone();
                            tel.Numero = telefone.Numero;
                            tel.Tipo = telefone.Tipo;
                            tel.Deletado = telefone.Deletado;
                            telefones.Add(tel);
                        }

                        //Preparando o endereço
                        Endereco enderecoConvertido = new Endereco();
                        if (politicoDto.Endereco != null)
                        {
                            enderecoConvertido.Cep = politicoDto.Endereco.Cep;
                            enderecoConvertido.Cidade = politicoDto.Endereco.Cidade;
                            enderecoConvertido.Estado = politicoDto.Endereco.Estado.ToUpper();
                            enderecoConvertido.Rua = politicoDto.Endereco.Rua;
                            enderecoConvertido.Bairro = politicoDto.Endereco.Bairro;
                        }
                        var dbPartido = _database.Partidos.FirstOrDefault(x => x.Id == politicoDto.PartidoId);
                        var dbRepresentante = _database.Representantes.FirstOrDefault(x => x.Id == politicoDto.RepresentanteId);

                        dtPolitico.Nome = politicoDto.Nome != null ? politicoDto.Nome : dtPolitico.Nome;
                        dtPolitico.Cpf = politicoDto.Cpf != null ? politicoDto.Cpf : dtPolitico.Cpf;
                        dtPolitico.Cargo = politicoDto.Cargo != dtPolitico.Cargo ? politicoDto.Cargo : dtPolitico.Cargo;
                        dtPolitico.Endereco = enderecoConvertido.Cep != null ? enderecoConvertido : dtPolitico.Endereco;
                        dtPolitico.Telefone = telefones.Count > 0 ? telefones : dtPolitico.Telefone;
                        dtPolitico.Partido = dbPartido != null ? dbPartido : dtPolitico.Partido;
                        dtPolitico.Representante = dbRepresentante != null ? dbRepresentante : dtPolitico.Representante;
                        //Salvando a foto
                        if (politicoDto.Foto != null)
                        {
                            var base64str = politicoDto.Foto;
                            var nomeFoto = (politicoDto.Cpf + politicoDto.Nome + DateTime.Now.ToString("yyMMddHHmmssfff"));
                            var nomeFormatado = Regex.Replace(nomeFoto, "[^0-9a-zA-Z]+", "");

                            IFormFile iformImg = FotoService.Base64ToImage(base64str, nomeFormatado);

                            dtPolitico.Foto = await FotoService.UploadFoto(_environment, iformImg);
                        }
                        _database.SaveChanges();
                        return Ok("Politico atualizado");
                    }
                    else
                    {
                        return BadRequest(new ErrorResponse()
                        {
                            Errors = new List<string>()
                        {
                            "CPF inválido."
                        }
                        });
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
            else
            {
                return BadRequest(new ErrorResponse()
                {
                    Errors = new List<string>()
                        {
                            "Político não encontrado."
                        }
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PoliticoDto politicoDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Politico dtPolitico = new Politico();
                    var cpfIsValid = Validate(politicoDto.Cpf);
                    var jaRegistrado = _database.Politicos.FirstOrDefault(x => x.Cpf == politicoDto.Cpf && x.Deletado == false);
                    if (cpfIsValid && jaRegistrado == null)
                    {
                        //Preparando o telefone
                        List<Telefone> telefones = new List<Telefone>();
                        foreach (var telefone in politicoDto.Telefone)
                        {
                            Telefone tel = new Telefone();
                            tel.Numero = telefone.Numero;
                            tel.Tipo = telefone.Tipo;
                            tel.Deletado = telefone.Deletado;
                            telefones.Add(tel);
                        }

                        //Preparando o endereço
                        Endereco enderecoConvertido = new Endereco();
                        enderecoConvertido.Cep = politicoDto.Endereco.Cep;
                        enderecoConvertido.Cidade = politicoDto.Endereco.Cidade;
                        enderecoConvertido.Estado = politicoDto.Endereco.Estado.ToUpper();
                        enderecoConvertido.Rua = politicoDto.Endereco.Rua;
                        enderecoConvertido.Bairro = politicoDto.Endereco.Bairro;

                        dtPolitico.Nome = politicoDto.Nome;
                        dtPolitico.Cpf = politicoDto.Cpf;
                        dtPolitico.Cargo = politicoDto.Cargo;
                        dtPolitico.Endereco = enderecoConvertido;
                        dtPolitico.Telefone = telefones;
                        dtPolitico.Partido = _database.Partidos.First(x => x.Id == politicoDto.PartidoId);
                        dtPolitico.Representante = _database.Representantes.First(x => x.Id == politicoDto.RepresentanteId);

                        //Salvando a foto
                        var base64str = politicoDto.Foto;
                        var nomeFoto = (politicoDto.Cpf + politicoDto.Nome + DateTime.Now.ToString("yyMMddHHmmssfff"));
                        var nomeFormatado = Regex.Replace(nomeFoto, "[^0-9a-zA-Z]+", "");

                        IFormFile iformImg = FotoService.Base64ToImage(base64str, nomeFormatado);
                        dtPolitico.Foto = await FotoService.UploadFoto(_environment, iformImg);

                        _database.Politicos.Add(dtPolitico);
                        _database.SaveChanges();

                        return Ok("Novo político cadastrado.");
                    }
                    else
                    {
                        return BadRequest(new ErrorResponse()
                        {
                            Errors = new List<string>()
                        {
                            "CPF inválido ou já está cadastrado como político."
                        }
                        });
                    }
                }
                else
                {
                    return BadRequest(new ErrorResponse()
                    {
                        Errors = new List<string>()
                        {
                            "Formato de político inválido."
                        }
                    });
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

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Politico politico = _database.Politicos.First(x => x.Id == id);
                if (politico != null)
                {
                    politico.Deletado = true;
                    _database.SaveChanges();
                    return Ok("Registro deletado.");
                }
                else
                {
                    return BadRequest(new ErrorResponse()
                    {
                        Errors = new List<string>()
                        {
                            "Político não encontrado."
                        }
                    });
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

        public class PoliticoContainer
        {
            public Politico Politico { get; set; }
            public Link[] Links { get; set; }
        }

        // [HttpPost("upload")]
        // public async Task<string> EnviaArquivo([FromForm] IFormFile arquivo)
        // {
        //     if (arquivo.Length > 0)
        //     {
        //         try
        //         {
        //             if (!Directory.Exists(_environment.WebRootPath + "\\imagens\\"))
        //             {
        //                 Directory.CreateDirectory(_environment.WebRootPath + "\\imagens\\");
        //             }
        //             using (FileStream filestream = System.IO.File.Create(_environment.WebRootPath + "\\imagens\\" + arquivo.FileName))
        //             {
        //                 await arquivo.CopyToAsync(filestream);
        //                 filestream.Flush();
        //                 return "\\imagens\\" + arquivo.FileName;
        //             }
        //         }
        //         catch (Exception ex)
        //         {
        //             return ex.ToString();
        //         }
        //     }
        //     else
        //     {
        //         return "Ocorreu uma falha no envio do arquivo...";
        //     }
        // }
    }
}