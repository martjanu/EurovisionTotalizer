using EurovisionTotalizer.Domain.Interfaces;
using EurovisionTotalizer.Domain.Interfaces.Persistence;

namespace EurovisionTotalizer.Domain.Core.Validators;

public class NameValidator<T> where T : class, IHasName
{
    private readonly IJsonStorageRepository<T> _repository;

    public NameValidator(IJsonStorageRepository<T> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public bool IsValid(string name)
    {
        return !string.IsNullOrWhiteSpace(name)
            && name.Length <= 15
            && _repository.Exists(x => x.Name == name);
    }
}
