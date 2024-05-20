using Npgsql;
using PencaAPI.DatabaseConnection;

// String de conexión a la base de datos
// Server: Nombre del contenedor de Docker de la base de datos
// Port: Puerto interno de la base de datos (5432, el puerto 8001 lo usamos solo para DBeaver)
// Database: Nombre de la base de datos
// User Id: Usuario de la base de datos
// Password: Contraseña del usuario de la base de datos
var connString = "Server=pencadb;Port=5432;Database=pencadb;User Id=postgres;Password=postgres;";

PgDatabaseConnection dbConnection = new PgDatabaseConnection(connString);

await dbConnection.QueryAsync("CREATE TABLE IF NOT EXISTS test (id serial PRIMARY KEY, name VARCHAR(50))");

// @p es un parámetro que le pusimos de nombre p
await dbConnection.QueryAsync(
    "INSERT INTO test (name) VALUES (@p)",
    new Dictionary<string, object>() {
        {"p", "Hello World"}
    });

foreach (var result in await dbConnection.QueryAsync("SELECT name from test"))
{
    Console.WriteLine(result);
}