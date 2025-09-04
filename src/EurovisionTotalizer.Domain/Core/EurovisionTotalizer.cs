using EurovisionTotalizer.Domain.Core.Factories;
using EurovisionTotalizer.Domain.Core.Factories.Persistence;
using EurovisionTotalizer.Domain.Models;
using EurovisionTotalizer.Domain.Models.Enums;

namespace EurovisionTotalizer.Domain.Core;

public class EurovisionTotalizer
{
    public void ProcessPredictions()
    {
        var serializerFactory = new EurovisionTotalizerSerializationFactory();
        var serializer = serializerFactory.Create();

        var storageFactory = new EurovisionTOtalizerJsonStorageFactory();
        var countryRepository = storageFactory.Create<Country>("country.json", serializer);

    }
}
