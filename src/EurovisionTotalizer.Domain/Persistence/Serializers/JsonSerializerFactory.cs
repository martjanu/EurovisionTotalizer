using EurovisionTotalizer.Domain.Persistence.Serializera;

namespace EurovisionTotalizer.Domain.Persistence.Serializers;

public class JsonSerializerFactory : IJsonSerializerFactory
{
    public IJsonSerializer Create() => new SystemTextJsonSerializer();
}
