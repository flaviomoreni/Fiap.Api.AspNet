using Microsoft.AspNetCore.Mvc;
using System;

namespace Fiap.Api.AspNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcaController : ControllerBase
    {

        [HttpGet]
        public String Get()
        {
            return "Marca GET";
        }

        [HttpGet("{id:int}")]
        //[Route("{id:int")]
        public String GetById(int id)
        {
            return $"Marca GetById {id}";
        }


    }
}
