using Fiap.Api.AspNet.Models;
using Fiap.Api.AspNet.Repository.Interface;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fiap.Api.AspNet.Controllers
{

    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [ApiVersion("3.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ResponseCache(VaryByHeader = "User-Agent", Location = ResponseCacheLocation.Any, Duration = 10)]
    public class MarcaController : ControllerBase
    {

        [HttpGet]
        [ApiVersion("1.0", Deprecated = true)]
        public ActionResult<IList<MarcaModel>> GetAll(
            [FromServices] IMarcaRepository marcaRepository)
        {
            var marca = marcaRepository.FindAll();

            if (marca.Count == 0)
            {
                return NoContent();
            }

            return Ok(marca);
        }



        [HttpGet]
        [ApiVersion("2.0")]
        [ApiVersion("3.0")]
        public ActionResult<dynamic> GetAllPagination(
            [FromQuery] int pagina = 0,
            [FromQuery] int tamanho = 3,
            [FromServices] IMarcaRepository marcaRepository = null)
        {

            var totalGeral = marcaRepository.Count();
            var totalPaginas = (int)Math.Ceiling((double)totalGeral / tamanho);
            var anterior = pagina > 0 ? $"marca?pagina={pagina - 1}&tamanho={tamanho}" : "";
            var proximo = pagina < totalPaginas - 1 ? $"marca?pagina={pagina + 1}&tamanho={tamanho}" : "";

            IList<MarcaModel> marcas = marcaRepository.FindAll(pagina, tamanho);

            return Ok(
                new
                {
                    Total = totalGeral,
                    TotalPaginas = totalPaginas,
                    AnteriorLink = anterior,
                    ProximoLink = proximo,
                    Marcas = marcas
                }
            );

        }


        [HttpGet("{id:int}")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public ActionResult<MarcaModel> GetById(
            [FromRoute] int id,
            [FromServices] IMarcaRepository marcaRepository)
        {
            var marca = marcaRepository.FindById(id);

            if (marca == null)
            {
                return NotFound();
            }

            return Ok(marca);
        }

        [HttpPost]
        public ActionResult<MarcaModel> Post(
            [FromServices] IMarcaRepository marcaRepository,
            [FromBody] MarcaModel marcaModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var marcaId = marcaRepository.Insert(marcaModel);
                marcaModel.MarcaId = marcaId;

                var location = new Uri(Request.GetEncodedUrl() + marcaId);

                return Created(location, marcaModel);
            }
            catch (Exception error)
            {
                return BadRequest(new { message = $"Não foi possível inserir a marca. Detalhes: {error.Message}" });
            }
        }

        [HttpPut("{id:int}")]
        public ActionResult<MarcaModel> Put(
            [FromRoute] int id,
            [FromServices] IMarcaRepository marcaRepository,
            [FromBody] MarcaModel marcaModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (marcaModel.MarcaId != id)
            {
                return NotFound();
            }

            try
            {
                marcaRepository.Update(marcaModel);

                return Ok(marcaModel);
            }
            catch (Exception error)
            {
                return BadRequest(new { message = $"Não foi possível alterar a marca. Detalhes: {error.Message}" });
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult<MarcaModel> Delete(
             [FromRoute] int id,
             [FromServices] IMarcaRepository marcaRepository)
        {
            marcaRepository.Delete(id);

            return Ok();
        }

    }
}
