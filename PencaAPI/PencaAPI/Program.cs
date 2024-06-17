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
        
        // Configuración de servicios
        var key = Encoding.ASCII.GetBytes("your_secret_key");

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

        

        builder.Services.AddScoped<PgDatabaseConnection>(__ => dbConnection);
        builder.Services.AddScoped<AlumnoService>();
        builder.Services.AddSingleton<IUsuarioService, UsuarioService>();
        builder.Services.AddScoped<EtapaService>();
        builder.Services.AddScoped<CarreraService>();
        builder.Services.AddScoped<EquipoService>();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddTransient<IAuthService, AuthService>();

        // Buildear la app
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

        // Habilitar los endpoints y los controllers para cada uno
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/", async context => { await context.Response.WriteAsync("Hello World!"); });
        });
    }
}