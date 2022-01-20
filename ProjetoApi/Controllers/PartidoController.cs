using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class PartidoController : ControllerBase
    {
        private readonly ApplicationDbContext _database;
        private HATEOAS.HATEOAS _HATEOAS;

        public PartidoController(ApplicationDbContext database)
        {
            _database = database;
            _HATEOAS = new HATEOAS.HATEOAS("localhost:5001/api/v1/Partido");
            _HATEOAS.AddAction("GET_INFO", "GET");
            _HATEOAS.AddAction("DELETAR_PARTIDO", "DELETE");
            _HATEOAS.AddAction("EDITAR_PARTIDO", "PATCH");
        }

        [HttpGet]
        public IActionResult Get()
        {
            var partidos = _database.Partidos.Where(x => x.Deletado == false).ToList();
            List<PartidoContainer> PartidosHATEOAS = new List<PartidoContainer>();
            foreach (var partido in partidos)
            {
                PartidoContainer partidoHATEOAS = new PartidoContainer();
                partidoHATEOAS.Partido = partido;
                partidoHATEOAS.Links = _HATEOAS.GetActions(partido.Id.ToString());
                PartidosHATEOAS.Add(partidoHATEOAS);
            }
            return Ok(PartidosHATEOAS);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var partido = _database.Partidos.First(x => x.Deletado == false && x.Id == id);
                PartidoContainer partidoHATEOAS = new PartidoContainer();
                partidoHATEOAS.Partido = partido;
                partidoHATEOAS.Links = _HATEOAS.GetActions(partido.Id.ToString());
                return Ok(partidoHATEOAS);
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
        public IActionResult Post([FromBody] PartidoDto partidoDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Partido dbPartido = new Partido();
                    dbPartido.Nome = partidoDto.Nome;
                    dbPartido.Sigla = partidoDto.Sigla;
                    dbPartido.Representante = _database.Representantes.First(x => x.Id == partidoDto.Id);
                    dbPartido.Deletado = false;
                    _database.Partidos.Add(dbPartido);
                    _database.SaveChanges();

                    return Ok("Partido criado.");
                }
                else
                {
                    return BadRequest(new ErrorResponse()
                    {
                        Errors = new List<string>()
                        {
                            "Formato de partido inválido."
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
        public IActionResult Patch([FromBody] PartidoDto partidoDto)
        {
            try
            {
                Partido dbPartido = _database.Partidos.First(x => x.Id == partidoDto.Id);
                if (dbPartido != null)
                {
                    var dbRepresentante = _database.Representantes.FirstOrDefault(x => x.Id == partidoDto.RepresentanteId);
                    dbPartido.Nome = partidoDto.Nome != null ? partidoDto.Nome : dbPartido.Nome;
                    dbPartido.Sigla = partidoDto.Sigla != null ? partidoDto.Sigla : dbPartido.Sigla;
                    dbPartido.Representante =  dbRepresentante != null ? dbRepresentante : dbPartido.Representante;
                    _database.SaveChanges();
                    return Ok("Partido atualizado");
                }
                else
                {
                    return BadRequest(new ErrorResponse()
                    {
                        Errors = new List<string>()
                        {
                            "Partido não encontrado."
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
                Partido partido = _database.Partidos.First(x => x.Id == id);
                if (partido != null)
                {
                    partido.Deletado = true;
                    _database.SaveChanges();
                    return Ok("Registro deletado.");
                }
                else
                {
                    return BadRequest(new ErrorResponse()
                    {
                        Errors = new List<string>()
                        {
                            "Partido não encontrado."
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

    public class PartidoContainer
    {
        public Partido Partido { get; set; }
        public Link[] Links { get; set; }
    }
}