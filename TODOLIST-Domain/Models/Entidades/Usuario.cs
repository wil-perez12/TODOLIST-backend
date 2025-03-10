using System.Text.Json.Serialization;
using System.Threading;

namespace Models.Entidades
{
    public class Usuario
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Correo { get; set; }
        public string? Contrasena { get; set; }

        //Lista de tareas
        [JsonIgnore]
        public ICollection<Tareas> Tareas { get; set; }

    }
}
