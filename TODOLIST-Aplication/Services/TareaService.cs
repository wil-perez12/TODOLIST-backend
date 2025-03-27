using Microsoft.EntityFrameworkCore;
using Models.Entidades;
using TODO.ContextDB;
using TODO.Interfaces;
using TODO.Models.Dtos;

namespace TODO.Services
{
    public class TareaService : ITareas
    {
        private readonly TodoContext _context;

        public TareaService(TodoContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteTarea(int id)
        {
            var tarea = await _context.Tareas.FirstOrDefaultAsync(t => t.Id == id);

            if (tarea == null)
            {
                return false; 
            }

            _context.Tareas.Remove(tarea);
            await _context.SaveChangesAsync();

            return true; 
        }

        public async Task<List<Tareas>> GetTarea()
        {
            return await _context.Tareas.ToListAsync();
        }

        public async Task<List<Tareas>> GetTareaByEstado(string estado)
        {
            var tareaEstado = await _context.Tareas.Where(x => x.Estado == estado)
                                                    .ToListAsync();

            return tareaEstado;
        }

        public async Task<Tareas> GetTareaById(int id)
        {
            var tarea = await _context.Tareas 
                        .FirstOrDefaultAsync(x => x.Id == id);

            return tarea!;
        }

        public async Task<List<Tareas>> GetTareaByIdUsuario(int id)
        {

            var tareaIdUser = await _context.Tareas.Where(x => x.IdUsuario == id)
                                                   .ToListAsync(); 

            return tareaIdUser;
        }

        public async Task<Tareas> PostTarea(TareaDTO modelo)
        {
            try
            {
                var Tarea = new Tareas
                {
                    Titulo = modelo.Titulo,
                    Descripcion = modelo.Descripcion,
                    Estado = modelo.Estado,
                    IdUsuario = modelo.IdUsuario
                };

                await _context.AddAsync(Tarea);
                await _context.SaveChangesAsync();
                return Tarea;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear tarea: {ex.Message}");
                return null!;
            }
        }
        public async Task<Tareas> PutTarea(int id, TareaDTO modelo)
        {
            var tareaExistente = await _context.Tareas.FindAsync(id);

            if (tareaExistente == null)
                return null!; 

            tareaExistente.Titulo = modelo.Titulo;
            tareaExistente.Descripcion = modelo.Descripcion;
            tareaExistente.Estado = modelo.Estado;

            _context.Tareas.Update(tareaExistente);
            await _context.SaveChangesAsync();

            return tareaExistente;
        }
    }
}
