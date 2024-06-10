namespace PencaAPI.Models;
/// <summary>
/// Implementación de la relacion de los alumnos con las carreras que estudian en el sistema.
/// </summary>
/// <param name="cedula">Cedula del alumno</param>
/// <param name="idCarrera">Identificador de la carrera</param>
public class Estudia(int cedula, int idCarrera)
{
    public int Cedula { get; set; } = cedula;
    public int IdCarrera { get; set; } = idCarrera;
}