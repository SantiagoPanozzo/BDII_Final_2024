using System.Collections;

namespace PencaAPI.DatabaseConnection;
using Npgsql;

/// <summary>
/// Clase que representa una conexión a una base de datos Postgres
/// </summary>
public class PgDatabaseConnection : IDatabaseConnection
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
    public async Task<string[]> QueryAsync(string queryString)
    {
        await using var cmd = new NpgsqlCommand(queryString, _connection);
        // reader es el lector de lo que devuelve la base de datos
        await using var reader = await cmd.ExecuteReaderAsync();
        var results = new List<string>();
        // leemos los resultados de la base de datos
        while (await reader.ReadAsync())
        {
            // agregamos los datos a una lista, van a llegar uno por uno los datos de la columna name
            results.Add(reader.GetString(0));
        }
        return results.ToArray();
    }
    
    /// <summary>
    /// Realizar una petición a la base de datos con un string de consulta de manera asíncrona
    /// </summary>
    /// <param name="queryString">String de consulta, con parámetros indicados por un @NOMBRE</param>
    /// <param name="parameters">Diccionario con pares (clave, valor) donde la clave es el NOMBRE de un parámetro y el
    /// valor es el valor del parámetro correspondiente (puede ser de cualquier tipo).</param>
    /// <returns>String[] con las columnas que retorna la base de datos, en orden de la petición</returns>
    public async Task<string[]> QueryAsync(string queryString, Dictionary<string, object> parameters)
    {
        await using var cmd = new NpgsqlCommand(queryString, _connection);
        foreach (var (key, value) in parameters)
        {
            cmd.Parameters.AddWithValue(key, value);
        }
        // reader es el lector de lo que devuelve la base de datos
        await using var reader = await cmd.ExecuteReaderAsync();
        var results = new List<string>();
        // leemos los resultados de la base de datos
        while (await reader.ReadAsync())
        {
            // agregamos los datos a una lista, van a llegar uno por uno los datos de la columna name
            results.Add(reader.GetString(0));
        }
        return results.ToArray();
    }
    
}