using Fiap.Api.AspNet.Models;
using Fiap.Api.AspNet.Repository.Interface;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Fiap.Api.AspNet.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [ApiVersion("3.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {

        [HttpGet]
        public ActionResult<IList<CategoriaModel>> GetAll(
            [FromServices] ICategoriaRepository categoriaRepository)
        {
            var categoria = categoriaRepository.FindAll();

            if (categoria.Count == 0)
            {
                return NoContent();
            }

            return Ok(categoria);
        }

        [HttpGet("{id:int}")]
        public ActionResult<CategoriaModel> GetById(
            [FromRoute] int id,
            [FromServices] ICategoriaRepository categoriaRepository)
        {
            var categoria = categoriaRepository.FindById(id);

            if (categoria == null)
            {
                return NotFound();
            }

            return Ok(categoria);
        }

        [HttpPost]
        public ActionResult<CategoriaModel> Post(
            [FromServices] ICategoriaRepository categoriaRepository,
            [FromBody] CategoriaModel categoriaModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var categoriaId = categoriaRepository.Insert(categoriaModel);
                categoriaModel.CategoriaId = categoriaId;

                var location = new Uri(Request.GetEncodedUrl() + categoriaId);

                return Created(location, categoriaModel);
            }
            catch (Exception error)
            {
                return BadRequest(new { message = $"Não foi possível inserir a marca. Detalhes: {error.Message}" });
            }
        }

        [HttpPut("{id:int}")]
        public ActionResult<CategoriaModel> Put(
            [FromRoute] int id,
            [FromServices] ICategoriaRepository categoriaRepository,
            [FromBody] CategoriaModel categoriaModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (categoriaModel.CategoriaId != id)
            {
                return NotFound();
            }

            try
            {
                categoriaRepository.Update(categoriaModel);

                return Ok(categoriaModel);
            }
            catch (Exception error)
            {
                return BadRequest(new { message = $"Não foi possível alterar a marca. Detalhes: {error.Message}" });
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult<CategoriaModel> Delete(
            [FromRoute] int id,
            [FromServices] ICategoriaRepository categoriaRepository)
        {
            categoriaRepository.Delete(id);

            return Ok();
        }
    }
}
