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
        var sqlQuery = @"SELECT a.*,
                             c.Pais as campeon_nombre,
                             s.Pais as subcampeon_nombre,
                             carr.id as carrera_id,
                             carr.nombre as carrera_nombre
                      FROM Alumno a
                      join Equipo c
                      on a.campeon = c.Abreviatura
                      join Equipo s
                      on a.subcampeon = s.Abreviatura 
                      join Estudia e
                      on e.cedula = a.cedula
                      join Carrera carr
                      on e.id_carrera = carr.id
                      where e.principal = 1
                      ";
        var result = await _dbConnection.QueryAsync(sqlQuery);
        var alumnos = result.Select(x => new Alumno(
                nombre: (string)x["nombre"],
                apellido: (string)x["apellido"],
                cedula: (int)x["cedula"],
                contrasena: (string)x["contrasena"],
                fechaNacimiento: (DateTime)x["fecha_nacimiento"],
                anioIngreso: (int)x["anio_ingreso"],
                semestreIngreso: (int)x["semestre_ingreso"],
                puntajeTotal: (int)x["puntaje_total"],
                campeon: new Equipo(
                    abreviatura: (string)x["campeon"],
                    pais: (string)x["campeon_nombre"]
                ),
                subCampeon: new Equipo(
                    abreviatura: (string)x["subcampeon"],
                    pais: (string)x["subcampeon_nombre"]
                ),
                carreraPrincipal: new Carrera(
                    id: (int)x["carrera_id"],
                    nombre: (string)x["carrera_nombre"]
                )
            )
        ).ToList();
        return alumnos.ToArray();
    }
    /// <summary>
    /// Obtener todos los alumnos de la base de datos ordenados por puntaje.
    /// </summary>
    /// <returns>Un array con todos los alumnos de la base de datos.</returns>
    public async Task<Alumno[]> GetAllOrderAsync()
    {
        var sqlQuery = @"SELECT a.*,
                             c.Pais as campeon_nombre,
                             s.Pais as subcampeon_nombre,
                             carr.id as carrera_id,
                             carr.nombre as carrera_nombre
                      FROM Alumno a
                      join Equipo c
                      on a.campeon = c.Abreviatura
                      join Equipo s
                      on a.subcampeon = s.Abreviatura 
                      join Estudia e
                      on e.cedula = a.cedula
                      join Carrera carr
                      on e.id_carrera = carr.id
                      where e.principal = 1
                      order by a.puntaje_total desc
                      ";
        var result = await _dbConnection.QueryAsync(sqlQuery);
        var alumnos = result.Select(x => new Alumno(
                nombre: (string)x["nombre"],
                apellido: (string)x["apellido"],
                cedula: (int)x["cedula"],
                contrasena: (string)x["contrasena"],
                fechaNacimiento: (DateTime)x["fecha_nacimiento"],
                anioIngreso: (int)x["anio_ingreso"],
                semestreIngreso: (int)x["semestre_ingreso"],
                puntajeTotal: (int)x["puntaje_total"],
                campeon: new Equipo(
                    abreviatura: (string)x["campeon"],
                    pais: (string)x["campeon_nombre"]
                ),
                subCampeon: new Equipo(
                    abreviatura: (string)x["subcampeon"],
                    pais: (string)x["subcampeon_nombre"]
                ),
                carreraPrincipal: new Carrera(
                    id: (int)x["carrera_id"],
                    nombre: (string)x["carrera_nombre"]
                )
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
        var sqlQuery = @"SELECT a.*,
                             c.Pais as campeon_nombre,
                             s.Pais as subcampeon_nombre,
                             carr.id as carrera_id,
                             carr.nombre as carrera_nombre
                      FROM Alumno a
                      join Equipo c
                      on a.campeon = c.Abreviatura
                      join Equipo s
                      on a.subcampeon = s.Abreviatura 
                      join Estudia e
                      on e.cedula = a.cedula
                      join Carrera carr
                      on e.id_carrera = carr.id
                      where
                      a.cedula = @c
                      and
                      e.principal = 1
                      ";
        var result = (
            await _dbConnection.QueryAsync(
                sqlQuery,
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
              campeon: new Equipo(
                    abreviatura: (string)alumno["campeon"],
                    pais: (string)alumno["campeon_nombre"]
                ),
                subCampeon: new Equipo(
                    abreviatura: (string)alumno["subcampeon"],
                    pais: (string)alumno["subcampeon_nombre"]
                ),
                carreraPrincipal: new Carrera(
                    id: (int)alumno["carrera_id"],
                    nombre: (string)alumno["carrera_nombre"]
                )
        );
    }

    public async Task<Alumno> CreateAsync(Alumno entity)
    {
        // Hash de la contraseña
        var hashedContrasena = ContrasenaHasher.HashContrasena(entity.Contrasena);
        var sqlQuery = @"
                        WITH insertedAlumno as (
                        INSERT INTO alumno 
                        (nombre, apellido, cedula, contrasena ,fecha_nacimiento, anio_ingreso, semestre_ingreso, puntaje_total, campeon, subcampeon)
                        VALUES (@n, @a, @c,@p, @f, @ai, @si, @pt ,@cam, @scam) 
                        RETURNING *),
                        insertedEstudia as (
                        INSERT INTO estudia
                        (cedula, id_carrera, principal)
                        VALUES (@c, @ic, 1)
                        RETURNING *)
                        Select 
                            ia.*,
                            camp.Pais as campeon_nombre,
                            s.Pais as subcampeon_nombre,
                            c.id as carrera_id,
                            c.nombre as carrera_nombre
                         From insertedAlumno ia
                         join insertedEstudia ie
                         on ia.cedula = ie.cedula
                         join Carrera c
                         on ie.id_carrera = c.id
                         join Equipo camp
                         on ia.campeon = camp.Abreviatura
                         join Equipo s
                         on ia.subcampeon = s.Abreviatura;
                        ";
        var result = (
            await _dbConnection.QueryAsync(
                sqlQuery,

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
                    { "cam", entity.Campeon.Abreviatura },
                    { "scam", entity.SubCampeon.Abreviatura },
                    { "ic", entity.CarreraPrincipal.Id}
                    
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
            campeon: new Equipo(
                abreviatura: (string)alumno["campeon"],
                pais: (string)alumno["campeon_nombre"]
            ),
            subCampeon: new Equipo(
                abreviatura: (string)alumno["subcampeon"],
                pais: (string)alumno["subcampeon_nombre"]
            ),
            carreraPrincipal: new Carrera(
                id: (int)alumno["carrera_id"],
                nombre: (string)alumno["carrera_nombre"]
            )
        );
    }

    public async Task<Alumno> UpdateAsync(object id, Alumno entity)
    {
        await this.GetByIdAsync(id);
        var sqlQuery = @"
                        WITH updated as (
                        UPDATE alumno
                        SET  nombre = @n, 
                        apellido = @a, 
                        fecha_nacimiento = @f, 
                        anio_ingreso = @ai, 
                        semestre_ingreso = @si
                        WHERE cedula = @c 
                        RETURNING *)
                        SELECT updated.*,
                             c.Pais as campeon_nombre,
                             s.Pais as subcampeon_nombre,
                             carr.id as carrera_id,
                             carr.nombre as carrera_nombre
                        FROM updated
                        join Equipo c
                        on updated.campeon = c.Abreviatura
                        join Equipo s
                        on updated.subcampeon = s.Abreviatura 
                        join Estudia e
                        on e.cedula = updated.cedula
                        join Carrera carr
                        on e.id_carrera = carr.id
                        where
                        updated.cedula = @c
                        and
                        e.principal = 1
                        ";
        var result = (
            await _dbConnection.QueryAsync(
                sqlQuery,

                new Dictionary<string, object>()
                {
                    { "n", entity.Nombre },
                    { "a", entity.Apellido },
                    { "c", id },
                    { "f", entity.FechaNacimiento },
                    { "ai", entity.AnioIngreso },
                    { "si", entity.SemestreIngreso }
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
            campeon: new Equipo(
                abreviatura: (string)alumno["campeon"],
                pais: (string)alumno["campeon_nombre"]
            ),
            subCampeon: new Equipo(
                abreviatura: (string)alumno["subcampeon"],
                pais: (string)alumno["subcampeon_nombre"]
            ),
            carreraPrincipal: new Carrera(
                id: (int)alumno["carrera_id"],
                nombre: (string)alumno["carrera_nombre"]
            )
        );
    }

    public async Task SetPuntajeCampeonSubcampeonAsync(Partido partido)
    {
        if (partido.Etapa.Id != 4)
        {
            throw new ArgumentException("El partido no es de la etapa final.");
        }
        Equipo campeon = partido.Resultado_E1 > partido.Resultado_E2 ? partido.Equipo_E1 : partido.Equipo_E2;
        Equipo subcampeon = partido.Resultado_E1 < partido.Resultado_E2 ? partido.Equipo_E1 : partido.Equipo_E2;

        var sqlQuery = @"
                        UPDATE Alumno  
                        SET 
                        Puntaje_Total = Puntaje_Total +
                                    Case
                                        When Campeon = @c and Subcampeon <>@s then 10
                                        When Campeon <> @c and Subcampeon = @s then 5
                                        When Campeon = @c and Subcampeon = @s then 15
                                        else 0
                                    end
                        Where Campeon = @c
                              or SubCampeon =@s
                              or (Campeon = @c and SubCampeon =@s)
                        RETURNING *
                        ";
        await _dbConnection.QueryAsync(sqlQuery,
                new Dictionary<string, object>()
                {
                    { "c", campeon.Abreviatura},
                    { "s", subcampeon.Abreviatura}
                }
        );


    }

    public async Task DeleteAsync(object id)
    {
        await this.GetByIdAsync(id);
        var resultEstudia = (
            await _dbConnection.QueryAsync(
                "DELETE FROM estudia WHERE cedula = @c RETURNING *",
                new Dictionary<string, object>()
                {
                    { "c", id }
                }
            )
        );
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