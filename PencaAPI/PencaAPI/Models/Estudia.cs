namespace PencaAPI.Models;
/// <summary>
/// Implementación de la relacion de los alumnos con las carreras que estudian en el sistema.
/// </summary>
/// <param name="cedula">Cedula del alumno</param>
/// <param name="idCarrera">Identificador de la carrera</param>
public class Estudia(Alumno alumno, Carrera carrera)
{
    public Alumno Alumno { get; set; } = alumno;
    public Carrera Carrera { get; set; } = carrera;
}