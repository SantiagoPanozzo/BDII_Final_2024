using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PencaAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PencaAPI.Services
{
    /// <summary>
    /// Servicio para la autenticación de usuarios y generación de tokens JWT.
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly IUsuarioService _usuarioService;
        private readonly string _secret;

        /// <summary>
        /// Constructor del servicio de autenticación.
        /// </summary>
        /// <param name="usuarioService">Servicio de usuarios.</param>
        /// <param name="configuration">Configuración de la aplicación.</param>
        public AuthService(IUsuarioService usuarioService, IConfiguration configuration)
        {
            _usuarioService = usuarioService;
            _secret = configuration["Jwt:Secret"];
        }

        /// <summary>
        /// Autentica a un usuario y genera un token JWT.
        /// </summary>
        /// <param name="cedula">Cédula del usuario.</param>
        /// <param name="password">Contraseña del usuario.</param>
        /// <returns>Token JWT si la autenticación es exitosa, null si falla.</returns>
        public async Task<string> AuthenticateAsync(int cedula, string contrasena)
        {
            var user = await _usuarioService.Authenticate(cedula, contrasena);

            if (user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Cedula.ToString()),
                    new Claim(ClaimTypes.Role, user.Rol)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
