using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TODO.Interfaces;
using TODO.Models.Dtos;


namespace TODO.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class TareasController : ControllerBase
    {
        private readonly ITareas _servicio;

        public TareasController(ITareas servicio)
        {
            _servicio = servicio;
        }

        [HttpGet("ListaTareas")]


        [SwaggerOperation(
        Summary = "Lista de tareas",
        Description = "Devuelve una lista de tareas"
        )]
        [SwaggerResponse(200, "Devuelve la lista")]
        [SwaggerResponse(401, "no autorizado si no estas autenticado")]
        public async Task<IActionResult> GetTareas()
        {
            var lista = await _servicio.GetTarea();
            return Ok(lista);
        }

        [HttpGet("TareasPor/{id}")]

        [SwaggerOperation(
        Summary = "Lista de tareas por ID",
        Description = "obtiene el id de la tarea"
        )]
        [SwaggerResponse(200, "Devuelve la lista")]
        [SwaggerResponse(400, "Devuelve: Tarea no encontrada con este ID")]
        [SwaggerResponse(401, "no autorizado si no estas autenticado")]
        public async Task<IActionResult> GetTareaById(int id)
        {
            var lista = await _servicio.GetTareaById(id);

            if (lista != null)
                return Ok(lista);

            else
                return BadRequest("Tarea no encontrada con este ID");
        }

        [HttpGet("TareasBy/{estado}")]

        [SwaggerOperation(
        Summary = "Lista de tareas por estado",
        Description = "obtiene el estado de la tarea"
        )]
        [SwaggerResponse(200, "Devuelve la lista")]
        [SwaggerResponse(400, "Devuelve: Tarea no encontrada con este estado")]
        [SwaggerResponse(401, "no autorizado si no estas autenticado")]
        public async Task<IActionResult> GetTareaById(string estado)
        {
            var lista = await _servicio.GetTareaByEstado(estado);

            if (lista != null)
                return Ok(lista);

            else
                return BadRequest("Tarea no encontrada con este estado");
        }

        [HttpPost("New/tarea")]

        [SwaggerOperation(
        Summary = "Crear Tarea",
        Description = "obtiene el titulo, descripcion, estado y ID-usuario"
        )]
        [SwaggerResponse(200, "Devuelve la tarea creada")]
        [SwaggerResponse(500, "Error al crear la tarea!")]
        [SwaggerResponse(401, "no autorizado si no estas autenticado")]
        public async Task<IActionResult> PostTarea(TareaDTO modelo)
        {
            var newTarea = await _servicio.PostTarea(modelo);

            if (newTarea == null)
                return StatusCode(500, "Error al crear la tarea!");

            return Ok(newTarea);

        }

        [HttpPut("update/tarea")]

        [SwaggerOperation(
        Summary = "Modificar Tarea",
        Description = "obtiene el id, titulo, descripcion, estado y ID-usuario"
        )]
        [SwaggerResponse(200, "Devuelve la tarea modificada")]
        [SwaggerResponse(400, "Id incorrecto o error al actualizar la tarea")]
        [SwaggerResponse(401, "no autorizado si no estas autenticado")]
        public async Task<IActionResult> PutTarea(int id, TareaDTO modelo)
        {
            var updateTarea = await _servicio.PutTarea(id, modelo);

            if (updateTarea == null)
                return BadRequest("Id incorrecto o error al actualizar la tarea");

            else
                return Ok(updateTarea);
        }

        [HttpDelete("delete")]

        [SwaggerOperation(
        Summary = "Eliminar Tarea",
        Description = "obtiene el id de la tarea"
        )]
        [SwaggerResponse(200, "Tarea con ID eliminada!")]
        [SwaggerResponse(400, "Id no encontrado")]
        [SwaggerResponse(401, "no autorizado si no estas autenticado")]
        public async Task<IActionResult> DeleteTarea(int id)
        {
            var extisteID = await _servicio.DeleteTarea(id);

            if (!extisteID)
                return BadRequest("Id no encontrado");
            else
                return Ok(new { mensaje = $"Tarea con ID: {id}, eliminada!" });

        }
    }
}
