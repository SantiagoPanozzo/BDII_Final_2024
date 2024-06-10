namespace PencaAPI.Models;

/// <summary>
/// Implementación de una Carrera en el sistema.
/// </summary>
/// <param name="id">Identificador de la carrera</param>
/// <param name="nombre">Nombre de la carrera</param>
public class Carrera(int id, string nombre)
{
    public int Id { get; set; } = id;
    public string Nombre { get; set; } = nombre;
    
}