using EurovisionTotalizer.Domain.Persistence.Repositories;
using EurovisionTotalizer.Domain.Persistence.Serializations;

namespace EurovisionTotalizer.Domain.Persistence.Factories;

public class EurovisionTOtalizerJsonStorageFactory
{
    public IJsonStorageRepository<T> Create<T>(string filePath, IJsonSerializer serializer) where T : class
        => new JsonStorageRepository<T>(filePath, serializer);
}
