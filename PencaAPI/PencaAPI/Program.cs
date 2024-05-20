using Npgsql;
using PencaAPI.DatabaseConnection;
using PencaAPI.Models;
using PencaAPI.Services;

// String de conexión a la base de datos
// Server: Nombre del contenedor de Docker de la base de datos
// Port: Puerto interno de la base de datos (5432, el puerto 8001 lo usamos solo para DBeaver)
// Database: Nombre de la base de datos
// User Id: Usuario de la base de datos
// Password: Contraseña del usuario de la base de datos
var connString = "Server=pencadb;Port=5432;Database=pencadb;User Id=postgres;Password=postgres;";

PgDatabaseConnection dbConnection = new PgDatabaseConnection(connString);

//await dbConnection.QueryAsync("INSERT INTO equipo (abreviatura, pais) VALUES ('uyu', 'Uruguay')");
//await dbConnection.QueryAsync("INSERT INTO equipo (abreviatura, pais) VALUES ('arg', 'Argentina')");

//await dbConnection.QueryAsync("INSERT INTO Alumno (nombre, apellido, cedula, fecha_nacimiento, anio_ingreso, semestre_ingreso, puntaje_total, campeon, subcampeon) VALUES ('nombre', 'apellido', 123456789, '2021-01-01', 2021, 1, 0, 'uyu', 'arg')");
//await dbConnection.QueryAsync("INSERT INTO Alumno (nombre, apellido, cedula, fecha_nacimiento, anio_ingreso, semestre_ingreso, puntaje_total, campeon, subcampeon) VALUES ('nombre', 'apellido', 123456798, '2021-01-01', 2021, 1, 0, 'uyu', 'arg')");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

AlumnoService alumnoService = new(dbConnection);

app.MapGet("/alumnos", async () =>
    {
        Alumno[] alumnos = await alumnoService.GetAllAsync();
        return alumnos;
    })
    .WithName("Get Alumnos")
    .WithOpenApi();

app.Run();