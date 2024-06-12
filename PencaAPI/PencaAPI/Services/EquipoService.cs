using PencaAPI.DatabaseConnection;
using PencaAPI.Models;

namespace PencaAPI.Services;

/// <summary>
/// Service para realizar acciones en la base de datos realacionadas a la tabla de Equipo.
/// </summary>
/// <param name="dbConnection">Instancia de PgDatabaseConnection correspondiente a la base de datos.</param>
public class EquipoService(PgDatabaseConnection dbConnection)
    : IService<Equipo>
{
    private readonly PgDatabaseConnection _dbConnection = dbConnection;
    
    /// <summary>
    /// Obtener todos los Equipos de la base de datos.
    /// </summary>
    /// <returns>Un array con todos los equipos de la base de datos.</returns>
    public async Task<Equipo[]> GetAllAsync()
    {
        var result = await _dbConnection.QueryAsync("SELECT * FROM Equipo");
        var equipos = result.Select(x => new Equipo(
                abreviatura : (string)x["abreviatura"]
                pais: (string)x["pais"],
                
            )
        ).ToList();
        return equipos.ToArray();
    }
    
    /// <summary>
    /// Obtener un equipo de la base de datos por su abreviatura.
    /// </summary>
    /// <param name="id">abreviatura del equipo.</param>
    /// <returns>El alumno correspondiente a la cédula introducida.</returns>
    /// <exception cref="ArgumentException">No existe una equipo con esa abreviatura.</exception>
    public async Task<Equipo> GetByIdAsync(object id)
    {
        var result = (
            await _dbConnection.QueryAsync(
                "SELECT * FROM equipo WHERE abreviatura = @a",
                new Dictionary<string, object>()
                {
                    { "a", id }
                }
            )
        );

        var equipo = result.FirstOrDefault();

        if (equipo == null) throw new ArgumentException("No existe un equipo con esa abreviatura.");
        
        return new Equipo(
            abreviatura: (string)equipo["abreviatura"],
            pais: (string)alumno["pais"],
        );
    }

}