using EurovisionTotalizer.Domain.Models;
using EurovisionTotalizer.Domain.Persistence.Repositories;
using EurovisionTotalizer.Domain.Persistence.Serializera;
using Moq;

namespace EurovisionTotalizer.Tests.Domain.Persistence.Repositories;

public class JsonStorageRepositoryTests : IDisposable
{
    private readonly string _testFilePath;
    private readonly Mock<IJsonSerializer> _serializerMock;

    private class TestEntity : IHasName
    {
        public string Name { get; set; } = string.Empty;
        public int Value { get; set; }
    }

    public JsonStorageRepositoryTests()
    {
        _testFilePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.json");
        _serializerMock = new Mock<IJsonSerializer>();
    }

    public void Dispose()
    {
        if (File.Exists(_testFilePath))
            File.Delete(_testFilePath);
    }

    [Fact]
    public void Constructor_CreatesFileIfNotExists()
    {
        // Arrange & Act
        var repo = new JsonStorageRepository<TestEntity>(_testFilePath, _serializerMock.Object);

        // Assert
        Assert.True(File.Exists(_testFilePath));
    }

    [Fact]
    public void Constructor_NullFilePath_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            new JsonStorageRepository<TestEntity>(null!, _serializerMock.Object));
    }

    [Fact]
    public void Constructor_NullSerializer_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            new JsonStorageRepository<TestEntity>(_testFilePath, null!));
    }

    [Fact]
    public void Add_AddsItemSuccessfully()
    {
        // Arrange
        var repo = new JsonStorageRepository<TestEntity>(_testFilePath, _serializerMock.Object);

        var entity = new TestEntity { Name = "Test", Value = 1 };
        _serializerMock.Setup(s => s.Deserialize<List<TestEntity>>(It.IsAny<string>()))
            .Returns(new List<TestEntity>());
        _serializerMock.Setup(s => s.Serialize(It.IsAny<List<TestEntity>>()))
            .Returns("{}");

        // Act
        repo.Add(entity);

        // Assert
        _serializerMock.Verify(s => s.Serialize(It.Is<List<TestEntity>>(l => l.Any(e => e.Name == "Test"))), Times.Once);
    }

    [Fact]
    public void Add_DuplicateName_ThrowsInvalidOperationException()
    {
        // Arrange
        var repo = new JsonStorageRepository<TestEntity>(_testFilePath, _serializerMock.Object);
        var entity = new TestEntity { Name = "Test" };

        _serializerMock.Setup(s => s.Deserialize<List<TestEntity>>(It.IsAny<string>()))
            .Returns(new List<TestEntity> { entity });

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => repo.Add(entity));
    }

    [Fact]
    public void GetAll_ReturnsItems()
    {
        // Arrange
        var repo = new JsonStorageRepository<TestEntity>(_testFilePath, _serializerMock.Object);
        var entities = new List<TestEntity> { new TestEntity { Name = "A" }, new TestEntity { Name = "B" } };

        _serializerMock.Setup(s => s.Deserialize<List<TestEntity>>(It.IsAny<string>()))
            .Returns(entities);

        // Act
        var result = repo.GetAll();

        // Assert
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public void Update_ExistingItem_UpdatesSuccessfully()
    {
        // Arrange
        var repo = new JsonStorageRepository<TestEntity>(_testFilePath, _serializerMock.Object);
        var original = new TestEntity { Name = "Test", Value = 1 };
        var updated = new TestEntity { Name = "Test", Value = 2 };

        _serializerMock.Setup(s => s.Deserialize<List<TestEntity>>(It.IsAny<string>()))
            .Returns(new List<TestEntity> { original });
        _serializerMock.Setup(s => s.Serialize(It.IsAny<List<TestEntity>>()))
            .Returns("{}");

        // Act
        repo.Update(original, updated);

        // Assert
        _serializerMock.Verify(s => s.Serialize(It.Is<List<TestEntity>>(l => l.First().Value == 2)), Times.Once);
    }

    [Fact]
    public void Update_ItemNotFound_ThrowsKeyNotFoundException()
    {
        // Arrange
        var repo = new JsonStorageRepository<TestEntity>(_testFilePath, _serializerMock.Object);
        var original = new TestEntity { Name = "A" };
        var updated = new TestEntity { Name = "B" };

        _serializerMock.Setup(s => s.Deserialize<List<TestEntity>>(It.IsAny<string>()))
            .Returns(new List<TestEntity>());

        // Act & Assert
        Assert.Throws<KeyNotFoundException>(() => repo.Update(original, updated));
    }

    [Fact]
    public void Delete_ExistingItem_RemovesItem()
    {
        // Arrange
        var repo = new JsonStorageRepository<TestEntity>(_testFilePath, _serializerMock.Object);
        var entity = new TestEntity { Name = "Test" };

        _serializerMock.Setup(s => s.Deserialize<List<TestEntity>>(It.IsAny<string>()))
            .Returns(new List<TestEntity> { entity });
        _serializerMock.Setup(s => s.Serialize(It.IsAny<List<TestEntity>>()))
            .Returns("{}");

        // Act
        repo.Delete(entity);

        // Assert
        _serializerMock.Verify(s => s.Serialize(It.Is<List<TestEntity>>(l => !l.Any())), Times.Once);
    }

    [Fact]
    public void Delete_ItemNotFound_ThrowsKeyNotFoundException()
    {
        // Arrange
        var repo = new JsonStorageRepository<TestEntity>(_testFilePath, _serializerMock.Object);
        var entity = new TestEntity { Name = "NotExist" };

        _serializerMock.Setup(s => s.Deserialize<List<TestEntity>>(It.IsAny<string>()))
            .Returns(new List<TestEntity>());

        // Act & Assert
        Assert.Throws<KeyNotFoundException>(() => repo.Delete(entity));
    }

    [Fact]
    public void GetByName_ExistingItem_ReturnsItem()
    {
        // Arrange
        var repo = new JsonStorageRepository<TestEntity>(_testFilePath, _serializerMock.Object);
        var entity = new TestEntity { Name = "Test" };

        _serializerMock.Setup(s => s.Deserialize<List<TestEntity>>(It.IsAny<string>()))
            .Returns(new List<TestEntity> { entity });

        // Act
        var result = repo.GetByName("Test");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test", result?.Name);
    }

    [Fact]
    public void GetByName_ItemNotFound_ReturnsNull()
    {
        // Arrange
        var repo = new JsonStorageRepository<TestEntity>(_testFilePath, _serializerMock.Object);

        _serializerMock.Setup(s => s.Deserialize<List<TestEntity>>(It.IsAny<string>()))
            .Returns(new List<TestEntity>());

        // Act
        var result = repo.GetByName("Nope");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetByName_NullOrEmptyName_ThrowsArgumentException()
    {
        // Arrange
        var repo = new JsonStorageRepository<TestEntity>(_testFilePath, _serializerMock.Object);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => repo.GetByName(null!));
        Assert.Throws<ArgumentException>(() => repo.GetByName(""));
        Assert.Throws<ArgumentException>(() => repo.GetByName(" "));
    }
}
