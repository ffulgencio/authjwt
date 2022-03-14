
using System.Collections.Generic;
using authjwt.Helpers;
using authjwt.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace authjwt.Controllers{

    [ApiController]
    [Route("api/[controller]")]
    public class PersonasController: ControllerBase
    {
        private readonly IConfiguration conf;

        public PersonasController(IConfiguration conf)
        {
            this.conf = conf;
        }

        [HttpGet]
        public IEnumerable<object> GetAll(){
            var personas = new List<object>(){
                new {nombre="Paul", estatura= 1.70,nacionalidad="MEX"},
                new {nombre="Juan", estatura= 1.90,nacionalidad="MEX"},
                new {nombre="Pedro", estatura= 1.70,nacionalidad="CL"},
            };

            return personas;
        }

        [HttpPost]
        public ActionResult<object> Login([FromBody] Persona persona){
            var jwtHelper = new JWTHelper(conf.GetValue<string>("secret"));
            var token = jwtHelper.CreateToken(persona.Usuario);

            return Ok(new {
                ok = true,
                msg= "Login success!"
            });
            
        }
    }
}