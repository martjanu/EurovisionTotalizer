
namespace EurovisionTotalizer.Domain.Services
{
    public interface IDataCrudService<T> where T : class
    {
        void AddData(T item);
        void DeleteData(Func<T, bool> predicate);
        IEnumerable<T> GetAllData();
        void UpdateData(Func<T, bool> predicate, T newItem);
    }
}