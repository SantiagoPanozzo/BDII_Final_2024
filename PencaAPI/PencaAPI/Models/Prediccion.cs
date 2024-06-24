using System.Text.Json.Serialization;

namespace PencaAPI.Models;
/// <summary>
/// Implementación de una Predicción en el sistema.
/// </summary>
/// <param name="alumno">Alumno que hace la predicción</param>
/// <param name="partido">Partido a predecir</param>
/// <param name="prediccion_e1">Valor de predicción para el primer equipo</param>
/// <param name="prediccion_e2">Valor de predicción para el segundo equipo</param>
/// <param name="puntaje">Puntaje calculado para dicha predicción</param>
public class Prediccion
{
    public Alumno Alumno { get; set; } 
    public Partido Partido { get; set; } 
    public int Prediccion_E1 { get; set; } 
    public int Prediccion_E2 { get; set; } 
    public int? Puntaje { get; set; } 


    public Prediccion (Alumno alumno, Partido partido, int prediccion_e1, int prediccion_e2, int? puntaje)
    {
        Alumno = alumno;
        Partido = partido;
        Prediccion_E1 = prediccion_e1;
        Prediccion_E2 = prediccion_e2;
        Puntaje = puntaje;
    }
}