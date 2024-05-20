namespace PencaAPI.Models;

/// <summary>
/// Clase que representa una entidad de la base de datos.
/// </summary>
/// <typeparam name="TId">Tipo de la primary key de la entidad.</typeparam>
public interface IEntity<TId>
{
    TId Id { get; set; }
}