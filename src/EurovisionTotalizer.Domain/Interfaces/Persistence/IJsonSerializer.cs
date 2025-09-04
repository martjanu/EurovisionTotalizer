namespace EurovisionTotalizer.Domain.Interfaces.Persistence;

public interface IJsonSerializer
{
    string Serialize<T>(T obj);
    T? Deserialize<T>(string json);
}
