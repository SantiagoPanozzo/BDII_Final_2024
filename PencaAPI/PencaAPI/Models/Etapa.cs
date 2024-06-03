namespace PencaAPI.Models;
/// <summary>
/// Implementación de una Etapa en el sistema.
/// </summary>
/// <param name="id">Identificador de la etapa</param>
/// <param name="nombre">Nombre de la etapa</param>
public class Etapa(int id, string nombre)
{
    public int Id { get; set; } = id;
    public string Nombre { get; set; } = nombre;
    
}