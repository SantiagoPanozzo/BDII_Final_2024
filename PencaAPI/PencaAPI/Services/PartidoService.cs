using PencaAPI.DatabaseConnection;
using PencaAPI.Models;
using PencaAPI.DTOs;
using System.Runtime.InteropServices;
using Npgsql;

namespace PencaAPI.Services;

/// <summary>
/// Service para realizar acciones en la base de datos realacionadas a la tabla de Carera.
/// </summary>
/// <param name="dbConnection">Instancia de PgDatabaseConnection correspondiente a la base de datos.</param>
public class PartidoService(PgDatabaseConnection dbConnection, PrediccionService prediccionService, AlumnoService alumnoService)
    : IService<Partido>
{
    private readonly PgDatabaseConnection _dbConnection = dbConnection;
    private readonly PrediccionService _prediccionService = prediccionService;
    private readonly AlumnoService _alumnoService = alumnoService;
    
    /// <summary>
    /// Obtener todas las arreras de la base de datos.
    /// </summary>
    /// <returns>Un array con todas las carreras de la base de datos.</returns>
    public async Task<Partido[]> GetAllAsync()
    {
        try{
            var sqlQuery = @"Select 
                                p.Fecha,
                                e1.Abreviatura as E1_Abreviatura,
                                e1.Pais as E1_Pais,
                                e2.Abreviatura as E2_Abreviatura,
                                e2.Pais as E2_Pais,
                                coalesce(p.Resultado_E1, -1) as Resultado_E1,
                                coalesce(p.Resultado_E2, -1) as Resultado_E2,
                                e.Id as Etapa_Id,
                                e.Nombre as Etapa_Nombre
                            From Partido p
                            join Etapa e
                            on p.Etapa = e.Id
                            join Equipo e1
                            on p.Equipo_E1 = e1.Abreviatura
                            join Equipo e2
                            on p.Equipo_E2 = e2.Abreviatura";
            var result = await _dbConnection.QueryAsync(sqlQuery);
            var partidos = result.Select(x => new Partido(
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
                )
            ).ToList();
                return partidos.ToArray();
            }catch (NpgsqlException e){
            throw new ArgumentException(e.ToString());
            }
        }
        
        /// <summary>
        /// Obtener una carrera de la base de datos por su id
        /// </summary>
        /// <param name="id">id de la carrera.</param>
        /// <returns>La carrera correspondiente a el id introducido.</returns>
        /// <exception cref="ArgumentException">No existe una carrera con el id introducido.</exception>
        public async Task<Partido> GetByIdAsync(object id)
        {   
            try{
                PartidoDTO partidoDto = (PartidoDTO)id;
                var sqlQuery = @"Select 
                                    p.Fecha,
                                    e1.Abreviatura as E1_Abreviatura,
                                    e1.Pais as E1_Pais,
                                    e2.Abreviatura as E2_Abreviatura,
                                    e2.Pais as E2_Pais,
                                    coalesce(p.Resultado_E1, -1) as Resultado_E1,
                                    coalesce(p.Resultado_E2, -1) as Resultado_E2,
                                    e.Id as Etapa_Id,
                                    e.Nombre as Etapa_Nombre
                                From Partido p
                                join Etapa e
                                on p.Etapa = e.Id
                                join Equipo e1
                                on p.Equipo_E1 = e1.Abreviatura
                                join Equipo e2
                                on p.Equipo_E2 = e2.Abreviatura
                                where p.Fecha = @fecha
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
                            { "fecha", partidoDto.Fecha },
                            { "equipo1", partidoDto.Equipo_E1 },
                            { "equipo2", partidoDto.Equipo_E2 }
                        }
                    )
                );

                var partido = result.FirstOrDefault();

                if (partido == null) throw new ArgumentException("No existe un partido con esa combinación de fecha y equipos.");
                
                var equipo1 = new Equipo
                (
                    abreviatura: (string)partido["e1_abreviatura"],
                    pais: (string)partido["e1_pais"]
                );
                var equipo2 = new Equipo
                (
                    abreviatura: (string)partido["e2_abreviatura"],
                    pais: (string)partido["e2_pais"]
                );
                var et = new Etapa
                (
                    id: (int)partido["etapa_id"],
                    nombre: (string)partido["etapa_nombre"]
                );
                return new Partido(
                    fecha: (DateTime)partido["fecha"],
                    equipo_E1: equipo1,
                    equipo_E2: equipo2,
                    resultado_E1:(int)partido["resultado_e1"],
                    resultado_E2:(int)partido["resultado_e2"],
                    etapa: et
                );
            }catch (NpgsqlException e){
                throw new ArgumentException(e.ToString());
            }
        }
        
        public async Task<Partido> CreateAsync(Partido entity)
        {
            try{
                var sqlQuery = @"
                            WITH inserted as (
                            INSERT INTO Partido 
                            (Fecha, Equipo_E1, Equipo_E2, Etapa)
                            VALUES (@f, @e1, @e2, @e) 
                            RETURNING *
                            )
                            Select 
                                p.Fecha,
                                p.Equipo_E1 as E1_Abreviatura,
                                e1.Pais as E1_Pais,
                                p.Equipo_E2 as E2_Abreviatura,
                                e2.Pais as E2_Pais,
                                coalesce(p.Resultado_E1, -1) as Resultado_E1,
                                coalesce(p.Resultado_E2, -1) as Resultado_E2,
                                p.Etapa as Etapa_Id,
                                e.Nombre as Etapa_Nombre
                            From inserted p
                            join Etapa e
                            on p.Etapa = e.Id
                            join Equipo e1
                            on p.Equipo_E1 = e1.Abreviatura
                            join Equipo e2
                            on p.Equipo_E2 = e2.Abreviatura
                            ";
            var result = await _dbConnection.QueryAsync(
                sqlQuery,
                new Dictionary<string, object>()
                {
                    { "f", entity.Fecha },
                    { "e1", entity.Equipo_E1.Abreviatura },
                    { "e2", entity.Equipo_E2.Abreviatura},
                    { "e", entity.Etapa.Id}
                }
            );
            Console.WriteLine(result.FirstOrDefault().ToString());

            var partido = result.FirstOrDefault();
            if (partido == null) throw new ArgumentException("Partido no creado con exito.");

            var equipo1 = new Equipo
            (
                abreviatura: (string)partido["e1_abreviatura"],
                pais: (string)partido["e1_pais"]
            );
            var equipo2 = new Equipo
            (
                abreviatura: (string)partido["e2_abreviatura"],
                pais: (string)partido["e2_pais"]
            );
            var et = new Etapa
            (
                id: (int)partido["etapa_id"],
                nombre: (string)partido["etapa_nombre"]
            );

            return new Partido(
                fecha: (DateTime)partido["fecha"],
                equipo_E1: equipo1,
                equipo_E2: equipo2,
                resultado_E1: (int)partido["resultado_e1"],
                resultado_E2: (int)partido["resultado_e2"],
                etapa: et
            );
        }catch (NpgsqlException e){
            throw new ArgumentException(e.ToString());
        }
    }
    
    public async Task<Partido> UpdateAsync(object id, Partido entity)
    {
        try{
            await this.GetByIdAsync(id);
            
            PartidoDTO partidoDto = (PartidoDTO)id;
            var sqlQuery = @"
                            WITH updated as (
                            UPDATE Partido  
                            SET 
                            Resultado_E1 = @r1,
                            Resultado_E2 = @r2
                            Where Fecha = @fw 
                            and Equipo_E1 = @e1w
                            and Equipo_E2 = @e2w
                            RETURNING *
                            )
                            Select 
                                p.Fecha,
                                p.Equipo_E1 as E1_Abreviatura,
                                e1.Pais as E1_Pais,
                                p.Equipo_E2 as E2_Abreviatura,
                                e2.Pais as E2_Pais,
                                coalesce (p.Resultado_E1, -1) as Resultado_E1,
                                coalesce (p.Resultado_E2, -1) as Resultado_E2,
                                p.Etapa as Etapa_Id,
                                e.Nombre as Etapa_Nombre
                            From updated p
                            join Etapa e
                            on p.Etapa = e.Id
                            join Equipo e1
                            on p.Equipo_E1 = e1.Abreviatura
                            join Equipo e2
                            on p.Equipo_E2 = e2.Abreviatura";

            var sqlQuerySinResultados = 
                            @"
                            WITH updated as (
                            UPDATE Partido 
                            set Fecha = @f , 
                            Equipo_E1 = @e1, 
                            Equipo_E2 = @e2,
                            Etapa = @e
                            Where Fecha = @fw
                            and Equipo_E1 = @e1w
                            and Equipo_E2 = @e2w
                            RETURNING *
                            )
                            Select 
                                p.Fecha,
                                p.Equipo_E1 as E1_Abreviatura,
                                e1.Pais as E1_Pais,
                                p.Equipo_E2 as E2_Abreviatura,
                                e2.Pais as E2_Pais,
                                coalesce (p.Resultado_E1, -1) as Resultado_E1,
                                coalesce (p.Resultado_E2, -1) as Resultado_E2,
                                p.Etapa as Etapa_Id,
                                e.Nombre as Etapa_Nombre
                            From updated p
                            join Etapa e
                            on p.Etapa = e.Id
                            join Equipo e1
                            on p.Equipo_E1 = e1.Abreviatura
                            join Equipo e2
                            on p.Equipo_E2 = e2.Abreviatura";
            if ((entity.Resultado_E1 != null ||entity.Resultado_E1 >= 0) && 
                (entity.Resultado_E2 != null ||entity.Resultado_E2 >= 0))
            {
                var result = await _dbConnection.QueryAsync(
                    sqlQuery,
                    new Dictionary<string, object>()
                    {
                        { "r1", entity.Resultado_E1},
                        { "r2", entity.Resultado_E2},
                        { "fw", partidoDto.Fecha },
                        { "e1w", partidoDto.Equipo_E1},
                        { "e2w", partidoDto.Equipo_E2}
                    }
                );
                var partido = result.FirstOrDefault();
                if (partido == null) throw new ArgumentException("Partido con resultados no se actualizó.");

                var equipo1 = new Equipo
                (
                    abreviatura: (string)partido["e1_abreviatura"],
                    pais: (string)partido["e1_pais"]
                );
                var equipo2 = new Equipo
                (
                    abreviatura: (string)partido["e2_abreviatura"],
                    pais: (string)partido["e2_pais"]
                );
                var et = new Etapa
                (
                    id: (int)partido["etapa_id"],
                    nombre: (string)partido["etapa_nombre"]
                );

                var part = new Partido(
                        fecha: (DateTime)partido["fecha"],
                        equipo_E1: equipo1,
                        equipo_E2: equipo2,
                        resultado_E1:(int)partido["resultado_e1"],
                        resultado_E2:(int)partido["resultado_e2"],
                        etapa: et
                );
            
                var resultPuntajes=  await _prediccionService.SetPuntajeAsync(partidoDto);
                var prediciones = resultPuntajes.FirstOrDefault();
                if (prediciones == null) throw new ArgumentException("Partido con resultados no se actualizó.");
                if(part.Etapa.Id == 4)
                {
                    await _alumnoService.SetPuntajeCampeonSubcampeonAsync(part);
                }

                return part;
            }
            else
            {   
                var result = await _dbConnection.QueryAsync(
                    sqlQuerySinResultados,
                    new Dictionary<string, object>()
                    {
                        { "f", entity.Fecha},
                        { "e1", entity.Equipo_E1.Abreviatura},
                        { "e2", entity.Equipo_E2.Abreviatura},
                        { "e", entity.Etapa.Id},
                        { "fw", partidoDto.Fecha },
                        { "e1w", partidoDto.Equipo_E1},
                        { "e2w", partidoDto.Equipo_E2}
                    }
                );
            
                var partido = result.FirstOrDefault();
                if (partido == null) throw new ArgumentException("Partido SIN resultados no se actualizó");

                var equipo1 = new Equipo
                (
                    abreviatura: (string)partido["e1_abreviatura"],
                    pais: (string)partido["e1_pais"]
                );
                var equipo2 = new Equipo
                (
                    abreviatura: (string)partido["e2_abreviatura"],
                    pais: (string)partido["e2_pais"]
                );
                var et = new Etapa
                (
                    id: (int)partido["etapa_id"],
                    nombre: (string)partido["etapa_nombre"]
                );
                return new Partido(
                        fecha: (DateTime)partido["fecha"],
                        equipo_E1: equipo1,
                        equipo_E2: equipo2,
                        resultado_E1:(int)partido["resultado_e1"],
                        resultado_E2:(int)partido["resultado_e2"],
                        etapa: et
                );
            }
        }catch (NpgsqlException e){
            throw new ArgumentException(e.ToString());
        }
    }
    
    public async Task DeleteAsync(object id)
    {
        try{
            await this.GetByIdAsync(id);
            PartidoDTO partidoDto = (PartidoDTO)id;
            var sqlQuery = @"DELETE FROM Partido
                            Where Fecha = @fw
                            and Equipo_E1 = @e1w
                            and Equipo_E2 = @e2w";
            var result = await _dbConnection.QueryAsync(
                sqlQuery,
                new Dictionary<string, object>()
                {
                    { "fw", partidoDto.Fecha },
                    { "e1w", partidoDto.Equipo_E1},
                    { "e2w", partidoDto.Equipo_E2}
                }
            );
        }catch (NpgsqlException e){
            throw new ArgumentException(e.ToString());
        }
    }

}
