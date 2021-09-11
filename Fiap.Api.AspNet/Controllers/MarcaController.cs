using Fiap.Api.AspNet.Models;
using Fiap.Api.AspNet.Repository;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Fiap.Api.AspNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcaController : ControllerBase
    {


        [HttpGet]
        public ActionResult<IList<MarcaModel>> Get([FromServices] IMarcaRepository marcaRepository)
        {
            var lista = marcaRepository.FindAll();

            if (lista.Count == 0)
            {
                return NoContent();
            }

            return Ok(marcaRepository.FindAll());
        }


        [HttpGet("{id:int}")]
        public ActionResult<MarcaModel> GetById(int id, [FromServices] IMarcaRepository marcaRepository)
        {
            var marca = marcaRepository.FindById(id);

            if ( marca == null )
            {
                return NotFound();
            }

            return Ok(marca);
        }

        [HttpPost]
        public ActionResult<MarcaModel> Post([FromBody] MarcaModel marcaModel, [FromServices] IMarcaRepository marcaRepository)
        {
            
            if ( ! ModelState.IsValid )
            {
                return BadRequest(ModelState);
            }


            try
            {
                var idMarca = marcaRepository.Insert(marcaModel);
                marcaModel.MarcaId = idMarca;

                var location = new Uri(Request.GetEncodedUrl() + idMarca);

                return Created(location, marcaModel);

            } catch (Exception e)
            {
                return BadRequest(new { message = $"Não foi possível inserir a marca. Detalhe:  {e.Message} !" });
            }

        }


        [HttpPut("{id:int}")]
        public ActionResult<MarcaModel> Put([FromRoute] int id, [FromBody] MarcaModel marcaModel, [FromServices] IMarcaRepository marcaRepository)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if ( id != marcaModel.MarcaId)
            {
                return NotFound();
            }


            try
            {
                marcaRepository.Update(marcaModel);
                return Ok(marcaModel);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = $"Não foi possível alterar a marca. Detalhe:  {e.Message} !" });
            }

        }


        [HttpDelete("{id:int}")]
        public ActionResult Delete([FromRoute] int id, [FromServices] IMarcaRepository marcaRepository)
        {
            marcaRepository.Delete(id);
            return Ok();
        }


    }
}
