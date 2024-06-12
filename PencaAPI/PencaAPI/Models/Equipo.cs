namespace PencaAPI.Models;
/// <summary>
/// Implementación de una Equipo en el sistema.
/// </summary>
/// <param name="abreviatura">Identificador de un equipo</param>
/// <param name="pais">Nombre completo de un país</param>
public class Equipo(string abreviatura, string pais)
{
    public string Abreviatura { get; set; } = abreviatura;
    public string Pais { get; set; } = pais;
    
}