using Moq;
using FluentAssertions;
using EurovisionTotalizer.Domain.Persistence.Repositories;
using EurovisionTotalizer.Domain.Persistence.Serializera;
using EurovisionTotalizer.Domain.Models;

namespace EurovisionTotalizer.Tests;

public class JsonStorageRepositoryTests
{
    private readonly Mock<IJsonSerializer> _serializerMock;
    private readonly string _filePath = "test.json";

    public JsonStorageRepositoryTests()
    {
        _serializerMock = new Mock<IJsonSerializer>();

        if (!File.Exists(_filePath))
            File.WriteAllText(_filePath, string.Empty);
    }

    private JsonStorageRepository<TestItem> CreateRepositoryWithData(List<TestItem> items)
    {
        _serializerMock.Setup(s => s.Deserialize<List<TestItem>>(It.IsAny<string>()))
            .Returns((string json) => items); ;

        _serializerMock.Setup(s => s.Serialize(It.IsAny<List<TestItem>>()))
            .Returns((List<TestItem> list) => "serialized-json");

        return new JsonStorageRepository<TestItem>(_filePath, _serializerMock.Object);
    }

    [Fact]
    public void Add_Should_Add_Item_When_Not_Exists()
    {
        var repo = CreateRepositoryWithData(new List<TestItem>());

        var newItem = new TestItem { Name = "Test1" };
        repo.Add(newItem);

        var all = repo.GetAll();
        all.Should().ContainSingle().Which.Name.Should().Be("Test1");
    }

    [Fact]
    public void Add_Should_Throw_When_Item_Already_Exists()
    {
        var repo = CreateRepositoryWithData(new List<TestItem> { new TestItem { Name = "Test1" } });

        var newItem = new TestItem { Name = "Test1" };

        Assert.Throws<InvalidOperationException>(() => repo.Add(newItem));
    }

    [Fact]
    public void GetByName_Should_Return_Item_When_Exists()
    {
        var repo = CreateRepositoryWithData(new List<TestItem> { new TestItem { Name = "Test1" } });

        var result = repo.GetByName("Test1");

        result.Should().NotBeNull();
        result!.Name.Should().Be("Test1");
    }

    [Fact]
    public void GetByName_Should_Return_Null_When_Not_Exists()
    {
        var repo = CreateRepositoryWithData(new List<TestItem>());

        var result = repo.GetByName("NoName");

        result.Should().BeNull();
    }

    [Fact]
    public void Update_Should_Update_Item_When_Exists()
    {
        var repo = CreateRepositoryWithData(new List<TestItem> { new TestItem { Name = "Test1" } });

        var updatedItem = new TestItem { Name = "Test1" }; // Name = "Test1" → tas pats objektas, tik kiti duomenys
        repo.Update(updatedItem);

        repo.GetAll().Should().ContainSingle().Which.Name.Should().Be("Test1");
    }

    [Fact]
    public void Update_Should_Throw_When_Item_Not_Found()
    {
        var repo = CreateRepositoryWithData(new List<TestItem>());

        var updatedItem = new TestItem { Name = "NoName" };

        Assert.Throws<KeyNotFoundException>(() => repo.Update(updatedItem));
    }


    [Fact]
    public void Delete_Should_Remove_Item_When_Exists()
    {
        var repo = CreateRepositoryWithData(new List<TestItem> { new TestItem { Name = "ToDelete" } });

        repo.Delete(new TestItem { Name = "ToDelete" });

        repo.GetAll().Should().BeEmpty();
    }

    [Fact]
    public void Delete_Should_Throw_When_Item_Not_Found()
    {
        var repo = CreateRepositoryWithData(new List<TestItem>());

        Assert.Throws<KeyNotFoundException>(() => repo.Delete(new TestItem { Name = "NoName" }));
    }
}

public class TestItem : IHasName
{
    public string Name { get; set; } = string.Empty;
}
