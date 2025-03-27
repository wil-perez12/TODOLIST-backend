using Models.Entidades;
using TODO.Models.Dtos;

namespace TODO.Interfaces
{
    public interface ITareas
    {

        Task<List<Tareas>> GetTarea();
        Task<Tareas> GetTareaById(int id);
        Task<List<Tareas>> GetTareaByEstado(string estado);
        Task<List<Tareas>> GetTareaByIdUsuario(int id);
        Task<Tareas> PostTarea(TareaDTO modelo);
        Task<Tareas> PutTarea(int id,TareaDTO modelo);
        Task<bool> DeleteTarea(int id);
    }
}
