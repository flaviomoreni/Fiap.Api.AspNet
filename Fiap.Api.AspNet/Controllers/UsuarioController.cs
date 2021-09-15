using Fiap.Api.AspNet.Model;
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
    public class UsuarioController : ControllerBase
    {

        [HttpGet]
        public ActionResult<IList<UsuarioModel>> GetAll(
            [FromServices] IUsuarioRepository usuarioRepository)
        {
            var usuario = usuarioRepository.FindAll();

            if (usuario.Count == 0)
            {
                return NoContent();
            }

            return Ok(usuario);
        }

        [HttpGet("{id:int}")]
        public ActionResult<UsuarioModel> GetById(
            [FromRoute] int id,
            [FromServices] IUsuarioRepository usuarioRepository)
        {
            var usuario = usuarioRepository.FindById(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        //[HttpGet("{name}")]
        //public ActionResult<UsuarioModel> GetByName(
        //    [FromRoute] string name,
        //    [FromServices] IUsuarioRepository usuarioRepository)
        //{
        //    var usuario = usuarioRepository.FindByName(name);

        //    if (usuario == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(usuario);
        //}

        //[HttpGet("{regra}")]
        //public ActionResult<IList<UsuarioModel>> GetByRegra(
        //    [FromRoute] string regra,
        //    [FromServices] IUsuarioRepository usuarioRepository)
        //{
        //    var usuario = usuarioRepository.FindByRegra(regra);

        //    if (usuario.Count == 0)
        //    {
        //        return NoContent();
        //    }

        //    return Ok(usuario);
        //}

        [HttpPost]
        public ActionResult<UsuarioModel> Post(
            [FromServices] IUsuarioRepository usuarioRepository,
            [FromBody] UsuarioModel usuarioModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var usuarioId = usuarioRepository.Insert(usuarioModel);
                usuarioModel.UsuarioId = usuarioId;

                var location = new Uri(Request.GetEncodedUrl() + usuarioId);

                return Created(location, usuarioModel);
            }
            catch (Exception error)
            {
                return BadRequest(new { message = $"Não foi possível inserir a marca. Detalhes: {error.Message}" });
            }
        }

        [HttpPut("{id:int}")]
        public ActionResult<UsuarioModel> Put(
            [FromRoute] int id,
            [FromServices] IUsuarioRepository usuarioRepository,
            [FromBody] UsuarioModel usuarioModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (usuarioModel.UsuarioId != id)
            {
                return NotFound();
            }

            try
            {
                usuarioRepository.Update(usuarioModel);

                return Ok(usuarioModel);
            }
            catch (Exception error)
            {
                return BadRequest(new { message = $"Não foi possível alterar a marca. Detalhes: {error.Message}" });
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult<UsuarioModel> Delete(
            [FromRoute] int id,
            [FromServices] IUsuarioRepository usuarioRepository)
        {
            usuarioRepository.Delete(id);

            return Ok();
        }
    }
}
