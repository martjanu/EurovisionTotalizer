using EurovisionTotalizer.Domain.Persistence.Repositories;

namespace EurovisionTotalizer.Domain.Services;

public class DataCrudServices
{
    private readonly IJsonStorageRepository _jsonStorageRepository;

    public DataCrudServices(IJsonStorageRepository jsonStorageRepository)
    {
        _jsonStorageRepository = jsonStorageRepository;
    }

    public void AddData<T>(T item) where T : class
    {
        _jsonStorageRepository.Add(item);
    }

    public IEnumerable<T> GetAllData<T>() where T : class
    {
        return _jsonStorageRepository.GetAll<T>();
    }

    public void UpdateData<T>(Func<T, bool> predicate, T newItem) where T : class
    {
        _jsonStorageRepository.Update(predicate, newItem);
    }

    public void DeleteData<T>(Func<T, bool> predicate) where T : class
    {
        _jsonStorageRepository.Delete(predicate);
    }
}
