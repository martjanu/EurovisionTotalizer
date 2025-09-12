using EurovisionTotalizer.Domain.Models;
using EurovisionTotalizer.Domain.Persistence.Repositories;

namespace EurovisionTotalizer.Domain.Validators;

public class NameValidator<T> : INameValidator where T : class, IHasName
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
