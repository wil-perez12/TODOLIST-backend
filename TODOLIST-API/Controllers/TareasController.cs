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

        // lista de todas las tareas
        [HttpGet("ListaTareas")]

        [SwaggerOperation(
        Summary = "Lista de tareas",
        Description = "Devuelve una lista de tareas"
        )]
        [SwaggerResponse(200, "Devuelve la lista, succes: true")]
        [SwaggerResponse(401, "no autorizado si no estas autenticado")]
        public async Task<IActionResult> GetTareas()
        {
            var lista = await _servicio.GetTarea();
            return Ok(new { 
                   succes = true,
                   values = lista
            });
        }

        //lista de tareas por id
        [HttpGet("TareasPor/{id}")]

        [SwaggerOperation(
        Summary = "Lista de tareas por ID",
        Description = "obtiene el id de la tarea"
        )]
        [SwaggerResponse(200, "Devuelve la lista, succes: true y mensaje: id encontrado")]
        [SwaggerResponse(404, "Devuelve: Tarea no encontrada con este ID, succes: false, values: null")]
        [SwaggerResponse(401, "no autorizado si no estas autenticado")]
        public async Task<IActionResult> GetTareaById(int id)
        {
            var lista = await _servicio.GetTareaById(id);

            if (lista != null)
                return Ok(new
                {
                    succes = true,
                    mensaje = "Id encontrado",
                    values = lista

                });

            return NotFound(new
            {
                succes = false,
                mensaje = "Tarea no encontrada con este id",
                values = null as object
            });
        }

        //lista de tareas por el estado
        [HttpGet("TareasBy/{estado}")]

        [SwaggerOperation(
        Summary = "Lista de tareas por estado",
        Description = "obtiene el estado de la tarea"
        )]
        [SwaggerResponse(200, "Devuelve la lista, succes: true y mensaje: id encontrado")]
        [SwaggerResponse(404, "Devuelve: Tarea no encontrada con este estado, succes: false, values: null")]
        [SwaggerResponse(401, "no autorizado si no estas autenticado")]
        public async Task<IActionResult> GetTareaById(string estado)
        {
            var lista = await _servicio.GetTareaByEstado(estado);

            if (lista != null)
                return Ok(new { 
                       succes = true,
                       mensaje = "Id encontrado",
                       values = lista
                
                });

                return NotFound(new { 
                        succes = false,
                        mensaje = "Tarea no encontrada con este estado",
                        values = null as object
                });
        }

        //Lista de tareas por id
        [HttpGet("TareasBy/Usuario/{id}")]

        [SwaggerOperation(
        Summary = "Lista de tareas por id de usuario",
        Description = "obtiene el id del usuario por tarea"
        )]
        [SwaggerResponse(200, "Devuelve la lista, id encontrado y  succes:true")]
        [SwaggerResponse(404, "Devuelve: Tarea no encontrada con este id usuario, succes: false")]
        [SwaggerResponse(401, "no autorizado si no estas autenticado")]
        public async Task<IActionResult> GetTareaByIdUser(int id)
        {
            var lista = await _servicio.GetTareaByIdUsuario(id);

            if (lista != null)
                return Ok(new { 
                    succes = true,
                    mensaje = "Id encontrado",
                    values = lista
                });

            return NotFound(new { 
                   succes = false,
                   mensaje = "Tarea no encontrada con este id usuario",
                   values = null as object
            });
        }

        //Crear Tarea
        [HttpPost("New/tarea")]

        [SwaggerOperation(
        Summary = "Crear Tarea",
        Description = "obtiene el titulo, descripcion, estado y ID-usuario"
        )]
        [SwaggerResponse(200, "Devuelve la tarea creada, succes: true")]
        [SwaggerResponse(500, "Error al crear la tarea, succes: false")]
        [SwaggerResponse(401, "no autorizado si no estas autenticado")]
        public async Task<IActionResult> PostTarea(TareaDTO modelo)
        {
            var newTarea = await _servicio.PostTarea(modelo);

            if (newTarea == null)
                return StatusCode(500, new
                {
                    success = false,
                    mensaje = "Error al crear la tarea",
                    values = null as object
                });


            return Ok(new
            {
                success = true,
                mensaje = "Creación exitosa",
                values = newTarea
            });

        }

        //Actualizar tarea
        [HttpPut("update/tarea/{id}")]

        [SwaggerOperation(
        Summary = "Modificar Tarea",
        Description = "obtiene el id, titulo, descripcion, estado y ID-usuario"
        )]
        [SwaggerResponse(200, "Devuelve la tarea modificada, succes: true")]
        [SwaggerResponse(404, "Id incorrecto o error al actualizar la tarea, succes: false y values: null")]
        [SwaggerResponse(401, "no autorizado si no estas autenticado")]
        public async Task<IActionResult> PutTarea(int id, TareaDTO modelo)
        {
            var updateTarea = await _servicio.PutTarea(id, modelo);

            if (updateTarea == null)
                return NotFound(new {
                    success = false,
                    mensaje ="Id incorrecto o error al actualizar la tarea",
                    values = null as object
                });


            return Ok(new
            {
                success = true,
                mensaje = "Modificación exitosa",
                values = updateTarea
            });
        }

        //Eliminar tarea por su id
        [HttpDelete("delete/{id}")]

        [SwaggerOperation(
        Summary = "Eliminar Tarea",
        Description = "obtiene el id de la tarea"
        )]
        [SwaggerResponse(200, "Tarea con ID eliminada!,succes: true")]
        [SwaggerResponse(404, "Id no encontrado, succes: false")]
        [SwaggerResponse(401, "no autorizado si no estas autenticado")]
        public async Task<IActionResult> DeleteTarea(int id)
        {
            var extisteID = await _servicio.DeleteTarea(id);

            if (!extisteID)
                return NotFound(new { 
                    succes = false,
                    mensaje = "Id no encontrado"
                });
   
                return Ok(new {
                    succes = true,
                    mensaje = $"Tarea con ID: {id}, eliminada!" 
                });

        }
    }
}
