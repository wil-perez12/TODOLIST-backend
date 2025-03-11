using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TODO.Helpers;
using TODO.Interfaces;
using TODO.Models;
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

        //endpoint para obtener la lista de usuarios
        [HttpGet("Usuarios")]
        public async Task<IActionResult> GetUsuarios()
        {
            var lista = await _Iacceso.GetUsuario();
            return Ok(lista);
        }

        //endpoint que retorna un jwt si el usuario esta autenticado
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO modelo)
        {
            var token = await _Iacceso.Login(modelo);

            if (token == null)
                return Unauthorized(new { mensaje = "Credenciales Incorrectas" });

            return Ok(new { Token = token });
        }


        //endpoint para registrar el usuario en la app
        [HttpPost("Registro")]
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
