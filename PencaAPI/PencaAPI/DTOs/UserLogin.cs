namespace PencaAPI.DTOs
{
    /// <summary>
    /// Modelo para los datos de inicio de sesión del usuario.
    /// </summary>
    public class UserLogin
    {
        /// <summary>
        /// Obtiene o establece la cédula del usuario.
        /// </summary>
        public int Cedula { get; set; }

        /// <summary>
        /// Obtiene o establece la contraseña del usuario.
        /// </summary>
        public string Contrasena { get; set; }
    }
}
