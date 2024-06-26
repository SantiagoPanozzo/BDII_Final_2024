using Npgsql;
using PencaAPI.DatabaseConnection;
using PencaAPI.Models;

namespace PencaAPI.Services;

/// <summary>
/// Service para realizar acciones en la base de datos realacionadas a la tabla de Carera.
/// </summary>
/// <param name="dbConnection">Instancia de PgDatabaseConnection correspondiente a la base de datos.</param>
public class CarreraService(PgDatabaseConnection dbConnection)
    : IService<Carrera>
{
    private readonly PgDatabaseConnection _dbConnection = dbConnection;
    
    /// <summary>
    /// Obtener todas las arreras de la base de datos.
    /// </summary>
    /// <returns>Un array con todas las carreras de la base de datos.</returns>
    public async Task<Carrera[]> GetAllAsync()
    {
        try{
            var result = await _dbConnection.QueryAsync("SELECT * FROM Carrera");
            var carreras = result.Select(x => new Carrera(
                    id: (int)x["id"],
                    nombre: (string)x["nombre"]
                    
                )
            ).ToList();
            return carreras.ToArray();
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
    public async Task<Carrera> GetByIdAsync(object id)
    {
        try{
            var result = (
                await _dbConnection.QueryAsync(
                    "SELECT * FROM carrera WHERE id = @i",
                    new Dictionary<string, object>()
                    {
                        { "i", id }
                    }
                )
            );

            var carrera = result.FirstOrDefault();

            if (carrera == null) throw new ArgumentException("No existe una carrera con ese id.");
            
            return new Carrera(
                id: (int)carrera["id"],
                nombre: (string)carrera["nombre"]
            );
        }
        catch (PostgresException e)
        {
            throw new ArgumentException("Ocurrió un error al acceder a la base de datos.", e);
        }
    }
    
    public async Task<Carrera> CreateAsync(Carrera entity)
    {
        try{
            var result = await _dbConnection.QueryAsync(
                "INSERT INTO carrera (nombre) VALUES (@n) RETURNING *",
                new Dictionary<string, object>()
                {
                    { "n", entity.Nombre }
                }
            );

            var carrera = result.FirstOrDefault();

            return new Carrera(
                id: (int)carrera["id"],
                nombre: (string)carrera["nombre"]
            );
        }
        catch (PostgresException e)
        {
            throw new ArgumentException("Ocurrió un error al acceder a la base de datos.", e);
        }
    }
    
    public async Task<Carrera> UpdateAsync(object id, Carrera entity)
    {
        try{
            var result = await _dbConnection.QueryAsync(
                "UPDATE carrera SET nombre = @n WHERE id = @i RETURNING *",
                new Dictionary<string, object>()
                {
                    { "n", entity.Nombre },
                    { "i", id }
                }
            );

            var carrera = result.FirstOrDefault();

            return new Carrera(
                id: (int)carrera["id"],
                nombre: (string)carrera["nombre"]
            );
        }
        catch (PostgresException e)
        {
            throw new ArgumentException("Ocurrió un error al acceder a la base de datos.", e);
        }
    }
    
    public async Task DeleteAsync(object id)
    {
        try{
            var result = await _dbConnection.QueryAsync(
                "DELETE FROM carrera WHERE id = @i RETURNING *",
                new Dictionary<string, object>()
                {
                    { "i", id }
                }
            );
        }
        catch (PostgresException e)
        {
            throw new ArgumentException("Ocurrió un error al acceder a la base de datos.", e);
        }
    }

}