using EurovisionTotalizer.Domain.Services;
    using EurovisionTotalizer.Domain.Persistence.Repositories;

namespace EurovisionTotalizer.Domain.Factories;

public class DataCrudServiceFactory<T> where T : class
{
    public IDataCrudService<T> Create(IJsonStorageRepository<T> repository)
        => new DataCrudService<T>(repository);
}
