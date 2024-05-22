using PencaAPI.DatabaseConnection;
using PencaAPI.Services;

namespace PencaAPI;

public static class Program
{
    public static void Main(string[] args)
    {
        // String de conexión a la base de datos
        // Server: Nombre del contenedor de Docker de la base de datos
        // Port: Puerto interno de la base de datos (5432, el puerto 8001 lo usamos solo para DBeaver)
        // Database: Nombre de la base de datos
        // User Id: Usuario de la base de datos
        // Password: Contraseña del usuario de la base de datos
        var connString = "Server=pencadb;Port=5432;Database=pencadb;User Id=postgres;Password=postgres;";

        // Conexión con la base de datos
        PgDatabaseConnection dbConnection = new PgDatabaseConnection(connString);
        
        // Creo el builder de la api web
        var builder = WebApplication.CreateBuilder(args);
        
        // Los builder.Services son una forma de tener singletons de todos los services y cosas útiles
        // En este caso agrego la dbConnection a los services para que siempre que se necesite una dbConnection use
        // esa misma instancia. Lo mismo pasa con los services para los controllers y etc.
        builder.Services.AddScoped<PgDatabaseConnection>(__ => dbConnection); // Agrego la dbConnection a los services
        builder.Services.AddScoped<AlumnoService>(); // Registro los services, la dbConnection se inyecta automáticamente
        builder.Services.AddControllers(); // Registro los controllers, los services se inyectan automáticamente
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();
        Configure(app, app.Environment);
        
        app.Run();

    }
    
    public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // swagger
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // cosas de .NET, de la documentación 
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();

        // Habilitar los endpoints y los controllers para cada uno
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers(); // Mappear controllers
            endpoints.MapGet("/", async context => { await context.Response.WriteAsync("Hello World!"); });
        });
    }
}