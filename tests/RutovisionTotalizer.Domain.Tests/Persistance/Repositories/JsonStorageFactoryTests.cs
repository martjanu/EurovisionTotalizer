using EurovisionTotalizer.Domain.Models;
using EurovisionTotalizer.Domain.Persistence.Repositories;
using EurovisionTotalizer.Domain.Persistence.Serializera;
using Moq;

namespace EurovisionTotalizer.Tests.Domain.Persistence.Repositories;

public class JsonStorageFactoryTests
{
    private class TestEntity : IHasName
    {
        public string Name { get; set; } = string.Empty;
    }

    [Fact]
    public void Create_ReturnsJsonStorageRepositoryInstance()
    {
        // Arrange
        var factory = new JsonStorageFactory();
        var serializerMock = new Mock<IJsonSerializer>();
        string filePath = "test.json";

        // Act
        var repository = factory.Create<TestEntity>(filePath, serializerMock.Object);

        // Assert
        Assert.NotNull(repository);
        Assert.IsAssignableFrom<IJsonStorageRepository<TestEntity>>(repository);
    }

    [Fact]
    public void Create_WithValidParameters_SetsCorrectType()
    {
        // Arrange
        var factory = new JsonStorageFactory();
        var serializerMock = new Mock<IJsonSerializer>();
        string filePath = "test.json";

        // Act
        var repository = factory.Create<TestEntity>(filePath, serializerMock.Object);

        // Assert
        Assert.Equal(typeof(JsonStorageRepository<TestEntity>), repository.GetType());
    }

    [Fact]
    public void Create_WithNullSerializer_ThrowsArgumentNullException()
    {
        // Arrange
        var factory = new JsonStorageFactory();
        string filePath = "test.json";

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => factory.Create<TestEntity>(filePath, null!));
    }
}
