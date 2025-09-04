using EurovisionTotalizer.Domain.Interfaces.Persistence;
using EurovisionTotalizer.Domain.Persistence.Serializations;

namespace EurovisionTotalizer.Domain.Core.Factories.Persistence;

public class EurovisionTotalizerSerializationFactory
{
    public IJsonSerializer Create() => new SystemTextJsonSerializer();
}
