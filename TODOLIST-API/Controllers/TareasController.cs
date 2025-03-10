using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetTareas()
        {
            var lista = await _servicio.GetTarea();
            return Ok(lista);
        }

        [HttpGet("TareasPor/{id}")]
        public async Task<IActionResult> GetTareaById(int id)
        {
            var lista = await _servicio.GetTareaById(id);

            if (lista != null)
                return Ok(lista);

            else
                return BadRequest("Tarea no encontrada con este ID");
        }

        [HttpGet("TareasBy/{estado}")]
        public async Task<IActionResult> GetTareaById(string estado)
        {
            var lista = await _servicio.GetTareaByEstado(estado);

            if (lista != null)
                return Ok(lista);

            else
                return BadRequest("Tarea no encontrada con este estado");
        }

        [HttpPost("New/tarea")]
        public async Task<IActionResult> PostTarea(TareaDTO modelo)
        {
            var newTarea = await _servicio.PostTarea(modelo);

            if (newTarea == null)
                return StatusCode(500, "Error al crear la tarea!");

            return Ok(newTarea);

        }

        [HttpPut("update/tarea")]
        public async Task<IActionResult> PutTarea(int id, TareaDTO modelo)
        {
            var updateTarea = await _servicio.PutTarea(id, modelo);

            if (updateTarea == null)
                return BadRequest("Id incorrecto o error al actualizar la tarea");

            else
                return Ok(updateTarea);
        }

        [HttpDelete("delete")]
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
