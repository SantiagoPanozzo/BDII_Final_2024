namespace PencaAPI.DatabaseConnection;

public interface IDatabaseConnection
{
    public Task<string[]> QueryAsync(string queryString);
}