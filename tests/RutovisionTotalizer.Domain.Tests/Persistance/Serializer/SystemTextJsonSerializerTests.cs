using System.Text.Json;
using EurovisionTotalizer.Domain.Persistence.Serializera;

namespace EurovisionTotalizer.Tests.Domain.Persistence.Serializera;

public class SystemTextJsonSerializerTests
{
    private readonly SystemTextJsonSerializer _serializer;

    public SystemTextJsonSerializerTests()
    {
        _serializer = new SystemTextJsonSerializer();
    }

    [Fact]
    public void Constructor_WithNullOptions_SetsDefaultOptions()
    {
        // Arrange & Act
        var serializer = new SystemTextJsonSerializer(null);

        // Assert
        Assert.NotNull(serializer);
    }

    [Fact]
    public void Serialize_Object_ReturnsJsonString()
    {
        // Arrange
        var obj = new { Name = "Test", Age = 25 };

        // Act
        var json = _serializer.Serialize(obj);

        // Assert
        Assert.NotNull(json);
        Assert.Contains("Test", json);
        Assert.Contains("25", json);
    }

    [Fact]
    public void Serialize_NullObject_ThrowsArgumentNullException()
    {
        // Arrange
        object obj = null!;

        // Act & Assert
        var ex = Assert.Throws<ArgumentNullException>(() => _serializer.Serialize(obj));
        Assert.Equal("Object to serialize cannot be null (Parameter 'obj')", ex.Message);
    }

    [Fact]
    public void Deserialize_ValidJson_ReturnsObject()
    {
        // Arrange
        var obj = new { Name = "Test", Age = 25 };
        var json = _serializer.Serialize(obj);

        // Act
        var result = _serializer.Deserialize<JsonElement>(json);

        // Assert
        Assert.Equal("Test", result.GetProperty("Name").GetString());
        Assert.Equal(25, result.GetProperty("Age").GetInt32());
    }

    [Fact]
    public void Deserialize_NullOrEmptyJson_ThrowsArgumentException()
    {
        // Arrange
        string json = "";

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => _serializer.Deserialize<object>(json));
        Assert.Contains("JSON string cannot be null or empty", ex.Message);
    }

    [Fact]
    public void Deserialize_InvalidJson_ThrowsJsonException()
    {
        // Arrange
        var invalidJson = "{ invalid json }";

        // Act & Assert
        Assert.Throws<JsonException>(() => _serializer.Deserialize<object>(invalidJson));
    }
}
