using EurovisionTotalizer.Application.DTOs;
using EurovisionTotalizer.Domain.Persistence.Repositories;
using EurovisionTotalizer.Domain.Models;

namespace EurovisionTotalizer.Application.Services.Home;

public class HomeService : IHomeService
{
    private readonly IJsonStorageRepository<Participant> _participantRepo;
    private readonly IJsonStorageRepository<Country> _countryRepo;

    public HomeService(
        IJsonStorageRepository<Participant> participantRepo,
        IJsonStorageRepository<Country> countryRepo)
    {
        _participantRepo = participantRepo;
        _countryRepo = countryRepo;
    }

    public RepositoriesDto GetRepositories()
    {
        return new RepositoriesDto
        {
            Participants = _participantRepo.GetAll(),
            Countries = _countryRepo.GetAll()
        };
    }
}
