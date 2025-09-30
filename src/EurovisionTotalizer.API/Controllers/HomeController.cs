using EurovisionTotalizer.Application.Services.Home;
using EurovisionTotalizer.API.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EurovisionTotalizer.API.Controllers;

public class HomeController : Controller
{
    private readonly IHomeService _homeService;

    public HomeController(IHomeService homeService)
    {
        _homeService = homeService;
    }

    public IActionResult Index()
    {
        var dto = _homeService.GetRepositories();

        var model = new RepositoriesViewModel
        {
            Participants = dto.Participants,
            Countries = dto.Countries
        };

        return View(model);
    }

    public IActionResult Leaderboard()
    {
        return View();
    }
}
