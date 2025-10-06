using EurovisionTotalizer.Domain.Models;
using EurovisionTotalizer.Domain.Persistence.Serializera;

namespace EurovisionTotalizer.Domain.Persistence.Repositories
{
    public interface IJsonStorageFactory
    {
        IJsonStorageRepository<T> Create<T>(string filePath, IJsonSerializer serializer) where T : IHasName;
    }
}