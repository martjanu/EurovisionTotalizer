using EurovisionTotalizer.Domain.Persistence.Serializations;

namespace EurovisionTotalizer.Domain.Persistence.Factories;

public class EurovisionTotalizerSerializationFactory
{
    public IJsonSerializer Create() => new SystemTextJsonSerializer();
}
