using Quartz;
using Npgsql;
using PencaAPI.DatabaseConnection;
using System.Threading.Tasks;

public class GenerateNotificationsJob : IJob
{
    private readonly PgDatabaseConnection _dbConnection;

    public GenerateNotificationsJob(PgDatabaseConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            var result = await _dbConnection.QueryAsync("SELECT generar_notificaciones();");
           
        }
        catch(PostgresException e)
        {
            throw new ArgumentException("Ocurrió un error al acceder a la base de datos.", e);
        }
    }
}
