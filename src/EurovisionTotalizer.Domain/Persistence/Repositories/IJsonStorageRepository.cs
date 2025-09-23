
namespace EurovisionTotalizer.Domain.Persistence.Repositories
{
    public interface IJsonStorageRepository<IHasName>
    {
        void Add(IHasName item);
        void Delete(IHasName item);
        bool Exists(Func<IHasName, bool> predicate);
        IEnumerable<IHasName> GetAll();
        IHasName? GetByName(string name);
        void Update(IHasName item, IHasName newItem);
    }
}