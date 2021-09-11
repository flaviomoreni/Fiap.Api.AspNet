using Fiap.Api.AspNet.Data;
using Fiap.Api.AspNet.Model;
using Fiap.Api.AspNet.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Fiap.Api.AspNet.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        [HttpPost]
        public ActionResult<dynamic> Index([FromBody] UsuarioModel usuarioRequest , [FromServices] DataContext dataContext )
        {
            var usuarioModel = dataContext.Usuario.AsNoTracking()
                    .Where(
                        u => u.NomeUsuario == usuarioRequest.NomeUsuario && 
                        u.Senha == usuarioRequest.Senha)
                    .SingleOrDefault();

            if (usuarioModel == null) { 
                return Unauthorized();
            }

            string token = AuthenticationService.GetToken(usuarioModel);

            var retorno = new
            {
                usuario = usuarioModel,
                token = token
            };

            return Ok(retorno);
        }
    }
}
