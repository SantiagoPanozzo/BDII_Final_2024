using PencaAPI.DatabaseConnection;
using PencaAPI.Models;
using PencaAPI.DTOs;

namespace PencaAPI.Services;

/// <summary>
/// Service para realizar acciones en la base de datos realacionadas a la tabla de Carera.
/// </summary>
/// <param name="dbConnection">Instancia de PgDatabaseConnection correspondiente a la base de datos.</param>
public class PartidoService(PgDatabaseConnection dbConnection)
    : IService<Partido>
{
    private readonly PgDatabaseConnection _dbConnection = dbConnection;
    
    /// <summary>
    /// Obtener todas las arreras de la base de datos.
    /// </summary>
    /// <returns>Un array con todas las carreras de la base de datos.</returns>
    public async Task<Partido[]> GetAllAsync()
    {
         var sqlQuery = @"Select 
                            p.Fecha,
                            e1.Abreviatura as E1_Abreviatura,
                            e1.Pais as E1_Pais,
                            e2.Abreviatura as E2_Abreviatura,
                            e2.Pais as E2_Pais,
                            IFNULL(p.Resultado_E1, -1) as Resultado_E1,
                            IFNULL(p.Resultado_E2, -1) as Resultado_E2,
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
                fecha: (DateTime)x["Fecha"],
                equipoE1: new Equipo(
                    abreviatura: (string)x["E1_Abreviatura"],
                    pais: (string)x["E1_Pais"]
                ),
                equipoE2: new Equipo(
                    abreviatura: (string)x["E2_Abreviatura"],
                    pais: (string)x["E2_Pais"]
                ),
                resultadoE1:(int)x["Resultado_E1"],
                resultadoE2:(int)x["Resultado_E2"],
                etapa: new Etapa(
                    id: (int)x["Etapa_Id"],
                    nombre: (string)x["Etapa_Nombre"]
                )
            )
        ).ToList();
        return partidos.ToArray();
    }
    
    /// <summary>
    /// Obtener una carrera de la base de datos por su id
    /// </summary>
    /// <param name="id">id de la carrera.</param>
    /// <returns>La carrera correspondiente a el id introducido.</returns>
    /// <exception cref="ArgumentException">No existe una carrera con el id introducido.</exception>
    public async Task<Partido> GetByIdAsync(object id)
    {   
        PartidoDTO partidoDto = (PartidoDTO)id;
        var sqlQuery = @"Select 
                            p.Fecha,
                            e1.Abreviatura as E1_Abreviatura,
                            e1.Pais as E1_Pais,
                            e2.Abreviatura as E2_Abreviatura,
                            e2.Pais as E2_Pais,
                            IFNULL(p.Resultado_E1, -1) as Resultado_E1,
                            IFNULL(p.Resultado_E2, -1) as Resultado_E2,
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
            abreviatura: (string)partido["E1_Abreviatura"],
            pais: (string)partido["E1_Pais"]
        );
        var equipo2 = new Equipo
        (
            abreviatura: (string)partido["E2_Abreviatura"],
            pais: (string)partido["E2_Pais"]
        );
        var et = new Etapa
        (
            id: (int)partido["Etapa_Id"],
            nombre: (string)partido["Etapa_Nombre"]
        );
        return new Partido(
            fecha: (DateTime)partido["Fecha"],
            equipoE1: equipo1,
            equipoE2: equipo2,
            resultadoE1:(int)partido["Resultado_E1"],
            resultadoE2:(int)partido["Resultado_E2"],
            etapa: et
        );
    }
    
    public async Task<Partido> CreateAsync(Partido entity)
    {
         var sqlQuery = @"INSERT INTO Partido (Fecha, Equipo_E1, Equipo_E2, Etapa)
                         VALUES (@f, @e1, @e2, @e)";
        var result = await _dbConnection.QueryAsync(
            sqlQuery,
            new Dictionary<string, object>()
            {
                { "f", entity.Fecha },
                { "e1", entity.Equipo_E1 },
                { "e2", entity.Equipo_E2},
                { "e", entity.Etapa}
            }
        );

        var partido = result.FirstOrDefault();
        if (partido == null) throw new ArgumentException("Partido no creado con exito.");

        var equipo1 = new Equipo
        (
            abreviatura: (string)partido["E1_Abreviatura"],
            pais: (string)partido["E1_Pais"]
        );
        var equipo2 = new Equipo
        (
            abreviatura: (string)partido["E2_Abreviatura"],
            pais: (string)partido["E2_Pais"]
        );
        var et = new Etapa
        (
            id: (int)partido["Etapa_Id"],
            nombre: (string)partido["Etapa_Nombre"]
        );

        return new Partido(
            fecha: (DateTime)partido["Fecha"],
            equipoE1: equipo1,
            equipoE2: equipo2,
            etapa: et
        );
    }
    
    public async Task<Partido> UpdateAsync(object id, Partido entity)
    {
        PartidoDTO partidoDto = (PartidoDTO)id;
        var sqlQuerySinResultados = @"UPDATE Partido 
                        set Fecha = @f , 
                        Equipo_E1 = @e1, 
                        Equipo_E2 = @e2, 
                        Resultado_E1 = @r1,
                        Resultado_E2 = @r2,
                        Etapa = @e
                        Where Fecha = @f 
                        and Equipo_E1 = @e1
                        and Equipo_E2 = @e2
                        RETURNING *";

        var sqlQuery = 
                        @"UPDATE Partido 
                        set Fecha = @f , 
                        Equipo_E1 = @e1, 
                        Equipo_E2 = @e2,
                        Etapa = @e
                        Where Fecha = @fw
                        and Equipo_E1 = @e1w
                        and Equipo_E2 = @e2w
                        RETURNING *";
        if ((entity.Resultado_E1 != null ||entity.Resultado_E1 >= 0) && 
            (entity.Resultado_E2 != null ||entity.Resultado_E2 >= 0))
        {
            var result = await _dbConnection.QueryAsync(
                sqlQuery,
                new Dictionary<string, object>()
                {
                    { "f", entity.Fecha},
                    { "e1", entity.Equipo_E1.Abreviatura},
                    { "e2", entity.Equipo_E2.Abreviatura},
                    { "r1", entity.Resultado_E1},
                    { "r2", entity.Resultado_E2},
                    { "e", entity.Etapa.Id},
                    { "fw", partidoDto.Fecha },
                    { "e1w", partidoDto.Equipo_E1},
                    { "e2w", partidoDto.Equipo_E2}
                }
            );
            var partido = result.FirstOrDefault();
            if (partido == null) throw new ArgumentException("Partido no creado con exito.");

            var equipo1 = new Equipo
            (
                abreviatura: (string)partido["E1_Abreviatura"],
                pais: (string)partido["E1_Pais"]
            );
            var equipo2 = new Equipo
            (
                abreviatura: (string)partido["E2_Abreviatura"],
                pais: (string)partido["E2_Pais"]
            );
            var et = new Etapa
            (
                id: (int)partido["Etapa_Id"],
                nombre: (string)partido["Etapa_Nombre"]
            );
            return new Partido(
                    fecha: (DateTime)partido["Fecha"],
                    equipoE1: equipo1,
                    equipoE2: equipo2,
                    resultadoE1:(int)partido["Resultado_E1"],
                    resultadoE2:(int)partido["Resultado_E2"],
                    etapa: et
            );
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
            var selectQuery = @"
                            SELECT Fecha,
                                Equipo_E1,
                                Equipo_E2,
                                IFNULL(Resultado_E1, -1) AS Resultado_E1,
                                IFNULL(Resultado_E2, -1) AS Resultado_E2,
                                Etapa
                            FROM Partido
                            WHERE Fecha = @f
                            AND Equipo_E1 = @e1
                            AND Equipo_E2 = @e2";
            
            var resultSelect = await _dbConnection.QueryAsync(
                sqlQuerySinResultados,
                new Dictionary<string, object>()
                {
                    { "f", entity.Fecha},
                    { "e1", entity.Equipo_E1.Abreviatura},
                    { "e2", entity.Equipo_E2.Abreviatura}
                }
            );
            var partido = resultSelect.FirstOrDefault();
            if (partido == null) throw new ArgumentException("Partido no creado con exito.");

            var equipo1 = new Equipo
            (
                abreviatura: (string)partido["E1_Abreviatura"],
                pais: (string)partido["E1_Pais"]
            );
            var equipo2 = new Equipo
            (
                abreviatura: (string)partido["E2_Abreviatura"],
                pais: (string)partido["E2_Pais"]
            );
            var et = new Etapa
            (
                id: (int)partido["Etapa_Id"],
                nombre: (string)partido["Etapa_Nombre"]
            );
            return new Partido(
                    fecha: (DateTime)partido["Fecha"],
                    equipoE1: equipo1,
                    equipoE2: equipo2,
                    etapa: et
            );
        }
    }
    
    public async Task DeleteAsync(object id)
    {
        var result = await _dbConnection.QueryAsync(
            "DELETE FROM carrera WHERE id = @i RETURNING *",
            new Dictionary<string, object>()
            {
                { "i", id }
            }
        );
    }

}