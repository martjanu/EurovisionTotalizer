using EurovisionTotalizer.Domain.Persistence.Interfaces;
using EurovisionTotalizer.Domain.Persistence.Repositories;

namespace EurovisionTotalizer.Domain.Persistence.Factories;

public class EurovisionTOtalizerJsonStorageFactory
{
    public IJsonStorageRepository<T> Create<T>(string filePath, IJsonSerializer serializer) where T : class
        => new JsonStorageRepository<T>(filePath, serializer);
}
