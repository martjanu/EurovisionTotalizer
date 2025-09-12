using EurovisionTotalizer.Domain.Persistence.Interfaces;

namespace EurovisionTotalizer.Domain.Persistence.Repositories;

public class JsonStorageRepository<T> : IJsonStorageRepository<T> where T : class
{
    private readonly string _filePath;
    private readonly IJsonSerializer _serializer;

    public JsonStorageRepository(string filePath, IJsonSerializer serializer)
    {
        _filePath = filePath;
        _serializer = serializer;

        if (!File.Exists(_filePath))
        {
            SaveData(new List<T>());  
        }
    }

    private List<T> LoadData()
    {
        var json = File.ReadAllText(_filePath);
        return _serializer.Deserialize<List<T>>(json) ?? new List<T>();
    }

    private void SaveData(List<T> items)
    {
        var json = _serializer.Serialize(items);
        File.WriteAllText(_filePath, json);
    }

    public void Add(T item)
    {
        var items = LoadData();
        items.Add(item);
        SaveData(items);
    }

    public IEnumerable<T> GetAll()
    {
        return LoadData();
    }

    public void Update(Func<T, bool> predicate, T newItem)
    {
        var items = LoadData();
        var index = items.FindIndex(x => predicate(x));
        if (index >= 0)
        {
            items[index] = newItem;
            SaveData(items);
        }
    }

    public void Delete(Func<T, bool> predicate)
    {
        var items = LoadData();
        items.RemoveAll(x => predicate(x));
        SaveData(items);
    }

    public bool Exists(Func<T, bool> predicate)
    {
        var items = LoadData();
        return items.Any(predicate);
    }
}
