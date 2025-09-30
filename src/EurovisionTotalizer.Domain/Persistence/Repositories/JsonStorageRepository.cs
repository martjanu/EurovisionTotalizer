using EurovisionTotalizer.Domain.Models;
using EurovisionTotalizer.Domain.Persistence.Serializera;

namespace EurovisionTotalizer.Domain.Persistence.Repositories
{
    public class JsonStorageRepository<T> : IJsonStorageRepository<T> where T : IHasName
    {
        private readonly string _filePath;
        private readonly IJsonSerializer _serializer;
        private readonly object _lockObj = new();

        public JsonStorageRepository(string filePath, IJsonSerializer serializer)
        {
            _filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));

            if (!File.Exists(_filePath))
            {
                SaveData(new List<T>());
            }
        }

        private List<T> LoadData()
        {
            lock (_lockObj)
            {
                try
                {
                    var json = File.ReadAllText(_filePath);
                    if (string.IsNullOrWhiteSpace(json))
                        return new List<T>();

                    return _serializer.Deserialize<List<T>>(json) ?? new List<T>();
                }
                catch (Exception ex)
                {
                    throw new IOException($"Error reading from {_filePath}", ex);
                }
            }
        }

        private void SaveData(List<T> items)
        {
            lock (_lockObj)
            {
                try
                {
                    var json = _serializer.Serialize(items);
                    File.WriteAllText(_filePath, json);
                }
                catch (Exception ex)
                {
                    throw new IOException($"Error writing to {_filePath}", ex);
                }
            }
        }

        public void Add(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var items = LoadData();

            if (items.Any(x => x.Name == item.Name))
                throw new InvalidOperationException($"An item with the name '{item.Name}' already exists.");

            items.Add(item);
            SaveData(items);
        }

        public IEnumerable<T> GetAll()
        {
            return LoadData();
        }

        public void Update(T updatedItem)
        {
            if (updatedItem == null)
                throw new ArgumentNullException(nameof(updatedItem));

            var items = LoadData();
            var index = items.FindIndex(x => x.Name == updatedItem.Name);

            if (index >= 0)
            {
                items[index] = updatedItem;
                SaveData(items);
            }
            else
            {
                throw new KeyNotFoundException($"Item with name '{updatedItem.Name}' not found.");
            }
        }


        public void Delete(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            var items = LoadData();
            var removed = items.RemoveAll(x => x.Name == item.Name);

            if (removed == 0)
                throw new KeyNotFoundException($"Item with name '{item.Name}' not found.");

            SaveData(items);
        }

        public T? GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be null or empty.", nameof(name));

            return LoadData().FirstOrDefault(x => x.Name == name);
        }
    }
}
