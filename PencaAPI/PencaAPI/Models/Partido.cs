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

    public Partido (DateTime fecha, Equipo equipoE1, Equipo equipoE2, Etapa etapa)
    {
        Fecha  = fecha;
        Equipo_E1 = equipoE1;
        Equipo_E2 = equipoE2;
        Resultado_E1 = null;
        Resultado_E2 = null;
        Etapa = etapa;
    }
    public Partido (DateTime fecha, Equipo equipoE1, Equipo equipoE2, int resultadoE1, int resultadoE2, Etapa etapa)
    {
        Fecha  = fecha;
        Equipo_E1 = equipoE1;
        Equipo_E2 = equipoE2;
        Resultado_E1 = resultadoE1;
        Resultado_E2 = resultadoE2;
        Etapa = etapa;
    }
            
}