using System.Text.Json.Serialization;

namespace Models.Entidades
{
    public class Tareas
    {
        public int Id { get; set; }
        public string? Titulo { get; set; }
        public string? Descripcion { get; set; }
        public string? Estado { get; set; }

        //relacion con la tabla usuario
        public int IdUsuario { get; set; }

        [JsonIgnore] // ignora esta propiedad al serializar
        public Usuario? Usuario { get; set; }
    }
}
