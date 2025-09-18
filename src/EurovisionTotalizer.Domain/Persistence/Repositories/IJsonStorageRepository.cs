using EurovisionTotalizer.Domain.Models;

namespace EurovisionTotalizer.Domain.Persistence.Repositories;

public interface IJsonStorageRepository<T> where T : class
{
    void Add(T item);
    IEnumerable<T> GetAll();
    void Update(IHasName item, T newItem);
    void Delete(IHasName item);
    public bool Exists(Func<T, bool> predicate);
}
