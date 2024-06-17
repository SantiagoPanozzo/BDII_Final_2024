using PencaAPI.DTOs;
using PencaAPI.Models;

namespace PencaAPI.Services
{
    /// <summary>
    /// Interfaz para el servicio de usuarios.
    /// </summary>
    public interface IUsuarioService : IService<IUsuario>
    {
        /// <summary>
        /// Autentica a un usuario.
        /// </summary>
        /// <param name="cedula">Cédula del usuario.</param>
        /// <param name="password">Contraseña del usuario.</param>
        /// <returns>El usuario autenticado, o null si la autenticación falla.</returns>
        public Task<IUsuario> Authenticate(int cedula, string password);

    }
}
