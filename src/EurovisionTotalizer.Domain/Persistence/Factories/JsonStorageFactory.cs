using EurovisionTotalizer.Domain.Models;
using EurovisionTotalizer.Domain.Persistence.Repositories;
using EurovisionTotalizer.Domain.Persistence.Serializera;

namespace EurovisionTotalizer.Domain.Persistence.Factories;

public class JsonStorageFactory
{
    public IJsonStorageRepository<T> Create<T>(string filePath, IJsonSerializer serializer) where T : IHasName
        => new JsonStorageRepository<T>(filePath, serializer);
}
