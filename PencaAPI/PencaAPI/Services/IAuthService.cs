namespace PencaAPI.Services
{
    /// <summary>
    /// Interfaz para el servicio de autenticación.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Autentica a un usuario y genera un token JWT.
        /// </summary>
        /// <param name="cedula">Cédula del usuario.</param>
        /// <param name="password">Contraseña del usuario.</param>
        /// <returns>Token JWT si la autenticación es exitosa, null si falla.</returns>
        Task<string> AuthenticateAsync(int cedula, string password);
    }
}
