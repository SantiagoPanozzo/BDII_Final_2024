using PencaAPI.Models;

namespace PencaAPI.DTOs
{
    /// <summary>
    /// Modelo para los datos de inicio de sesión del usuario.
    /// </summary>
    public class PartidoDTO(DateTime fecha, string equipo_E1, string equipo_E2)
    {

        /// <summary>
        /// Obtiene o establece la cédula del usuario.
        /// </summary>
        public DateTime Fecha { get; set; } = fecha;

        /// <summary>
        /// Obtiene o establece la contraseña del usuario.
        /// </summary>
        public string Equipo_E1 { get; set; } = equipo_E1;
        /// <summary>
        /// Obtiene o establece la contraseña del usuario.
        /// </summary>
        public string Equipo_E2 { get; set; } = equipo_E2;
    }
}
