using EurovisionTotalizer.Domain.Persistence.Serializera;
using EurovisionTotalizer.Domain.Persistence.Serializers;

namespace EurovisionTotalizer.Tests.Domain.Persistence.Serializers;

public class JsonSerializerFactoryTests
{
    [Fact]
    public void Create_ReturnsSystemTextJsonSerializerInstance()
    {
        // Arrange
        var factory = new JsonSerializerFactory();

        // Act
        var serializer = factory.Create();

        // Assert
        Assert.NotNull(serializer);
        Assert.IsType<SystemTextJsonSerializer>(serializer);
    }
}
