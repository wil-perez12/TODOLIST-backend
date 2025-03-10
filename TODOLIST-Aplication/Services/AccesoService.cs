using TODO.ContextDB;
using TODO.Interfaces;
using TODO.Models.Dtos;
using TODO.Helpers;
using Models.Entidades;
using Microsoft.EntityFrameworkCore;


namespace TODO.Services
{
    public class AccesoService : IAccesible
    {
        private readonly TodoContext _context;
        private readonly EncriptarHelper _encriptarHelper;

        public AccesoService(TodoContext context, EncriptarHelper encriptarHelper)
        {
            this._context = context;
            this._encriptarHelper = encriptarHelper;
        }

        //retorna la lista de users en la base de datos
        public async Task<List<Usuario>> GetUsuario()
        {
            return await _context.Usuario.ToListAsync();
        }

        //retorna el modelo por el correo y contraseña que se encuenta encriptada
        public async Task<Usuario> Login(LoginDTO modelo)
        {
            var BuscarUsuario = await _context.Usuario
                        .FirstOrDefaultAsync(x => x.Correo == modelo.Correo
                        && x.Contrasena == _encriptarHelper.Encriptar(modelo.Contrasena!));

            return BuscarUsuario!;
        }

        //retorna un usuarioque no se encuentra en la tabla usuarios de la db
        public async Task<Usuario?> Registro(RegistroDTO modelo)
        {
            // Verifica si el usuario ya existe en la DB
            var existeUsuario = await _context.Usuario.AnyAsync(x => x.Correo == modelo.Correo);

            if (existeUsuario)
            {
                return null; 
            }

            var user = new Usuario
            {
                Nombre = modelo.Nombre,
                Apellido = modelo.Apellido,
                Correo = modelo.Correo,
                Contrasena = _encriptarHelper.Encriptar(modelo.Contrasena!)
            };

            try
            {
                await _context.Usuario.AddAsync(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al registrar usuario: {ex.Message}");
                return null;
            }
        }
    }
}
