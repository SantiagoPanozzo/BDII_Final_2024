using System.Collections;

namespace PencaAPI.DatabaseConnection;
using Npgsql;

/// <summary>
/// Clase que representa una conexión a una base de datos Postgres
/// </summary>
public class PgDatabaseConnection 
{
    private readonly NpgsqlConnection _connection;
    
    /// <summary>
    /// Constructor de la clase PgDatabaseConnection
    /// </summary>
    /// <param name="connectionString">String de conexión a la base de datos Postgres</param>
    public PgDatabaseConnection(string connectionString)
    {
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
        var dataSource = dataSourceBuilder.Build();
        _connection = dataSource.OpenConnection();
    }

    /// <summary>
    /// Realizar una petición a la base de datos con un string de consulta de manera asíncrona
    /// </summary>
    /// <param name="queryString">String de consulta</param>
    /// <returns>String[] con las columnas que retorna la base de datos, en orden de la petición</returns>
    public async Task<List<Dictionary<string, object>>> QueryAsync(string queryString)
    {
        var results = new List<Dictionary<string, object>>();
        await using var cmd = new NpgsqlCommand(queryString, _connection);

        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            var row = new Dictionary<string, object>();
            for (var i = 0; i < reader.FieldCount; i++)
            {
                row[reader.GetName(i)] = reader.GetValue(i);
            }
            results.Add(row);
        }
        return results;
    }
    
    /// <summary>
    /// Realizar una petición a la base de datos con un string de consulta de manera asíncrona
    /// </summary>
    /// <param name="queryString">String de consulta, con parámetros indicados por un @NOMBRE</param>
    /// <param name="parameters">Diccionario con pares (clave, valor) donde la clave es el NOMBRE de un parámetro y el
    /// valor es el valor del parámetro correspondiente (puede ser de cualquier tipo).</param>
    /// <returns>String[] con las columnas que retorna la base de datos, en orden de la petición</returns>
    public async Task<List<Dictionary<string, object>>> QueryAsync(string queryString, Dictionary<string, object> parameters)
    {
        var results = new List<Dictionary<string, object>>();
        await using var cmd = new NpgsqlCommand(queryString, _connection);
        foreach (var (key, value) in parameters)
        {
            cmd.Parameters.AddWithValue(key, value);
        }
        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            var row = new Dictionary<string, object>();
            for (var i = 0; i < reader.FieldCount; i++)
            {
                row[reader.GetName(i)] = reader.GetValue(i);
            }
            results.Add(row);
        }
        return results;
    }

    
}