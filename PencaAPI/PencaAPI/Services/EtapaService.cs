using PencaAPI.DatabaseConnection;
using PencaAPI.Models;

namespace PencaAPI.Services;

public class EtapaService(PgDatabaseConnection dbConnection)
    : IService<Etapa>
{
    private readonly PgDatabaseConnection _dbConnection = dbConnection;

    public async Task<Etapa[]> GetAllAsync()
    {
        var result = await _dbConnection.QueryAsync("SELECT * FROM etapa");
        var etapas = result.Select(x => new Etapa(
                id: (int)x["id"],
                nombre: (string)x["nombre"]
            )
        ).ToArray();
        return etapas;
    }

    public async Task<Etapa> GetByIdAsync(object id)
    {
        var result = await _dbConnection.QueryAsync(
            "SELECT * FROM etapa WHERE id = @i",
            new Dictionary<string, object>()
            {
                { "i", id }
            }
        );

        var etapa = result.FirstOrDefault();

        if (etapa == null) throw new ArgumentException("No existe una etapa con ese id.");

        return new Etapa(
            id: (int)etapa["id"],
            nombre: (string)etapa["nombre"]
        );
    }

    public async Task<Etapa> CreateAsync(Etapa entity)
    {

        var queryString = "INSERT INTO etapa (nombre) VALUES (@n) RETURNING *";
        var parameters = new Dictionary<string, object>()
        {
            { "n", entity.Nombre }
        };
        
        var result = await _dbConnection.QueryAsync(queryString, parameters);

        var etapa = result.FirstOrDefault();

        if (etapa == null)
            throw new ArgumentException("No se pudo crear la etapa.");

        Console.WriteLine("Id de la etapa:" + (int)(etapa["id"]));
        
        return new Etapa(
            id: (int)etapa["id"],
            nombre: (string)etapa["nombre"]
        );
    }

    public async Task<Etapa> UpdateAsync(object id, Etapa entity)
    {
        var queryString = "UPDATE etapa SET id = @i, nombre = @n WHERE id = @i";
        var parameters = new Dictionary<string, object>()
        {
            { "i", entity.Id },
            { "n", entity.Nombre }
        };
        
        var result = await _dbConnection.QueryAsync(queryString, parameters);

        var etapa = result.FirstOrDefault();

        if (etapa == null)
            throw new ArgumentException("No se pudo editar la etapa.");

        return new Etapa(
            id: (int)etapa["id"],
            nombre: (string)etapa["nombre"]
        );
    }

    public async Task DeleteAsync(object id)
    {
        var queryString = "DELETE FROM etapa WHERE id = @i";
        var parameters = new Dictionary<string, object>()
        {
            { "i", id }
        };

        await _dbConnection.QueryAsync(queryString, parameters);
    }
    
}