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

        [HttpGet("Usuarios")]

        [SwaggerOperation(
        Summary = "Lista de usurios",
        Description = "Devuelve una lista de usuarios"
        )]
        [SwaggerResponse(200, "Devuelve la lista")]
        public async Task<IActionResult> GetUsuarios()
        {
            var lista = await _Iacceso.GetUsuario();

            return Ok(lista);

        }


        [HttpPost("Login")]

        [SwaggerOperation(
        Summary = "Obtiene correo y contraseña",
        Description = "Devuelve un jeson web token"
        )]
        [SwaggerResponse(200, "Devuelve el token")]
        [SwaggerResponse(400, "Devuelve: Credenciales Incorrectas")]
        public async Task<IActionResult> Login([FromBody] LoginDTO modelo)
        {
            var token = await _Iacceso.Login(modelo);

            if (token == null)
                return BadRequest(new { mensaje = "Credenciales Incorrectas" });

            return Ok(new { Token = token });
        }


        //endpoint para registrar el usuario en la app
        [HttpPost("Registro")]

        [SwaggerOperation(
        Summary = "Obtiene nombre, apellido, correo y contraseña",
        Description = "registra el usuario en la base de datos"
        )]
        [SwaggerResponse(200, "Devuelve: Usuario registrado correctamente")]
        [SwaggerResponse(400, "Devuelve: El usuario ya existe o hubo un error en el registro")]
        public async Task<IActionResult> Registro([FromBody] RegistroDTO modelo)
        {

            var registro = await _Iacceso.Registro(modelo);

            if (registro == null)
                return BadRequest(new { mensaje = "El usuario ya existe o hubo un error en el registro" });
            else
                return Ok(new { mensaje = "Usuario registrado correctamente." });
        }

    }
}
