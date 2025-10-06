using System.Text.Json;

namespace EurovisionTotalizer.Domain.Persistence.Serializera
{
    public class SystemTextJsonSerializer : IJsonSerializer
    {
        private readonly JsonSerializerOptions _options;

        public SystemTextJsonSerializer(JsonSerializerOptions? options = null)
        {
            _options = options ?? new JsonSerializerOptions { WriteIndented = true };
        }

        public string Serialize<T>(T obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj), "Object to serialize cannot be null");
            return JsonSerializer.Serialize(obj, _options);
        }

        public T? Deserialize<T>(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                throw new ArgumentException("JSON string cannot be null or empty", nameof(json));

            return JsonSerializer.Deserialize<T>(json, _options);
        }
    }
}
