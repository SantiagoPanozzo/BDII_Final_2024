namespace PencaAPI.Models;

public interface IUsuario
{
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public int Cedula { get; set; }
    public DateTime FechaNacimiento { get; set; }
}