namespace EurovisionTotalizer.Domain.Interfaces.Persistence;

public interface IJsonStorageRepository<T> where T : class
{
    void Add(T item);
    IEnumerable<T> GetAll();
    void Update(Func<T, bool> predicate, T newItem);
    void Delete(Func<T, bool> predicate);
    public bool Exists(Func<T, bool> predicate);
}
