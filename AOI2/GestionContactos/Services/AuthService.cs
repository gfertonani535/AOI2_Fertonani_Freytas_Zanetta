using GestionContactos.Models;

namespace GestionContactos.Services
{
    public class AuthService
    {
        // Lista de usuarios "fake" para pruebas
        private readonly List<Usuario> _usuarios = new List<Usuario>
        {
            new Usuario { UserName = "admin", Password = "1234", Rol = "Admin" },
            new Usuario { UserName = "user", Password = "1234", Rol = "User" }
        };

        public Usuario? Login(string userName, string password)
        {
            // Busca coincidencia exacta de usuario y contraseña
            return _usuarios.FirstOrDefault(u =>
                u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase) &&
                u.Password == password);
        }
    }
}

