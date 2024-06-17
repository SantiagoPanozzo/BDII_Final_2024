using PencaAPI.DTOs;

namespace PencaAPI.Models;

/// <summary>
/// Implementación de un Usuario Alumno del sistema.
/// </summary>
/// <param name="nombre">Nombre del alumno</param>
/// <param name="apellido">Apellido del alumno</param>
/// <param name="cedula">Cédula del alumno</param>
/// <param name="fechaNacimiento">Fecha de nacimiento del alumno</param>
/// <param name="anioIngreso">Año de ingreso del alumno</param>
/// <param name="semestreIngreso">Semestre de ingreso del alumno</param>
/// <param name="puntajeTotal">Puntaje total obtenido por el alumno</param>
/// <param name="campeon">Campeón elegido por el alumno</param>
/// <param name="subCampeon">Subcampeón elegido por el alumno</param>
public class Alumno (
    string nombre, string apellido, int cedula, string contrasena, DateTime fechaNacimiento, int anioIngreso, int semestreIngreso,
    int puntajeTotal, string campeon, string subCampeon)
    : IUsuario
{
    public string Nombre { get; set; } = nombre;
    public string Apellido { get; set; } = apellido;
    public int Cedula { get; set; } = cedula;
    public string Contrasena { get; set; } = contrasena;
    public DateTime FechaNacimiento { get; set; } = fechaNacimiento;
    public int AnioIngreso { get; set; } = anioIngreso;
    public int SemestreIngreso { get; set; } = semestreIngreso;
    public int PuntajeTotal { get; set; } = puntajeTotal;
    public string Campeon { get; set; } = campeon;
    public string SubCampeon { get; set; } = subCampeon;

    public override string ToString()
    {
        return $"Nombre: {Nombre}, Apellido: {Apellido}, Cédula: {Cedula}, Fecha de Nacimiento: {FechaNacimiento}, Año de ingreso: {AnioIngreso}, Semestre de ingreso: {SemestreIngreso}, Puntaje total: {PuntajeTotal}, Campeón: {Campeon}, Subcampeón: {SubCampeon}";
    }

    public static explicit operator Alumno(UserRegisterDto v)
    {
        throw new NotImplementedException();
    }
}