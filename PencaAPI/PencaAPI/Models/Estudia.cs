namespace PencaAPI.Models;
/// <summary>
/// Implementación de la relacion de los alumnos con las carreras que estudian en el sistema.
/// </summary>
/// <param name="cedula">Cedula del alumno</param>
/// <param name="id_carrera">Identificador de la carrera</param>
public class Estudia(int cedula, int id_carrera)
{
    public int Cedula { get; set; } = cedula;
    public int Id_Carrera { get; set; } = id_carrera;
    
}