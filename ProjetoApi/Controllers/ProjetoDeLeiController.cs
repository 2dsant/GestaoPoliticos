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

namespace ProjetoApi.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    [Authorize(Roles = "admin")]
    public class ProjetoDeLeiController : ControllerBase
    {
        private readonly ApplicationDbContext _database;
        private HATEOAS.HATEOAS _HATEOAS;

        public ProjetoDeLeiController(ApplicationDbContext database)
        {
            _database = database;
            _HATEOAS = new HATEOAS.HATEOAS("localhost:5001/api/v1/ProjetoDeLei");
            _HATEOAS.AddAction("GET_INFO", "GET");
            _HATEOAS.AddAction("DELETAR_PROJETOLEI", "DELETE");
            _HATEOAS.AddAction("EDITAR_PROJETOLEI", "PATCH");
        }

        [HttpGet]
        public IActionResult Get()
        {
            var projetos = _database.ProjetosLeis.ToList();
            List<ProjetoLeiContainer> projetosLeisHATEOAS = new List<ProjetoLeiContainer>();
            foreach (var projeto in projetos)
            {
                ProjetoLeiContainer projetoLeiHATEOAS = new ProjetoLeiContainer();
                projetoLeiHATEOAS.ProjetoLei = projeto;
                projetoLeiHATEOAS.Links = _HATEOAS.GetActions(projeto.Id.ToString());
                projetosLeisHATEOAS.Add(projetoLeiHATEOAS);
            }
            return Ok(projetosLeisHATEOAS);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var projeto = _database.ProjetosLeis.First(x => x.Deletado == false && x.Id == id);
                ProjetoLeiContainer projetoLeiHATEOAS = new ProjetoLeiContainer();
                projetoLeiHATEOAS.ProjetoLei = projeto;
                projetoLeiHATEOAS.Links = _HATEOAS.GetActions(projeto.Id.ToString());
                return Ok(projetoLeiHATEOAS);
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
        public IActionResult Post([FromBody] ProjetoLeiDto projetoLeiDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ProjetoLei dtProjetoLei = new ProjetoLei();
                    dtProjetoLei.Nome = projetoLeiDto.Nome;
                    dtProjetoLei.TiposProjetosLei = projetoLeiDto.TiposProjetosLei;
                    dtProjetoLei.Ementa = projetoLeiDto.Ementa;
                    dtProjetoLei.Autor = _database.Politicos.First(x => x.Id == projetoLeiDto.AutorId);
                    dtProjetoLei.StatusProjetoLei = projetoLeiDto.StatusProjetoLei;
                    dtProjetoLei.Deletado = false;
                    _database.ProjetosLeis.Add(dtProjetoLei);
                    _database.SaveChanges();

                    return Ok("Projeto de lei criado.");
                }
                else
                {
                    return BadRequest(new ErrorResponse()
                    {
                        Errors = new List<string>()
                        {
                            "Formato inválido."
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
        public IActionResult Patch([FromBody] ProjetoLeiDto projetoLeiDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dtProjetoLei = _database.ProjetosLeis.First(x => x.Id == projetoLeiDto.Id);
                    var dbAutor = _database.Politicos.FirstOrDefault(x => x.Id == projetoLeiDto.AutorId);

                    dtProjetoLei.Nome = projetoLeiDto.Nome != null ? projetoLeiDto.Nome : dtProjetoLei.Nome;
                    dtProjetoLei.TiposProjetosLei = projetoLeiDto.TiposProjetosLei;
                    dtProjetoLei.Ementa = projetoLeiDto.Ementa != null ? projetoLeiDto.Ementa : dtProjetoLei.Ementa;
                    dtProjetoLei.Autor = dbAutor != null ? dbAutor : dtProjetoLei.Autor;
                    dtProjetoLei.StatusProjetoLei = projetoLeiDto.StatusProjetoLei;
                    dtProjetoLei.Deletado = projetoLeiDto.Deletado;
                    _database.SaveChanges();

                    return Ok("Projeto de lei atualizado.");
                }
                else
                {
                    return BadRequest(new ErrorResponse()
                    {
                        Errors = new List<string>()
                        {
                            "Formato inválido."
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
                var projeto = _database.ProjetosLeis.First(x => x.Id == id);
                if (projeto != null)
                {
                    projeto.Deletado = true;
                    _database.SaveChanges();
                    return Ok("Projeto excluído.");
                }
                else
                {
                    return BadRequest(new ErrorResponse()
                    {
                        Errors = new List<string>()
                        {
                            "Projeto de lei não encontrado"
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
    public class ProjetoLeiContainer
    {
        public ProjetoLei ProjetoLei { get; set; }
        public Link[] Links { get; set; }
    }
}