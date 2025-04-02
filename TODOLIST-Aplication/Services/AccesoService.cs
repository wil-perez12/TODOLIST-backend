using TODO.ContextDB;
using TODO.Interfaces;
using TODO.Models.Dtos;
using TODO.Helpers;
using Models.Entidades;
using Microsoft.EntityFrameworkCore;
using TODOLIST_Infraestructure.Helpers;


namespace TODO.Services
{
    public class AccesoService : IAccesible
    {
        private readonly TodoContext _context;
        private readonly EncriptarHelper _encriptarHelper;
        private readonly TokenHelper _tokenHelper;
        private readonly ValidarTokenHelper _ValidarTokenHelper;

        public AccesoService(TodoContext context, EncriptarHelper encriptarHelper, TokenHelper tokenHelper, ValidarTokenHelper ValidartokenHelper)
        {
            this._context = context;
            this._encriptarHelper = encriptarHelper;
            this._tokenHelper = tokenHelper;
            this._ValidarTokenHelper = ValidartokenHelper;
        }

        //retorna la lista de users en la base de datos
        public async Task<List<Usuario>> GetUsuario()
        {
            return await _context.Usuario.ToListAsync();
        }

        //retorna el modelo por el correo y contraseña que se encuenta encriptada
        public async Task<string> Login(LoginDTO modelo)
        {
            var contrasenaEncriptada = _encriptarHelper.Encriptar(modelo.Contrasena!);
            var BuscarUsuario = await _context.Usuario
                                .FirstOrDefaultAsync(x => x.Correo == modelo.Correo
                                 && x.Contrasena == contrasenaEncriptada);

            if (BuscarUsuario == null)
                return null!;

            //retorna el usuario autenticado con el token
            return _tokenHelper.TokenJwt(BuscarUsuario);
        }

        //retorna un usuario que no se encuentra en la tabla usuarios de la db
        public async Task<Usuario?> Registro(RegistroDTO modelo)
        {
            // Verifica si el usuario ya existe en la DB y si no esta lo registra
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


        //metodo para validar el token
        public bool validarToken(string token)
        {
           bool valid = _ValidarTokenHelper.ValidarToken(token);
           return valid;
        }
    }
}
