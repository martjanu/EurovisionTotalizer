using EurovisionTotalizer.Domain.Persistence.Repositories;

namespace EurovisionTotalizer.Domain.Services;

public class DataCrudService<T> : IDataCrudService<T> where T : class
{
    private readonly IJsonStorageRepository<T> _jsonStorageRepository;

    public DataCrudService(IJsonStorageRepository<T> jsonStorageRepository)
    {
        _jsonStorageRepository = jsonStorageRepository;
    }

    public void AddData(T item)
    {
        _jsonStorageRepository.Add(item);
    }

    public IEnumerable<T> GetAllData()
    {
        return _jsonStorageRepository.GetAll();
    }

    public void UpdateData(Func<T, bool> predicate, T newItem)
    {
        _jsonStorageRepository.Update(predicate, newItem);
    }

    public void DeleteData(Func<T, bool> predicate)
    {
        _jsonStorageRepository.Delete(predicate);
    }
}
