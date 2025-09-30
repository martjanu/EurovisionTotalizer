using EurovisionTotalizer.Domain.Persistence.Serializera;

namespace EurovisionTotalizer.Domain.Persistence.Serializers
{
    public interface IJsonSerializerFactory
    {
        IJsonSerializer Create();
    }
}