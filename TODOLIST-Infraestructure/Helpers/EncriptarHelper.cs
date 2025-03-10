using System.Security.Cryptography;
using System.Text;

namespace TODO.Helpers
{
    public class EncriptarHelper
    {
        //metodo para encriptar la contraseña del usuario. Utilizando Sha256
        public string Encriptar(string contrasena)
        { 
            using SHA256 sHA256 = SHA256.Create();
            byte[] bytes = sHA256.ComputeHash(Encoding.UTF8.GetBytes(contrasena));

            StringBuilder sb = new StringBuilder();
            foreach (var item in bytes)
            {
                sb.Append(item);
            }
            return sb.ToString();
        }
    }
}
