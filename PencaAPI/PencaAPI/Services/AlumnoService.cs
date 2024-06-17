using PencaAPI.DatabaseConnection;
using PencaAPI.Models;

namespace PencaAPI.Services;

/// <summary>
/// Service para realizar acciones en la base de datos realacionadas a la tabla de Alumnos.
/// </summary>
/// <param name="dbConnection">Instancia de PgDatabaseConnection correspondiente a la base de datos.</param>
public class AlumnoService(PgDatabaseConnection dbConnection)
    : IService<Alumno>
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
                contrasena: (string)x["contrasena"],
                fechaNacimiento: (DateTime)x["fecha_nacimiento"],
                anioIngreso: (int)x["anio_ingreso"],
                semestreIngreso: (int)x["semestre_ingreso"],
                puntajeTotal: (int)x["puntaje_total"],
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
    public async Task<Alumno> GetByIdAsync(object id)
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
            contrasena: (string)alumno["contrasena"],
            fechaNacimiento: (DateTime)alumno["fecha_nacimiento"],
            anioIngreso: (int)alumno["anio_ingreso"],
            semestreIngreso: (int)alumno["semestre_ingreso"],
            puntajeTotal: (int)alumno["puntaje_total"],
            campeon: (string)alumno["campeon"],
            subCampeon: (string)alumno["subcampeon"]
        );
    }

    public async Task<Alumno> CreateAsync(Alumno entity)
    {
        // Hash de la contraseña
        var hashedContrasena = ContrasenaHasher.HashContrasena(entity.Contrasena);
        var result = (
            await _dbConnection.QueryAsync(
                "INSERT INTO alumno (" +
                "nombre, apellido, cedula, contrasena ,fecha_nacimiento, anio_ingreso, semestre_ingreso, puntaje_total, campeon, subcampeon)" +
                "VALUES (@n, @a, @c,@p, @f, @ai, @si, @pt ,@cam, @scam) RETURNING *",

                new Dictionary<string, object>()
                {
                    { "n", entity.Nombre },
                    { "a", entity.Apellido },
                    { "c", entity.Cedula },
                    { "p", hashedContrasena },
                    { "f", entity.FechaNacimiento },
                    { "pt", entity.PuntajeTotal },
                    { "ai", entity.AnioIngreso },
                    { "si", entity.SemestreIngreso },
                    { "cam", entity.Campeon },
                    { "scam", entity.SubCampeon }
                }
            )
        );
        
        var alumno = result.FirstOrDefault();
        
        if (alumno == null) throw new ArgumentException("No se pudo crear el alumno.");
        
        return new Alumno(
            nombre: (string)alumno["nombre"],
            apellido: (string)alumno["apellido"],
            cedula: (int)alumno["cedula"],
            contrasena: (string)alumno["contrasena"],
            fechaNacimiento: (DateTime)alumno["fecha_nacimiento"],
            anioIngreso: (int)alumno["anio_ingreso"],
            semestreIngreso: (int)alumno["semestre_ingreso"],
            puntajeTotal: (int)alumno["puntaje_total"],
            campeon: (string)alumno["campeon"],
            subCampeon: (string)alumno["subcampeon"]
        );
    }

    public async Task<Alumno> UpdateAsync(object id, Alumno entity)
    {
        var result = (
            await _dbConnection.QueryAsync(
                "UPDATE alumno SET " +
                "nombre = @n, apellido = @a, fecha_nacimiento = @f, anio_ingreso = @ai, semestre_ingreso = @si, puntaje_total = @pt, campeon = @cam, subcampeon = @scam " +
                "WHERE cedula = @c RETURNING *",

                new Dictionary<string, object>()
                {
                    { "n", entity.Nombre },
                    { "a", entity.Apellido },
                    { "c", entity.Cedula },
                    { "f", entity.FechaNacimiento },
                    { "pt", entity.PuntajeTotal },
                    { "ai", entity.AnioIngreso },
                    { "si", entity.SemestreIngreso },
                    { "cam", entity.Campeon },
                    { "scam", entity.SubCampeon }
                }
            )
        );
        
        var alumno = result.FirstOrDefault();
        
        if (alumno == null) throw new ArgumentException("No se pudo actualizar el alumno.");
        
        return new Alumno(
            nombre: (string)alumno["nombre"],
            apellido: (string)alumno["apellido"],
            cedula: (int)alumno["cedula"],
            contrasena: (string)alumno["contrasena"],
            fechaNacimiento: (DateTime)alumno["fecha_nacimiento"],
            anioIngreso: (int)alumno["anio_ingreso"],
            semestreIngreso: (int)alumno["semestre_ingreso"],
            puntajeTotal: (int)alumno["puntaje_total"],
            campeon: (string)alumno["campeon"],
            subCampeon: (string)alumno["subcampeon"]
        );
    }

    public async Task DeleteAsync(object id)
    {
        var result = (
            await _dbConnection.QueryAsync(
                "DELETE FROM alumno WHERE cedula = @c RETURNING *",
                new Dictionary<string, object>()
                {
                    { "c", id }
                }
            )
        );
    }
}