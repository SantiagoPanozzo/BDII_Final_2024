namespace PencaAPI.Models;

public class Alumno (
    string nombre, string apellido, int cedula, DateTime fechaNacimiento, int anioIngreso, int semestreIngreso,
    int? puntajeTotal, string campeon, string subCampeon)
    : IUsuario, IEntity<int>
{
    public string Nombre { get; set; } = nombre;
    public string Apellido { get; set; } = apellido;
    public int Cedula { get; set; } = cedula;
    public int Id { get => Cedula; set => Cedula = value;}
    public DateTime FechaNacimiento { get; set; } = fechaNacimiento;
    public int AnioIngreso { get; set; } = anioIngreso;
    public int SemestreIngreso { get; set; } = semestreIngreso;
    public int? PuntajeTotal { get; set; } = puntajeTotal;
    public string Campeon { get; set; } = campeon;
    public string SubCampeon { get; set; } = subCampeon;

    public override string ToString()
    {
        return $"Nombre: {Nombre}, Apellido: {Apellido}, Cedula: {Cedula}, Id: {Id}, FechaNacimiento: {FechaNacimiento}, AnioIngreso: {AnioIngreso}, SemestreIngreso: {SemestreIngreso}, PuntajeTotal: {PuntajeTotal}, Campeon: {Campeon}, SubCampeon: {SubCampeon}";
    }

}