// Services/ContrasenaHasher.cs
using BCrypt.Net;

namespace PencaAPI.Services
{
    /// <summary>
    /// Provee de métodos para encriptar y desencriptar contraseñas.
    /// </summary>
    public static class ContrasenaHasher
    {
        /// <summary>
        /// Encripta una contraseña especifica.
        /// </summary>
        /// <param name="contrasena">la contraseña para encriptar.</param>
        /// <returns>The hashed Contrasena.</returns>
        public static string HashContrasena(string contrasena)
        {
            return BCrypt.Net.BCrypt.HashPassword(contrasena);
        }

        /// <summary>
        /// Verifica que una contrasena es la misma que la contraseña encriptada.
        /// </summary>
        /// <param name="contrasena">La contraseña en texto plano a verificar.</param>
        /// <param name="hashedContrasena">la contraseña encriptada para comparar.</param>
        /// <returns><c>true</c> si la contraseña es válida; de otra forma, <c>false</c>.</returns>
        public static bool VerifyContrasena(string contrasena, string hashedContrasena)
        {
            return BCrypt.Net.BCrypt.Verify(contrasena, hashedContrasena);
        }
    }
}
