using Npgsql;
using PencaAPI.DatabaseConnection;
using PencaAPI.Models;

namespace PencaAPI.Services;

public class EtapaService(PgDatabaseConnection dbConnection)
    : IService<Etapa>
{
    private readonly PgDatabaseConnection _dbConnection = dbConnection;

    public async Task<Etapa[]> GetAllAsync()
    {
        try{
            var result = await _dbConnection.QueryAsync("SELECT * FROM etapa");
            var etapas = result.Select(x => new Etapa(
                    id: (int)x["id"],
                    nombre: (string)x["nombre"]
                )
            ).ToArray();
            return etapas;
        }catch (PostgresException e){
            throw new ArgumentException(e.ToString());
        }
    }

    public async Task<Etapa> GetByIdAsync(object id)
    {
        try{
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
        }catch (PostgresException e){
            throw new ArgumentException(e.ToString());
        }
    }

    public async Task<Etapa> CreateAsync(Etapa entity)
    {
        try{
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
        catch (PostgresException e) when (e.SqlState == PostgresErrorCodes.UniqueViolation)
        {
            throw new ArgumentException("Ya existe una etapa con el mismo identificador.", e);
        }
         catch (PostgresException e)
        {
            // Manejo genérico para otras excepciones de PostgreSQL
            throw new ArgumentException("Ocurrió un error al acceder a la base de datos.", e);
        }
        catch (Exception e)
        {
            throw new ArgumentException("Ocurrió un error al crear la etapa.", e);
        }
    }

    public async Task<Etapa> UpdateAsync(object id, Etapa entity)
    {
        try{
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
        }catch (PostgresException e){
            throw new ArgumentException(e.ToString());
        }
    }

    public async Task DeleteAsync(object id)
    {
        try{
            var queryString = "DELETE FROM etapa WHERE id = @i";
            var parameters = new Dictionary<string, object>()
            {
                { "i", id }
            };

            await _dbConnection.QueryAsync(queryString, parameters);
        }catch (PostgresException e){
            throw new ArgumentException(e.ToString());
        }
    }
    
}