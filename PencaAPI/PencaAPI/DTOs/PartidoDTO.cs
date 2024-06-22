namespace PencaAPI.DTOs
{
    /// <summary>
    /// Modelo para los datos de inicio de sesión del usuario.
    /// </summary>
    public class PartidoDTO
    {
        /// <summary>
        /// Obtiene o establece la cédula del usuario.
        /// </summary>
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Obtiene o establece la contraseña del usuario.
        /// </summary>
        public int Equipo_E1 { get; set; }
        /// <summary>
        /// Obtiene o establece la contraseña del usuario.
        /// </summary>
        public int Equipo_E2 { get; set; }
    }
}
