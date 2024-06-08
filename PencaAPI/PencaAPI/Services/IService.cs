using PencaAPI.Models;

namespace PencaAPI.Services;

/// <summary>
/// Interfaz que representa un servicio genérico.
/// </summary>
/// <typeparam name="T">Tipo de la entidad que maneja el servicio.</typeparam>
public interface IService<T>
{
    /// <summary>
    /// Obtener todas las instancias de objetos de tipo T.
    /// </summary>
    /// <returns>Todas las instancias de objetos de tipo T.</returns>
    public Task<T[]> GetAllAsync();

    /// <summary>
    /// Obtener un objeto de tipo T utilizando su id de tipo TId correspondiente.
    /// </summary>
    /// <param name="id">Id correspondiente al objeto a obtener.</param>
    /// <returns></returns>
    public Task<T> GetByIdAsync(object id);

    /// <summary>
    /// Persistir un nuevo objeto de tipo T.
    /// </summary>
    /// <param name="entity">Objeto de tipo T.</param>
    /// <returns>El objeto de tipo T persistido.</returns>
    public Task<T> CreateAsync(T entity);
    
    /// <summary>
    /// Modificar un objeto de tipo T según su id de tipo TId.
    /// </summary>
    /// <param name="id">Id del objeto de tipo T.</param>
    /// <param name="entity">Objeto de tipo T modificado.</param>
    /// <returns></returns>
    public Task<T> UpdateAsync(object id, T entity);
    
    /// <summary>
    /// Eliminar un objeto según su id.
    /// </summary>
    /// <param name="id">Id del objeto a eliminar.</param>
    /// <returns></returns>
    public Task<T> DeleteAsync(object id);
}