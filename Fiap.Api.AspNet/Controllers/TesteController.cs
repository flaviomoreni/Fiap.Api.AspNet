using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Api.AspNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class TesteController : ControllerBase
    {

        [HttpGet]
        [Route("anonimo")]
        [AllowAnonymous]
        public string Anonimo() { 
            return "Anonimo"; 
        }

        [HttpGet]
        [Route("autenticado")]
        [Authorize(Roles = "Junior, Senior, Pleno")]
        public string Autenticado() { 
            return "Autenticado"; 
        }

        [HttpGet]
        [Route("junior")]
        [Authorize(Roles = "Junior, Senior, Pleno")]
        public string Junior() { 
            return "Junior"; 
        }

        [HttpGet]
        [Route("senior")]
        [Authorize(Roles = "Senior")]
        public string Senior() { 
            return "Senior"; 
        }

    }
}
