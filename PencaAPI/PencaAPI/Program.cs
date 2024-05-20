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

await dbConnection.QueryAsync("INSERT INTO equipo (abreviatura, pais) VALUES ('uyu', 'Uruguay')");
await dbConnection.QueryAsync("INSERT INTO equipo (abreviatura, pais) VALUES ('arg', 'Argentina')");

await dbConnection.QueryAsync("INSERT INTO Alumno (nombre, apellido, cedula, fecha_nacimiento, anio_ingreso, semestre_ingreso, puntaje_total, campeon, subcampeon) VALUES ('nombre', 'apellido', 123456789, '2021-01-01', 2021, 1, 0, 'uyu', 'arg')");
await dbConnection.QueryAsync("INSERT INTO Alumno (nombre, apellido, cedula, fecha_nacimiento, anio_ingreso, semestre_ingreso, puntaje_total, campeon, subcampeon) VALUES ('nombre', 'apellido', 123456798, '2021-01-01', 2021, 1, 0, 'uyu', 'arg')");

var result = await dbConnection.QueryAsync("SELECT * FROM alumno");

foreach (var row in result)
{
    foreach (var column in row)
    {
        Console.WriteLine($"Column: {column.Key}, Value: {column.Value}");
    }
    Console.WriteLine();
}