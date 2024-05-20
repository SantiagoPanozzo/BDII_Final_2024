namespace PencaAPI.Models;

public class Administrador(string nombre, string apellido, int cedula, DateTime fechaNacimiento, string rolUniversidad)
    : IUsuario
{
    public string Nombre { get; set; } = nombre;
    public string Apellido { get; set; } = apellido;
    public int Cedula { get; set; } = cedula;
    public DateTime FechaNacimiento { get; set; } = fechaNacimiento;
    public string RolUniversidad { get; set; } = rolUniversidad;
}