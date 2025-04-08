using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TODO.Interfaces;
using TODO.Models.Dtos;



namespace TODO.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
 
    public class AccesoController : ControllerBase
    {
        private readonly IAccesible _Iacceso;

        public AccesoController(IAccesible iacceso)
        {
            _Iacceso = iacceso;
        }

        //lista de usuarios registrados
        [HttpGet("Usuarios")]

        [SwaggerOperation(
        Summary = "Lista de usurios",
        Description = "Devuelve una lista de usuarios"
        )]
        [SwaggerResponse(200, "Devuelve la lista, succes:true")]
        public async Task<IActionResult> GetUsuarios()
        {
            var lista = await _Iacceso.GetUsuario();

            return Ok(new
            {
                succes = true,
                values = lista
            });

        }

        // el usuario se logea via este endpoint
        [HttpPost("Login")]

        [SwaggerOperation(
        Summary = "Obtiene correo y contraseña",
        Description = "Devuelve un jeson web token"
        )]
        [SwaggerResponse(200, "Devuelve el token, succes: true y Token: el token")]
        [SwaggerResponse(400, "Devuelve: Credenciales Incorrectas, succes: false ")]
        public async Task<IActionResult> Login([FromBody] LoginDTO modelo)
        {
            var token = await _Iacceso.Login(modelo);

            if (token == null)
                return BadRequest(new
                {
                    succes = false,
                    mensaje = "Credenciales Incorrectas"
                });

            return Ok(new 
            { 
                succes = true,
                Token = token 
            });
        }


        //endpoint para registrar el usuario en la app
        [HttpPost("Registro")]

        [SwaggerOperation(
        Summary = "Obtiene nombre, apellido, correo y contraseña",
        Description = "registra el usuario en la base de datos"
        )]
        [SwaggerResponse(200, "Devuelve: Usuario registrado correctamente,succes: true")]
        [SwaggerResponse(400, "Devuelve: El usuario ya existe o hubo un error en el registro, succes: false")]
        public async Task<IActionResult> Registro([FromBody] RegistroDTO modelo)
        {

            var registro = await _Iacceso.Registro(modelo);

            if (registro == null)
                return BadRequest(new
                {
                    succes = false,
                    mensaje = "El usuario ya existe o hubo un error en el registro"
                });
            else
                return Ok(new
                {
                    succes = true,
                    mensaje = "Usuario registrado correctamente."
                });
        }


        // verifica que el token sea valido y no pueda ser modificado
        [HttpGet("validarToken")]

        [SwaggerOperation(
        Summary = "Obtiene el token",
        Description = "Devuelve una respuesta booleana"
        )]
        [SwaggerResponse(200, "Devuelve succes: true")]
        [SwaggerResponse(404, "Devuelve succes: false ")]
        public IActionResult ValidarToken([FromQuery] string token)
        {
            bool isCorrect = _Iacceso.validarToken(token);

            if (!isCorrect)
                return NotFound(new
                {
                    succes = false
                });

            return Ok(new
            {
                succes = true
            });
        }

        [SwaggerOperation(
        Summary = "pin de activacion",
        Description = "lanza un estatus, esto para mantener el ervidor despierto"
        )]
        [SwaggerResponse(200, "Devuelve api activa")]
        [HttpGet("pin")]
        public IActionResult Ping()
        {
            return Ok("API activa");
        }
    }
}
