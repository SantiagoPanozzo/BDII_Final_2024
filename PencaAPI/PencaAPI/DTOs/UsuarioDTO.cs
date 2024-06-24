namespace PencaAPI.DTOs;

/// <summary>
/// Clase que representa un Usuario del sistema.
/// </summary>
public class UsuarioDTO(string nombre, string apellido, int cedula, DateTime fechaNacimiento, string rol)
{
    public string Nombre { get; set; } = nombre ;
    public string Apellido { get; set; } = apellido;
    public int Cedula { get; set; } = cedula;
    public DateTime FechaNacimiento { get; set; } = fechaNacimiento;
    public string Rol {get; set;} = rol;
}