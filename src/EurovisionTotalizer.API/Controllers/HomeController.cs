using EurovisionTotalizer.Domain.Models;
using EurovisionTotalizer.Domain.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;
using EurovisionTotalizer.API.ViewModels;

public class HomeController : Controller
{
    private readonly IJsonStorageRepository<Participant> _participantRepo;
    private readonly IJsonStorageRepository<Country> _countryRepo;

    public HomeController(
        IJsonStorageRepository<Participant> participantRepo,
        IJsonStorageRepository<Country> countryRepo)
    {
        _participantRepo = participantRepo;
        _countryRepo = countryRepo;
    }

    public IActionResult DataEntry()
    {
        var participants = _participantRepo.GetAll();
        var countries = _countryRepo.GetAll();

        var model = new DataEntryViewModel
        {
            Participants = participants,
            Countries = countries
        };

        return View(model);
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Predictions()
    {
        return View();
    }

    public IActionResult Leaderboard()
    {
        return View();
    }
}

