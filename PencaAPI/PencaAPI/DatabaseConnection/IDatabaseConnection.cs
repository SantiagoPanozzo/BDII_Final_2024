namespace PencaAPI.DatabaseConnection;

public interface IDatabaseConnection
{
    Task<List<Dictionary<string, object>>> QueryAsync(string queryString);
    Task<List<Dictionary<string, object>>> QueryAsync(string queryString, Dictionary<string, object> parameters);
}