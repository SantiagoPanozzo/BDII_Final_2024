using Npgsql;

// String de conexión a la base de datos
// Server: Nombre del contenedor de Docker de la base de datos
// Port: Puerto interno de la base de datos (5432, el puerto 8001 lo usamos solo para DBeaver)
// Database: Nombre de la base de datos
// User Id: Usuario de la base de datos
// Password: Contraseña del usuario de la base de datos
var connString = "Server=pencadb;Port=5432;Database=pencadb;User Id=postgres;Password=postgres;";

// Crear un objeto NpgsqlDataSourceBuilder con la cadena de conexión y buildear la fuente de datos
var dataSourceBuilder = new NpgsqlDataSourceBuilder(connString);
var dataSource = dataSourceBuilder.Build();

// conn es la instancia de la conexión a la base de datos
var conn = await dataSource.OpenConnectionAsync();

// cmd es el comando que se ejecutará en la base de datos, conn es la conexión que tenemos abierta
await using (var cmd = new NpgsqlCommand("CREATE TABLE IF NOT EXISTS test (id serial PRIMARY KEY, name VARCHAR(50))", conn))
{
    // Ejecutar el comando
    await cmd.ExecuteNonQueryAsync();
}

// @p es un parámetro que le pusimos de nombre p
await using (var cmd = new NpgsqlCommand("INSERT INTO test (name) VALUES (@p)", conn))
{
    // Asignar el valor al parámetro p
    cmd.Parameters.AddWithValue("p", "Hello World");
    // Ejecutar el comando
    await cmd.ExecuteNonQueryAsync();
}

await using (var cmd = new NpgsqlCommand("SELECT name FROM test", conn))
{
    // reader es el lector de lo que devuelve la base de datos
    await using var reader = await cmd.ExecuteReaderAsync();
    // leemos los resultados de la base de datos
    while (await reader.ReadAsync())
    {
        // escribimos los resultados en la consola, van a llegar uno por uno los datos de la columna name
        Console.WriteLine(reader.GetString(0));
    }
}