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
    public class ProcessoController : ControllerBase
    {
        private readonly ApplicationDbContext _database;
        private HATEOAS.HATEOAS _HATEOAS;


        public ProcessoController(ApplicationDbContext database)
        {
            _database = database;
            _HATEOAS = new HATEOAS.HATEOAS("localhost:5001/api/v1/Processo");
            _HATEOAS.AddAction("GET_INFO", "GET");
            _HATEOAS.AddAction("DELETAR_PROCESSO", "DELETE");
            _HATEOAS.AddAction("EDITAR_PROCESSO", "PATCH");
        }

        [HttpGet]
        public IActionResult Get()
        {
            var processos = _database.Processos.ToList();
            List<ProcessoContainer> processosHATEOAS = new List<ProcessoContainer>();
            foreach (var processo in processos)
            {
                ProcessoContainer processoHATEOAS = new ProcessoContainer();
                processoHATEOAS.Processo = processo;
                processoHATEOAS.Links = _HATEOAS.GetActions(processo.Id.ToString());
                processosHATEOAS.Add(processoHATEOAS);
            }
            return Ok(processosHATEOAS);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var processo = _database.Processos.First(x => x.Id == id);
                ProcessoContainer processoHATEOAS = new ProcessoContainer();
                processoHATEOAS.Processo = processo;
                processoHATEOAS.Links = _HATEOAS.GetActions(processo.Id.ToString());
                return Ok(processoHATEOAS);
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
        public IActionResult Post([FromBody] ProcessoDto processoDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Processo dtProcesso = new Processo();
                    dtProcesso.Nome = processoDto.Nome;
                    dtProcesso.Descricao = processoDto.Descricao;
                    dtProcesso.Politico = _database.Politicos.First(x => x.Id == processoDto.PoliticoId);
                    dtProcesso.Status = processoDto.Status;
                    dtProcesso.Deletado = false;
                    _database.Processos.Add(dtProcesso);
                    _database.SaveChanges();

                    return Ok("Processo criado.");
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
                            "Formato de processo inválido."
                        }
                });
            }
        }

        [HttpPatch]
        public IActionResult Patch([FromBody] ProcessoDto processoDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var dbPolitico = _database.Politicos.FirstOrDefault(x => x.Id == processoDto.PoliticoId);

                    Processo dtProcesso = _database.Processos.First(x => x.Id == processoDto.Id);
                    dtProcesso.Nome = processoDto.Nome != null ? processoDto.Nome : dtProcesso.Nome;
                    dtProcesso.Descricao = processoDto.Descricao != null ? processoDto.Descricao : processoDto.Descricao;
                    dtProcesso.Politico = dbPolitico != null ? dbPolitico : dtProcesso.Politico;
                    dtProcesso.Status = processoDto.Status;
                    dtProcesso.Deletado = false;
                    _database.SaveChanges();

                    return Ok("Processo atualizado.");
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
                            "Formato de processo inválido."
                        }
                });
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Processo processo = _database.Processos.First(x => x.Id == id);
                if (processo != null)
                {
                    processo.Deletado = true;
                    _database.SaveChanges();
                    return Ok("Registro deletado.");
                }
                else
                {
                    return BadRequest(new ErrorResponse()
                    {
                        Errors = new List<string>()
                        {
                            "Registro não encontrado."
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

    public class ProcessoContainer
    {
        public Processo Processo { get; set; }
        public Link[] Links { get; set; }
    }
}