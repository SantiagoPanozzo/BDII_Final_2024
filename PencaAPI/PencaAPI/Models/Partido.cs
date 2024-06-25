using System.Text.Json.Serialization;

namespace PencaAPI.Models;
/// <summary>
/// Implementación de un Partido en el sistema.
/// </summary>
/// <param name="fecha">Fecha en la que ocurrirá el partido</param>
/// <param name="equipoE1">Equipo que compite en el partido</param>
/// <param name="equipoE2">Equipo que compite en el partido</param>
/// <param name="etapa">Etapa en la que se da el partido</param>
public class Partido
{
    public DateTime Fecha { get; set; } 
    public Equipo Equipo_E1 { get; set; } 
    public Equipo Equipo_E2 { get; set; } 
    public int? Resultado_E1 { get; set; } 
    public int? Resultado_E2 { get; set; } 
    public Etapa Etapa{ get; set; } 

    public Partido (DateTime fecha, Equipo equipo_E1, Equipo equipo_E2, int? resultado_E1, int? resultado_E2, Etapa etapa)
    {
        Fecha  = fecha;
        Equipo_E1 = equipo_E1;
        Equipo_E2 = equipo_E2;
        Resultado_E1 = resultado_E1;
        Resultado_E2 = resultado_E2;
        Etapa = etapa;
    }
    public bool EsPosibleCrearPrediccion()
    {
        // Obtener la hora actual
        DateTime horaActual = DateTime.Now;

        // Calcular la hora límite para la predicción (una hora antes del inicio del partido)
        DateTime horaLimite = Fecha.AddHours(-1);

        // Comparar la hora actual con la hora límite
        if (horaActual < horaLimite)
        {
            // Aún es posible crear la predicción
            return true;
        }
        else
        {
            // La hora actual es igual o mayor a la hora límite, no se puede crear la predicción
            return false;
        }
    }
            
}