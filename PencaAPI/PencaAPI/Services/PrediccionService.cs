using PencaAPI.DatabaseConnection;
using PencaAPI.Models;
using PencaAPI.DTOs;
using System.Runtime.InteropServices;
using System.Linq.Expressions;
using Npgsql;

namespace PencaAPI.Services;

/// <summary>
/// Service para realizar acciones en la base de datos realacionadas a la tabla de Carera.
/// </summary>
/// <param name="dbConnection">Instancia de PgDatabaseConnection correspondiente a la base de datos.</param>
public class PrediccionService(PgDatabaseConnection dbConnection)
    : IService<Prediccion>
{
    private readonly PgDatabaseConnection _dbConnection = dbConnection;

    
    /// <summary>
    /// Obtener todas las arreras de la base de datos.
    /// </summary>
    /// <returns>Un array con todas las carreras de la base de datos.</returns>
    public async Task<Prediccion[]> GetAllAsync()
    {
        try{
                var sqlQuery = @"Select 
                                    p.Prediccion_E1 as pe1,
                                    p.Prediccion_E2 as pe2,
                                    coalesce(p.Puntaje, 0) as PuntajePred,

                                    a.*,
                                    camp.Pais as campeon_nombre,
                                    subcamp.Pais as subcampeon_nombre,
                                    c.id as carrera_id,
                                    c.nombre as carrera_nombre,

                                    par.Fecha as fecha,
                                    e1.Abreviatura as E1_Abreviatura,
                                    e1.Pais as E1_Pais,
                                    e2.Abreviatura as E2_Abreviatura,
                                    e2.Pais as E2_Pais,
                                    coalesce(par.Resultado_E1, -1) as Resultado_E1,
                                    coalesce(par.Resultado_E2, -1) as Resultado_E2,
                                    et.Id as Etapa_Id,
                                    et.Nombre as Etapa_Nombre

                                From Prediccion p

                                join Alumno a
                                on p.cedula = a.cedula
                                join Estudia e
                                on a.cedula = e.cedula
                                join Carrera c
                                on e.id_carrera = c.id
                                join Equipo camp
                                on a.campeon = camp.abreviatura
                                join Equipo subcamp
                                on a.subcampeon = subcamp.abreviatura
                            
                                join Partido par
                                on (p.Equipo_E1 = par.Equipo_E1
                                and p.Equipo_E2 = par.Equipo_E2
                                AND p.fecha_partido = par.fecha)
                                or
                                (p.Equipo_E1 = par.Equipo_E2
                                and p.Equipo_E2 = par.Equipo_E1
                                AND p.fecha_partido = par.fecha)

                                join Equipo e1
                                on par.Equipo_E1 = e1.Abreviatura
                                join Equipo e2
                                on par.Equipo_E2 = e2.Abreviatura
                                
                                join Etapa et
                                on par.Etapa = et.id";
                var result = await _dbConnection.QueryAsync(sqlQuery);
                var predicciones = result.Select(x => new Prediccion(
                        alumno: new Alumno(
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
                        ),
                        partido: new Partido(
                                fecha: (DateTime)x["fecha"],
                                equipo_E1: new Equipo(
                                    abreviatura: (string)x["e1_abreviatura"],
                                    pais: (string)x["e1_pais"]
                                ),
                                equipo_E2: new Equipo(
                                    abreviatura: (string)x["e2_abreviatura"],
                                    pais: (string)x["e2_pais"]
                                ),
                                resultado_E1:(int)x["resultado_e1"],
                                resultado_E2:(int)x["resultado_e2"],
                                etapa: new Etapa(
                                    id: (int)x["etapa_id"],
                                    nombre: (string)x["etapa_nombre"]
                                )
                        ),
                        prediccion_e1: (int)x["pe1"],
                        prediccion_e2: (int)x["pe2"],
                        puntaje: (int)x["puntajepred"]
                    )
                ).ToList();
                return predicciones.ToArray();
        }
        catch (PostgresException e)
        {
            throw new ArgumentException("Ocurrió un error al acceder a la base de datos.", e);
        }
    }

      /// <summary>
    /// Obtener todas las arreras de la base de datos.
    /// </summary>
    /// <returns>Un array con todas las carreras de la base de datos.</returns>
    public async Task<Prediccion[]> GetAllByPartidoAsync(PartidoDTO partidoDTO)
    {
        try{
            var sqlQuery = @"Select 
                                p.Prediccion_E1 as pe1,
                                p.Prediccion_E2 as pe2,
                                coalesce(p.Puntaje, 0) as PuntajePred,

                                a.*,
                                camp.Pais as campeon_nombre,
                                subcamp.Pais as subcampeon_nombre,
                                c.id as carrera_id,
                                c.nombre as carrera_nombre,

                                par.Fecha as fecha,
                                e1.Abreviatura as E1_Abreviatura,
                                e1.Pais as E1_Pais,
                                e2.Abreviatura as E2_Abreviatura,
                                e2.Pais as E2_Pais,
                                coalesce(par.Resultado_E1, -1) as Resultado_E1,
                                coalesce(par.Resultado_E2, -1) as Resultado_E2,
                                et.Id as Etapa_Id,
                                et.Nombre as Etapa_Nombre

                            From Prediccion p

                            join Alumno a
                            on p.cedula = a.cedula
                            join Estudia e
                            on a.cedula = e.cedula
                            join Carrera c
                            on e.id_carrera = c.id
                            join Equipo camp
                            on a.campeon = camp.abreviatura
                            join Equipo subcamp
                            on a.subcampeon = subcamp.abreviatura
                        
                            join Partido par
                            on (p.Equipo_E1 = par.Equipo_E1
                            and p.Equipo_E2 = par.Equipo_E2
                            AND p.fecha_partido = par.fecha)
                            or
                            (p.Equipo_E1 = par.Equipo_E2
                            and p.Equipo_E2 = par.Equipo_E1
                            AND p.fecha_partido = par.fecha)

                            join Equipo e1
                            on par.Equipo_E1 = e1.Abreviatura
                            join Equipo e2
                            on par.Equipo_E2 = e2.Abreviatura
                            
                            join Etapa et
                            on par.Etapa = et.id
                            Where par.Equipo_E1 = @equipo1
                            and par.Equipo_E2 = @equipo2
                            and par.Fecha = @fecha";
            var result = (
                await _dbConnection.QueryAsync(
                    sqlQuery,
                    new Dictionary<string, object>()
                    {
                        { "fecha", partidoDTO.Fecha },
                        { "equipo1", partidoDTO.Equipo_E1 },
                        { "equipo2", partidoDTO.Equipo_E2 }
                    }
                )
            );
            var predicciones = result.Select(x => new Prediccion(
                    alumno: new Alumno(
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
                    ),
                    partido: new Partido(
                            fecha: (DateTime)x["fecha"],
                            equipo_E1: new Equipo(
                                abreviatura: (string)x["e1_abreviatura"],
                                pais: (string)x["e1_pais"]
                            ),
                            equipo_E2: new Equipo(
                                abreviatura: (string)x["e2_abreviatura"],
                                pais: (string)x["e2_pais"]
                            ),
                            resultado_E1:(int)x["resultado_e1"],
                            resultado_E2:(int)x["resultado_e2"],
                            etapa: new Etapa(
                                id: (int)x["etapa_id"],
                                nombre: (string)x["etapa_nombre"]
                            )
                    ),
                    prediccion_e1: (int)x["pe1"],
                    prediccion_e2: (int)x["pe2"],
                    puntaje: (int)x["puntajepred"]
                )
            ).ToList();
            return predicciones.ToArray();
        }
        catch (PostgresException e)
        {
            throw new ArgumentException("Ocurrió un error al acceder a la base de datos.", e);
        }
    }
    
    /// <summary>
    /// Obtener una carrera de la base de datos por su id
    /// </summary>
    /// <param name="id">id de la carrera.</param>
    /// <returns>La carrera correspondiente a el id introducido.</returns>
    /// <exception cref="ArgumentException">No existe una carrera con el id introducido.</exception>
    public async Task<Prediccion> GetByIdAsync(object id)
    {   
        try{
            PrediccionDTO prediccionDto = (PrediccionDTO)id;
            var sqlQuery = @"Select 
                                p.Prediccion_E1 as pe1,
                                p.Prediccion_E2 as pe2,
                                coalesce(p.Puntaje, 0) as PuntajePred,

                                a.*,
                                camp.Pais as campeon_nombre,
                                subcamp.Pais as subcampeon_nombre,
                                c.id as carrera_id,
                                c.nombre as carrera_nombre,

                                par.Fecha as fecha,
                                e1.Abreviatura as E1_Abreviatura,
                                e1.Pais as E1_Pais,
                                e2.Abreviatura as E2_Abreviatura,
                                e2.Pais as E2_Pais,
                                coalesce(par.Resultado_E1, -1) as Resultado_E1,
                                coalesce(par.Resultado_E2, -1) as Resultado_E2,
                                et.Id as Etapa_Id,
                                et.Nombre as Etapa_Nombre

                            From Prediccion p

                            join Alumno a
                            on p.cedula = a.cedula
                            join Estudia e
                            on a.cedula = e.cedula
                            join Carrera c
                            on e.id_carrera = c.id
                            join Equipo camp
                            on a.campeon = camp.abreviatura
                            join Equipo subcamp
                            on a.subcampeon = subcamp.abreviatura
                        
                            join Partido par
                            on (p.Equipo_E1 = par.Equipo_E1
                            and p.Equipo_E2 = par.Equipo_E2
                            AND p.fecha_partido = par.fecha)
                            or
                            (p.Equipo_E1 = par.Equipo_E2
                            and p.Equipo_E2 = par.Equipo_E1
                            AND p.fecha_partido = par.fecha)

                            join Equipo e1
                            on par.Equipo_E1 = e1.Abreviatura
                            join Equipo e2
                            on par.Equipo_E2 = e2.Abreviatura
                            
                            join Etapa et
                            on par.Etapa = et.id
                            where p.cedula = @cedula
                            and
                            p.Fecha_partido = @fecha
                            and 
                            ((p.Equipo_E1 = @equipo1
                            and p.Equipo_E2 = @equipo2) 
                            or
                            (p.Equipo_E1 = @equipo2
                            and p.Equipo_E2 = @equipo1))";
            
            var result = (
                await _dbConnection.QueryAsync(
                    sqlQuery,
                    new Dictionary<string, object>()
                    {
                        { "cedula", prediccionDto.Cedula},
                        { "fecha", prediccionDto.Fecha },
                        { "equipo1", prediccionDto.Equipo_E1 },
                        { "equipo2", prediccionDto.Equipo_E2 }
                    }
                )
            );

            var prediccion = result.FirstOrDefault();

            if (prediccion == null) throw new ArgumentException("No existe un Prediccion con esa combinación de fecha y equipos.");
            
            var alumno = new Alumno(
                            nombre: (string)prediccion["nombre"],
                            apellido: (string)prediccion["apellido"],
                            cedula: (int)prediccion["cedula"],
                            contrasena: (string)prediccion["contrasena"],
                            fechaNacimiento: (DateTime)prediccion["fecha_nacimiento"],
                            anioIngreso: (int)prediccion["anio_ingreso"],
                            semestreIngreso: (int)prediccion["semestre_ingreso"],
                            puntajeTotal: (int)prediccion["puntaje_total"],
                            campeon: new Equipo(
                                abreviatura: (string)prediccion["campeon"],
                                pais: (string)prediccion["campeon_nombre"]
                            ),
                            subCampeon: new Equipo(
                                abreviatura: (string)prediccion["subcampeon"],
                                pais: (string)prediccion["subcampeon_nombre"]
                            ),
                            carreraPrincipal: new Carrera(
                                id: (int)prediccion["carrera_id"],
                                nombre: (string)prediccion["carrera_nombre"]
                            ));
            var partido = new Partido(
                            fecha: (DateTime)prediccion["fecha"],
                            equipo_E1: new Equipo(
                                abreviatura: (string)prediccion["e1_abreviatura"],
                                pais: (string)prediccion["e1_pais"]
                            ),
                            equipo_E2: new Equipo(
                                abreviatura: (string)prediccion["e2_abreviatura"],
                                pais: (string)prediccion["e2_pais"]
                            ),
                            resultado_E1:(int)prediccion["resultado_e1"],
                            resultado_E2:(int)prediccion["resultado_e2"],
                            etapa: new Etapa(
                                id: (int)prediccion["etapa_id"],
                                nombre: (string)prediccion["etapa_nombre"]
                            )
                    );
        
            return new Prediccion(
                alumno: alumno,
                partido: partido,
                prediccion_e1: (int)prediccion["pe1"],
                prediccion_e2: (int)prediccion["pe2"],
                puntaje: (int)prediccion["puntajepred"]
            );
        }
        catch (PostgresException e)
        {
            throw new ArgumentException("Ocurrió un error al acceder a la base de datos.", e);
        }

    }
    
    public async Task<Prediccion> CreateAsync(Prediccion entity)
    {
        try{
            if (!entity.Partido.EsPosibleCrearPrediccion())
            {
                Console.WriteLine($"{entity.Partido.Fecha.ToString()}");
                throw new ArgumentException("No se puede crear la predicción porque el tiempo límite ha pasado.");
            }
            var sqlQuery = @"
                            WITH inserted AS (
                                INSERT INTO Prediccion 
                                (cedula, Equipo_E1, Equipo_E2, Fecha_partido, Prediccion_E1, Prediccion_E2, Puntaje)
                                VALUES (@cedula, @equipoE1, @equipoE2, @fechaPartido, @prediccionE1, @prediccionE2, 0) 
                                RETURNING *
                            )
                            SELECT 
                                p.Prediccion_E1 as pe1,
                                p.Prediccion_E2 as pe2,
                                COALESCE(p.Puntaje, 0) as PuntajePred,

                                a.*,
                                camp.Pais as campeon_nombre,
                                subcamp.Pais as subcampeon_nombre,
                                c.id as carrera_id,
                                c.nombre as carrera_nombre,

                                par.Fecha as fecha,
                                e1.Abreviatura as E1_Abreviatura,
                                e1.Pais as E1_Pais,
                                e2.Abreviatura as E2_Abreviatura,
                                e2.Pais as E2_Pais,
                                COALESCE(par.Resultado_E1, -1) as Resultado_E1,
                                COALESCE(par.Resultado_E2, -1) as Resultado_E2,
                                et.Id as Etapa_Id,
                                et.Nombre as Etapa_Nombre

                                from inserted p
                                join Alumno a 
                                on p.cedula = a.cedula
                                join Estudia e 
                                on a.cedula = e.cedula
                                join Carrera c 
                                on e.id_carrera = c.id
                                join Equipo camp 
                                on a.campeon = camp.abreviatura
                                join Equipo subcamp 
                                on a.subcampeon = subcamp.abreviatura
                                join Partido par 
                                on 
                                (p.Equipo_E1 = par.Equipo_E1 
                                and p.Equipo_E2 = par.Equipo_E2 
                                and p.Fecha_partido = par.Fecha) 
                                or
                                (p.Equipo_E1 = par.Equipo_E2 
                                and p.Equipo_E2 = par.Equipo_E1 
                                and p.Fecha_partido = par.Fecha)
                                join Equipo e1 
                                on par.Equipo_E1 = e1.Abreviatura
                                join Equipo e2 
                                on par.Equipo_E2 = e2.Abreviatura
                                join Etapa et 
                                on par.Etapa = et.Id
                                where p.cedula = @cedula 
                                and p.Fecha_partido = @fechaPartido
                                and (
                                (p.Equipo_E1 = @equipoE1 and p.Equipo_E2 = @equipoE2) 
                                or 
                                (p.Equipo_E1 = @equipoE2 and p.Equipo_E2 = @equipoE1));
                            ";

            var result = await _dbConnection.QueryAsync(
                sqlQuery,
                new Dictionary<string, object>()
                {
                    { "cedula", entity.Alumno.Cedula },
                    { "equipoE1", entity.Partido.Equipo_E1.Abreviatura },
                    { "equipoE2", entity.Partido.Equipo_E2.Abreviatura },
                    { "fechaPartido", entity.Partido.Fecha },
                    { "prediccionE1", entity.Prediccion_E1 },
                    { "prediccionE2", entity.Prediccion_E2 }
                }
            );

            var prediccion = result.FirstOrDefault();
            if (prediccion == null) throw new ArgumentException("Prediccion no creada con exito.");

            var alumno = new Alumno(
                            nombre: (string)prediccion["nombre"],
                            apellido: (string)prediccion["apellido"],
                            cedula: (int)prediccion["cedula"],
                            contrasena: (string)prediccion["contrasena"],
                            fechaNacimiento: (DateTime)prediccion["fecha_nacimiento"],
                            anioIngreso: (int)prediccion["anio_ingreso"],
                            semestreIngreso: (int)prediccion["semestre_ingreso"],
                            puntajeTotal: (int)prediccion["puntaje_total"],
                            campeon: new Equipo(
                                abreviatura: (string)prediccion["campeon"],
                                pais: (string)prediccion["campeon_nombre"]
                            ),
                            subCampeon: new Equipo(
                                abreviatura: (string)prediccion["subcampeon"],
                                pais: (string)prediccion["subcampeon_nombre"]
                            ),
                            carreraPrincipal: new Carrera(
                                id: (int)prediccion["carrera_id"],
                                nombre: (string)prediccion["carrera_nombre"]
                            ));
            var partido = new Partido(
                            fecha: (DateTime)prediccion["fecha"],
                            equipo_E1: new Equipo(
                                abreviatura: (string)prediccion["e1_abreviatura"],
                                pais: (string)prediccion["e1_pais"]
                            ),
                            equipo_E2: new Equipo(
                                abreviatura: (string)prediccion["e2_abreviatura"],
                                pais: (string)prediccion["e2_pais"]
                            ),
                            resultado_E1:(int)prediccion["resultado_e1"],
                            resultado_E2:(int)prediccion["resultado_e2"],
                            etapa: new Etapa(
                                id: (int)prediccion["etapa_id"],
                                nombre: (string)prediccion["etapa_nombre"]
                            )
                    );
        
            return new Prediccion(
                alumno: alumno,
                partido: partido,
                prediccion_e1: (int)prediccion["pe1"],
                prediccion_e2: (int)prediccion["pe2"],
                puntaje: (int)prediccion["puntajepred"]
            );
        }
        catch (PostgresException e)
        {
            throw new ArgumentException("Ocurrió un error al acceder a la base de datos.", e);
        }
    }
    
    public async Task<Prediccion> UpdateAsync(object id, Prediccion entity)
    {
        try{
            if (!entity.Partido.EsPosibleCrearPrediccion())
            {
                throw new ArgumentException("No se puede editar la predicción porque el tiempo límite ha pasado.");
            }
            await this.GetByIdAsync(id);
            
            PrediccionDTO prediccionDto = (PrediccionDTO)id;
            var sqlQuery = @"
                            WITH updated AS (
                                UPDATE Prediccion 
                                SET Prediccion_E1 = @prediccionE1, 
                                    Prediccion_E2 = @prediccionE2
                                WHERE cedula = @cedula 
                                AND Fecha_partido = @fechaPartido
                                AND ((Equipo_E1 = @equipoE1 AND Equipo_E2 = @equipoE2) 
                                    OR (Equipo_E1 = @equipoE2 AND Equipo_E2 = @equipoE1))
                                RETURNING *
                            )
                            SELECT 
                                p.Prediccion_E1 as pe1,
                                p.Prediccion_E2 as pe2,
                                COALESCE(p.Puntaje, 0) as PuntajePred,

                                a.*,
                                camp.Pais as campeon_nombre,
                                subcamp.Pais as subcampeon_nombre,
                                c.id as carrera_id,
                                c.nombre as carrera_nombre,

                                par.Fecha as fecha,
                                e1.Abreviatura as E1_Abreviatura,
                                e1.Pais as E1_Pais,
                                e2.Abreviatura as E2_Abreviatura,
                                e2.Pais as E2_Pais,
                                COALESCE(par.Resultado_E1, -1) as Resultado_E1,
                                COALESCE(par.Resultado_E2, -1) as Resultado_E2,
                                et.Id as Etapa_Id,
                                et.Nombre as Etapa_Nombre
                                from updated p
                                join Alumno a 
                                on p.cedula = a.cedula
                                join Estudia e 
                                on a.cedula = e.cedula
                                join Carrera c 
                                on e.id_carrera = c.id
                                join Equipo camp 
                                on a.campeon = camp.abreviatura
                                join Equipo subcamp 
                                on a.subcampeon = subcamp.abreviatura
                                join Partido par 
                                on 
                                (p.Equipo_E1 = par.Equipo_E1 
                                and p.Equipo_E2 = par.Equipo_E2 
                                and p.Fecha_partido = par.Fecha) 
                                or 
                                (p.Equipo_E1 = par.Equipo_E2 
                                and p.Equipo_E2 = par.Equipo_E1 
                                and p.Fecha_partido = par.Fecha)
                                join Equipo e1 
                                on par.Equipo_E1 = e1.Abreviatura
                                join Equipo e2 
                                on par.Equipo_E2 = e2.Abreviatura
                                join Etapa et 
                                on par.Etapa = et.Id
                                where p.cedula = @cedula 
                                and p.Fecha_partido = @fechaPartido
                                and (
                                (p.Equipo_E1 = @equipoE1 and p.Equipo_E2 = @equipoE2) 
                                or 
                                (p.Equipo_E1 = @equipoE2 and p.Equipo_E2 = @equipoE1));
                        ";
            
                var result = await _dbConnection.QueryAsync(
                    sqlQuery,
                    new Dictionary<string, object>()
                    {
                        { "cedula", prediccionDto.Cedula },
                        { "equipoE1", prediccionDto.Equipo_E1 },
                        { "equipoE2", prediccionDto.Equipo_E2 },
                        { "fechaPartido", prediccionDto.Fecha },
                        { "prediccionE1", entity.Prediccion_E1 },
                        { "prediccionE2", entity.Prediccion_E2 }
                    }
                );
                var prediccion = result.FirstOrDefault();
                if (prediccion == null) throw new ArgumentException("Prediccion con resultados no se actualizó.");

                var alumno = new Alumno(
                    nombre: (string)prediccion["nombre"],
                    apellido: (string)prediccion["apellido"],
                    cedula: (int)prediccion["cedula"],
                    contrasena: (string)prediccion["contrasena"],
                    fechaNacimiento: (DateTime)prediccion["fecha_nacimiento"],
                    anioIngreso: (int)prediccion["anio_ingreso"],
                    semestreIngreso: (int)prediccion["semestre_ingreso"],
                    puntajeTotal: (int)prediccion["puntaje_total"],
                    campeon: new Equipo(
                        abreviatura: (string)prediccion["campeon"],
                        pais: (string)prediccion["campeon_nombre"]
                    ),
                    subCampeon: new Equipo(
                        abreviatura: (string)prediccion["subcampeon"],
                        pais: (string)prediccion["subcampeon_nombre"]
                    ),
                    carreraPrincipal: new Carrera(
                        id: (int)prediccion["carrera_id"],
                        nombre: (string)prediccion["carrera_nombre"]
                    )
                );

                var partido = new Partido(
                    fecha: (DateTime)prediccion["fecha"],
                    equipo_E1: new Equipo(
                        abreviatura: (string)prediccion["e1_abreviatura"],
                        pais: (string)prediccion["e1_pais"]
                    ),
                    equipo_E2: new Equipo(
                        abreviatura: (string)prediccion["e2_abreviatura"],
                        pais: (string)prediccion["e2_pais"]
                    ),
                    resultado_E1: (int)prediccion["resultado_e1"],
                    resultado_E2: (int)prediccion["resultado_e2"],
                    etapa: new Etapa(
                        id: (int)prediccion["etapa_id"],
                        nombre: (string)prediccion["etapa_nombre"]
                    )
                );

                return new Prediccion(
                    alumno: alumno,
                    partido: partido,
                    prediccion_e1: (int)prediccion["pe1"],
                    prediccion_e2: (int)prediccion["pe2"],
                    puntaje: (int)prediccion["puntajepred"]
                );
         }
        catch (PostgresException e)
        {
            throw new ArgumentException("Ocurrió un error al acceder a la base de datos.", e);
        }
    }
    public async Task<Prediccion[]> SetPuntajeAsync(object id)
    {
        try{
            PartidoDTO partidoDto = (PartidoDTO)id;
        
                var sqlQuery = @"
                                UPDATE Prediccion
                                SET Puntaje = case 
                                    when (prediccion_e1= p.resultado_e1 
                                        and prediccion_e2 = p.resultado_e2) then 4
                                    when (
                                        (prediccion_e1 > prediccion_e2 
                                        and p.resultado_e1 > p.resultado_e2
                                        and (prediccion_e1 <> p.resultado_e1 
                                            or prediccion_e2 <> p.resultado_e2
                                            )
                                        )
                                        or 
                                        (prediccion_e2 > prediccion_e1 
                                        and p.resultado_e2 > p.resultado_e1
                                        and (prediccion_e1 <> p.resultado_e1 
                                            or prediccion_e2 <> p.resultado_e2
                                            )
                                        )
                                        or 
                                        (prediccion_e1 = prediccion_e2 
                                        and p.resultado_e1 = p.resultado_e2
                                        and (prediccion_e1 <> p.resultado_e1 
                                            or prediccion_e2 <> p.resultado_e2
                                            )
                                        )
                                        )then 2
                                    else 0
                                end

                                from partido p
                                Where Prediccion.equipo_e1 = p.equipo_e1 
                                and Prediccion.equipo_e2 = p.equipo_e2 
                                and Prediccion.fecha_partido = p.fecha
                                and Prediccion.equipo_e1 = @e1
                                and Prediccion.equipo_e2 = @e2
                                and Prediccion.fecha_partido = @f
                                RETURNING *;
                                ";

            var result = await _dbConnection.QueryAsync(
                sqlQuery,
                new Dictionary<string, object>()
                {
                    { "e1", partidoDto.Equipo_E1 },
                    { "e2", partidoDto.Equipo_E2 },
                    { "f", partidoDto.Fecha }
                }
            );
            var prediccion = result.FirstOrDefault();
            if (prediccion == null) throw new ArgumentException("Prediccion con resultados no se actualizó.");

            return await this.GetAllByPartidoAsync(partidoDto);
        }
        catch (PostgresException e)
        {
            throw new ArgumentException("Ocurrió un error al acceder a la base de datos.", e);
        }
       
    }
    
    public async Task DeleteAsync(object id)
    {
        try{
            await this.GetByIdAsync(id);
            PrediccionDTO prediccionDto = (PrediccionDTO)id;
            var sqlQuery = @"DELETE FROM Prediccion
                            Where cedula = @cedula
                            and Fecha_partido = @fw
                            and Equipo_E1 = @e1w
                            and Equipo_E2 = @e2w";
            var result = await _dbConnection.QueryAsync(
                sqlQuery,
                new Dictionary<string, object>()
                {
                    { "cedula", prediccionDto.Cedula },
                    { "fw", prediccionDto.Fecha },
                    { "e1w", prediccionDto.Equipo_E1},
                    { "e2w", prediccionDto.Equipo_E2}
                }
            );
        }
        catch (PostgresException e)
        {
            throw new ArgumentException("Ocurrió un error al acceder a la base de datos.", e);
        }
    }

}