using PencaAPI.Models;

namespace PencaAPI.Services;

public interface IService<T, TId> where T: IEntity<TId>
{
    public async Task<T> GetByIdAsync(TId id) {
        throw new NotImplementedException();
    }

    public Task<T[]> GetAllAsync() {
        throw new NotImplementedException();
    }
    public Task<T> CreateAsync(T entity) {
        throw new NotImplementedException();
    }
    public Task<T> UpdateAsync(TId id, T entity) {
        throw new NotImplementedException();
    }
    public Task<T> DeleteAsync(int id) {
        throw new NotImplementedException();
    }
}