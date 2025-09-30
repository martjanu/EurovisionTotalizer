using EurovisionTotalizer.Domain.Models;

namespace EurovisionTotalizer.Domain.Persistence.Repositories;

public interface IJsonStorageRepository<T> where T : IHasName
{
    void Add(T item);
    void Delete(T item);
    IEnumerable<T> GetAll();
    T? GetByName(string name);
    void Update(T item, T newItem);
}