using PencaAPI.DatabaseConnection;
using PencaAPI.Models;

namespace PencaAPI.Services;

/// <summary>
/// Service para realizar acciones en la base de datos realacionadas a la tabla de Alumnos.
/// </summary>
/// <param name="dbConnection">Instancia de PgDatabaseConnection correspondiente a la base de datos.</param>
public class AlumnoService(PgDatabaseConnection dbConnection)
    : IService<Alumno, int>
{
    private readonly PgDatabaseConnection _dbConnection = dbConnection;
    
    /// <summary>
    /// Obtener todos los alumnos de la base de datos.
    /// </summary>
    /// <returns>Un array con todos los alumnos de la base de datos.</returns>
    public async Task<Alumno[]> GetAllAsync()
    {
        var result = await _dbConnection.QueryAsync("SELECT * FROM alumno");
        var alumnos = result.Select(x => new Alumno(
                nombre: (string)x["nombre"],
                apellido: (string)x["apellido"],
                cedula: (int)x["cedula"],
                fechaNacimiento: (DateTime)x["fecha_nacimiento"],
                anioIngreso: (int)x["anio_ingreso"],
                semestreIngreso: (int)x["semestre_ingreso"],
                puntajeTotal: (int?)x["puntaje_total"],
                campeon: (string)x["campeon"],
                subCampeon: (string)x["subcampeon"]
            )
        ).ToList();
        return alumnos.ToArray();
    }
    
    /// <summary>
    /// Obtener un alumno de la base de datos por su cédula.
    /// </summary>
    /// <param name="id">Cédula del alumno.</param>
    /// <returns>El alumno correspondiente a la cédula introducida.</returns>
    /// <exception cref="ArgumentException">No existe un alumno para la cédula introducida.</exception>
    public async Task<Alumno> GetByIdAsync(int id)
    {
        var result = (
            await _dbConnection.QueryAsync(
                "SELECT * FROM alumno WHERE cedula = @c",
                new Dictionary<string, object>()
                {
                    { "c", id }
                }
            )
        );

        var alumno = result.FirstOrDefault();

        if (alumno == null) throw new ArgumentException("No existe un alumno con esa cedula.");
        
        return new Alumno(
            nombre: (string)alumno["nombre"],
            apellido: (string)alumno["apellido"],
            cedula: (int)alumno["cedula"],
            fechaNacimiento: (DateTime)alumno["fecha_nacimiento"],
            anioIngreso: (int)alumno["anio_ingreso"],
            semestreIngreso: (int)alumno["semestre_ingreso"],
            puntajeTotal: (int?)alumno["puntaje_total"],
            campeon: (string)alumno["campeon"],
            subCampeon: (string)alumno["subcampeon"]
        );
    }

    public async Task<Alumno> CreateAsync(Alumno entity)
    {
        throw new NotImplementedException();
    }

    public async Task<Alumno> UpdateAsync(int id, Alumno entity)
    {
        throw new NotImplementedException();
    }

    public async Task<Alumno> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}