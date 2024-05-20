namespace PencaAPI.Models;

/// <summary>
/// Implementación de un Usuario Administrador del sistema.
/// </summary>
/// <param name="nombre">Nombre del administrador</param>
/// <param name="apellido">Apellido del administrador</param>
/// <param name="cedula">Cédula del administrador</param>
/// <param name="fechaNacimiento">Fecha de nacimiento del administrador</param>
/// <param name="rolUniversidad">Rol en la universidad del administrador</param>
public class Administrador(string nombre, string apellido, int cedula, DateTime fechaNacimiento, string rolUniversidad)
    : IUsuario
{
    public string Nombre { get; set; } = nombre;
    public string Apellido { get; set; } = apellido;
    public int Cedula { get; set; } = cedula;
    public DateTime FechaNacimiento { get; set; } = fechaNacimiento;
    public string RolUniversidad { get; set; } = rolUniversidad;
}