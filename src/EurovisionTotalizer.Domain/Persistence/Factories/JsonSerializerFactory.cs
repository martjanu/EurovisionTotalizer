using EurovisionTotalizer.Domain.Persistence.Serializera;

namespace EurovisionTotalizer.Domain.Persistence.Factories;

public class JsonSerializerFactory
{
    public IJsonSerializer Create() => new SystemTextJsonSerializer();
}
