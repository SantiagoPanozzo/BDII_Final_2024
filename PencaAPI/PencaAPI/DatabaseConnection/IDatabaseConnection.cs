namespace PencaAPI.DatabaseConnection;

/// <summary>
/// Interfaz que representa una conexión a una base de datos genérica.
/// </summary>
public interface IDatabaseConnection
{
    /// <summary>
    /// Realizar una petición a la base de datos con un string de consulta de manera asíncrona.
    /// </summary>
    /// <param name="queryString">String de consulta, con parámetros indicados por un @NOMBRE</param>
    /// <returns>Lista de filas de la base de datos. Cada fila es un Diccionario que contiene todas las columnas para
    /// esa fila, donde la clave es un string con el nombre de la columna y su valor es un objeto genérico con el dato
    /// almacenado para esa columna en esa fila.</returns>
    Task<List<Dictionary<string, object>>> QueryAsync(string queryString);
    
    /// <summary>
    /// Realizar una petición a la base de datos con un string de consulta de manera asíncrona, utilizando parámetros.
    /// </summary>
    /// <param name="queryString">String de consulta, con parámetros indicados por un @NOMBRE</param>
    /// <param name="parameters">Diccionario con pares (clave, valor) donde la clave es el NOMBRE de un parámetro y el
    /// valor es el valor del parámetro correspondiente (puede ser de cualquier tipo).</param>
    /// <returns>Lista de filas de la base de datos. Cada fila es un Diccionario que contiene todas las columnas para
    /// esa fila, donde la clave es un string con el nombre de la columna y su valor es un objeto genérico con el dato
    /// almacenado para esa columna en esa fila.</returns>
    Task<List<Dictionary<string, object>>> QueryAsync(string queryString, Dictionary<string, object> parameters);
}