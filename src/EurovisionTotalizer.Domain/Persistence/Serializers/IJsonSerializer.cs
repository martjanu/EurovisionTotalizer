namespace EurovisionTotalizer.Domain.Persistence.Serializera;

public interface IJsonSerializer
{
    string Serialize<T>(T obj);
    T? Deserialize<T>(string json);
}
