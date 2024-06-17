using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using PencaAPI.DTOs;
using PencaAPI.Models;
using PencaAPI.Services;


namespace PencaAPI.Controllers
{
    /// <summary>
    /// Controlador para la autenticación de usuarios.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly AlumnoService _alumnoService;

        public AuthController(IAuthService authService, AlumnoService alumnoService)
        {
            _authService = authService;
            _alumnoService = alumnoService;
        }

        /// <summary>
        /// Autentica al usuario y genera un token JWT.
        /// </summary>
        /// <param name="login">El modelo de inicio de sesión.</param>
        /// <returns>El token JWT si la autenticación es exitosa, de lo contrario Unauthorized.</returns>
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLogin login)
        {
            Console.WriteLine($"{login.Cedula}");
            Console.WriteLine($"{login.Contrasena}");
            var token = _authService.AuthenticateAsync(login.Cedula, login.Contrasena);

            if (token == null)
                return Unauthorized();

            return Ok(new { Token = token });
        }
        /// <summary>
        /// Registra un nuevo usuario.
        /// </summary>
        /// <param name="registerDto">DTO con los datos del usuario.</param>
        /// <returns>Resultado del registro.</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Alumno alumno)
        {
            var user = await _alumnoService.CreateAsync(alumno);

            if (user == null)
                return BadRequest(new { message = "Error al registrar el usuario" });

            return Ok(new { message = "Usuario registrado exitosamente" });
        }
    }
}

