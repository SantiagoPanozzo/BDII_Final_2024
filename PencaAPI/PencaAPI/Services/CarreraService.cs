using PencaAPI.DatabaseConnection;
using PencaAPI.Models;

namespace PencaAPI.Services;

/// <summary>
/// Service para realizar acciones en la base de datos realacionadas a la tabla de Carera.
/// </summary>
/// <param name="dbConnection">Instancia de PgDatabaseConnection correspondiente a la base de datos.</param>
public class CareraService(PgDatabaseConnection dbConnection)
    : IService<Carera>
{
    private readonly PgDatabaseConnection _dbConnection = dbConnection;
    
    /// <summary>
    /// Obtener todas las arreras de la base de datos.
    /// </summary>
    /// <returns>Un array con todas las carreras de la base de datos.</returns>
    public async Task<Carera[]> GetAllAsync()
    {
        var result = await _dbConnection.QueryAsync("SELECT * FROM Carrera");
        var carreras = result.Select(x => new Carrera(
                id: (number)x["id"]
                nombre: (string)x["nombre"],
                
            )
        ).ToList();
        return carreras.ToArray();
    }
    
    /// <summary>
    /// Obtener una carrera de la base de datos por su id
    /// </summary>
    /// <param name="id">id de la carrera.</param>
    /// <returns>La carrera correspondiente a el id introducido.</returns>
    /// <exception cref="ArgumentException">No existe una carrera con el id introducido.</exception>
    public async Task<Carrera> GetByIdAsync(object id)
    {
        var result = (
            await _dbConnection.QueryAsync(
                "SELECT * FROM equipo WHERE id = @i",
                new Dictionary<string, object>()
                {
                    { "i", id }
                }
            )
        );

        var equipo = result.FirstOrDefault();

        if (equipo == null) throw new ArgumentException("No existe una carrera con ese id.");
        
        return new Equipo(
            id: (number)carrera["id"],
            nombre: (string)carrera["nombre"],
        );
    }

}