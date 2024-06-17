using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using PencaAPI.DatabaseConnection;
using PencaAPI.Services;

namespace PencaAPI;

public static class Program
{
    public static void Main(string[] args)
    {
        var connString = "Server=pencadb;Port=5432;Database=pencadb;User Id=postgres;Password=postgres;";

        PgDatabaseConnection dbConnection = new PgDatabaseConnection(connString);
        
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
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddTransient<IAuthService, AuthService>();

        var app = builder.Build();
        Configure(app, app.Environment);
        
        app.Run();
    }
    
    public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        // Configurar autenticación y autorización
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/", async context => { await context.Response.WriteAsync("Hello World!"); });
        });
    }
}
