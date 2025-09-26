using EurovisionTotalizer.API.ViewModels;
using EurovisionTotalizer.Domain.Models;
using EurovisionTotalizer.Domain.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    private readonly IJsonStorageRepository<Participant> _participantRepo;
    private readonly IJsonStorageRepository<Country> _countryRepo;

    public HomeController(
         IJsonStorageRepository<Participant> participantRepo,
         IJsonStorageRepository<Country> countryRepo)
    {
        _participantRepo = participantRepo ?? throw new ArgumentNullException(nameof(participantRepo));
        _countryRepo = countryRepo ?? throw new ArgumentNullException(nameof(countryRepo));
    }

    public IActionResult Index()
    {
        var model = new RepositoriesViewModel
        {
            Participants = _participantRepo.GetAll(),
            Countries = _countryRepo.GetAll()
        };
        return View(model);
    }

    public IActionResult Leaderboard()
    {
        return View();
    }
}

