using EurovisionTotalizer.Domain.Models;
using EurovisionTotalizer.Domain.Persistence.Serializera;

namespace EurovisionTotalizer.Domain.Persistence.Repositories;

public class JsonStorageRepository<IHasName> : IJsonStorageRepository<IHasName>
{
    private readonly string _filePath;
    private readonly IJsonSerializer _serializer;

    public JsonStorageRepository(string filePath, IJsonSerializer serializer)
    {
        _filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
        _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));

        if (!File.Exists(_filePath))
        {
            SaveData(new List<IHasName>());
        }
    }

    private List<IHasName> LoadData()
    {
        var json = File.ReadAllText(_filePath);
        return _serializer.Deserialize<List<IHasName>>(json) ?? new List<IHasName>();
    }

    private void SaveData(List<IHasName> items)
    {
        var json = _serializer.Serialize(items);
        File.WriteAllText(_filePath, json);
    }

    public void Add(IHasName item)
    {
        var items = LoadData();
        items.Add(item);
        SaveData(items);
    }

    public IEnumerable<IHasName> GetAll()
    {
        return LoadData();
    }

    public void Update(IHasName item, IHasName newItem)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));
        if (newItem == null)
            throw new ArgumentNullException(nameof(newItem));

        var items = LoadData();
        var index = items.FindIndex(x => (x as dynamic).Name == (item as dynamic).Name);
        if (index >= 0)
        {
            items[index] = newItem;
            SaveData(items);
        }
    }

    public void Delete(IHasName item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));

        var items = LoadData();
        items.RemoveAll(x =>
            (x as dynamic).Name == (item as dynamic).Name
        );
        SaveData(items);
    }

    public bool Exists(Func<IHasName, bool> predicate)
    {
        var items = LoadData();
        return items.Any(predicate);
    }

    public IHasName? GetByName(string name)
    {
        var items = LoadData();
        return items.FirstOrDefault(x => (x as dynamic).Nme == name);
    }
}
