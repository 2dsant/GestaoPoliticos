using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoApi.Data;
using ProjetoApi.HATEOAS;
using ProjetoApi.Models;
using ProjetoApi.Models.DTOs;
using ProjetoApi.Models.DTOs.Responses;
using static ValidaCpf.ValidaCpfExtension;


namespace ProjetoApi.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    [Authorize(Roles = "admin")]
    public class RepresentanteController : ControllerBase
    {
        private readonly ApplicationDbContext _database;
        private HATEOAS.HATEOAS _HATEOAS;

        public RepresentanteController(ApplicationDbContext database)
        {
            _database = database;
            _HATEOAS = new HATEOAS.HATEOAS("localhost:5001/api/v1/Representante");
            _HATEOAS.AddAction("GET_INFO", "GET");
            _HATEOAS.AddAction("DELETAR_REPRESENTANTE", "DELETE");
            _HATEOAS.AddAction("EDITAR_REPRESENTANTE", "PATCH");
        }

        [HttpGet]
        public IActionResult Get()
        {
            var representantes = _database.Representantes.ToList();
            List<RepresentanteContainer> representantesHATEOAS = new List<RepresentanteContainer>();
            foreach (var representante in representantes)
            {
                RepresentanteContainer representateHATEOAS = new RepresentanteContainer();
                representateHATEOAS.Representante = representante;
                representateHATEOAS.Links = _HATEOAS.GetActions(representante.Id.ToString());
                representantesHATEOAS.Add(representateHATEOAS);
            }
            return Ok(representantesHATEOAS);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                Representante representante = _database.Representantes.First(x => x.Id == id);
                RepresentanteContainer representateHATEOAS = new RepresentanteContainer();
                representateHATEOAS.Representante = representante;
                representateHATEOAS.Links = _HATEOAS.GetActions(representante.Id.ToString());
                return Ok(representateHATEOAS);
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
        public IActionResult Post([FromBody] RepresentanteDto representanteDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var cpfIsValid = Validate(representanteDto.Cpf);
                    if (cpfIsValid)
                    {
                        List<Telefone> telefones = new List<Telefone>();
                        foreach (var telefone in representanteDto.Telefone)
                        {
                            Telefone tel = new Telefone();
                            tel.Numero = telefone.Numero;
                            tel.Tipo = telefone.Tipo;
                            tel.Deletado = telefone.Deletado;
                            telefones.Add(tel);
                        }

                        Representante dbRepresentante = new Representante();
                        dbRepresentante.Nome = representanteDto.Nome;
                        dbRepresentante.Cpf = representanteDto.Cpf;
                        dbRepresentante.Telefone = telefones;
                        dbRepresentante.Deletado = false;
                        _database.Representantes.Add(dbRepresentante);
                        _database.SaveChanges();

                        return Ok("Representante criado.");
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
                else
                {
                    return BadRequest(new ErrorResponse()
                    {
                        Errors = new List<string>()
                        {
                            "Formato de representante inválido."
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

        [HttpPatch]
        public IActionResult Patch([FromBody] RepresentanteDto representanteDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var cpfIsValid = Validate(representanteDto.Cpf);
                    if (cpfIsValid)
                    {
                        List<Telefone> telefones = new List<Telefone>();
                        foreach (var telefone in representanteDto.Telefone)
                        {
                            Telefone tel = new Telefone();
                            tel.Numero = telefone.Numero;
                            tel.Tipo = telefone.Tipo;
                            tel.Deletado = telefone.Deletado;
                            telefones.Add(tel);
                        }

                        var dbRepresentante = _database.Representantes.First(x => x.Id == representanteDto.Id);
                        dbRepresentante.Cpf = representanteDto.Cpf != null ? representanteDto.Cpf : dbRepresentante.Cpf;
                        dbRepresentante.Nome = representanteDto.Nome != null ? representanteDto.Nome : dbRepresentante.Nome;
                        dbRepresentante.Telefone = telefones.Count > 0 ? telefones : dbRepresentante.Telefone;
                        _database.SaveChanges();

                        return Ok("Representante atualizado.");
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
                else
                {
                    return BadRequest(new ErrorResponse()
                    {
                        Errors = new List<string>()
                        {
                            "Formato de representante inválido."
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
                Representante representante = _database.Representantes.First(x => x.Id == id);
                if (representante != null)
                {
                    representante.Deletado = true;
                    _database.SaveChanges();
                    return Ok("Registro deletado.");
                }
                else
                {
                    return BadRequest(new ErrorResponse()
                    {
                        Errors = new List<string>()
                        {
                            "Representante não encontrado."
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
    }

    public class RepresentanteContainer
    {
        public Representante Representante { get; set; }
        public Link[] Links { get; set; }
    }
}