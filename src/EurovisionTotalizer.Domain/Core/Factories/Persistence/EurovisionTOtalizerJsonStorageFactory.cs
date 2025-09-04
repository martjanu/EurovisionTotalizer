using EurovisionTotalizer.Domain.Interfaces.Persistence;
using EurovisionTotalizer.Domain.Persistence.Repositories;

namespace EurovisionTotalizer.Domain.Core.Factories.Persistence;

public class EurovisionTOtalizerJsonStorageFactory
{
    public IJsonStorageRepository<T> Create<T>(string filePath, IJsonSerializer serializer) where T : class
        => new JsonStorageRepository<T>(filePath, serializer);
}
