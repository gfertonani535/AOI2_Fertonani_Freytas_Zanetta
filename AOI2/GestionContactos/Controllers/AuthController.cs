using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GestionContactos.Models;
using GestionContactos.Services;
using System.Linq;

namespace GestionContactos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly DbA358b2Pam3Context _context; // ✅ Agregado

        public AuthController(AuthService authService, DbA358b2Pam3Context context)
        {
            _authService = authService;
            _context = context; // ✅ Recibido por DI
        }

        // ✅ Registrar usuario en SQL Server
        [HttpPost("register")]
        public IActionResult Register([FromBody] Usuario usuario)
        {
            if (_context.Usuarios.Any(u => u.UserName == usuario.UserName))
                return BadRequest("El usuario ya existe");

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            return Ok(new { message = "Usuario registrado correctamente" });
        }

        // ✅ Login usando BD real
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest login)
        {
            var usuario = _authService.Login(login.UserName, login.Password);
            if (usuario == null)
                return Unauthorized("Credenciales inválidas");

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, usuario.UserName),
                new Claim(ClaimTypes.Role, usuario.Rol)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("clave_super_secreta_1234567890_9876543210"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "tuApp",
                audience: "tuApp",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
    }
}
