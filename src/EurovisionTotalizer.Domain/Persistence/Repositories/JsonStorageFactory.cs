using EurovisionTotalizer.Domain.Models;
using EurovisionTotalizer.Domain.Persistence.Serializera;

namespace EurovisionTotalizer.Domain.Persistence.Repositories;

public class JsonStorageFactory : IJsonStorageFactory
{
    public IJsonStorageRepository<T> Create<T>(string filePath, IJsonSerializer serializer) where T : IHasName
        => new JsonStorageRepository<T>(filePath, serializer);
}
