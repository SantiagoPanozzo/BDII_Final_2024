using PencaAPI.Models;

namespace PencaAPI.Services;

public interface IService<T, TId> where T: IEntity<TId>
{
    public Task<T> GetByIdAsync(TId id);
    public Task<T[]> GetAllAsync();
    public Task<T> CreateAsync(T entity);
    public Task<T> UpdateAsync(TId id, T entity);
    public Task<T> DeleteAsync(int id);
}