namespace PencaAPI.Models;

/// <summary>
/// Clase que representa un Usuario del sistema.
/// </summary>
public interface IUsuario
{
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public int Cedula { get; set; }
    public DateTime FechaNacimiento { get; set; }

    public string NombreCompleto => $"{Nombre} {Apellido}";
}