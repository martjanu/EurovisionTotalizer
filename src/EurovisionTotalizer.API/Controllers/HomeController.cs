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
            Tables = dto.Tables.Select(t => new TableViewModel
            {
                Title = t.Title,
                ParticipantNames = t.ParticipantNames, // <- BŪTINAI reikia perduoti
                Rows = t.Rows.Select(r => new TableRowViewModel
                {
                    CountryName = r.CountryName,
                    Predictions = r.Predictions
                }).ToList()
            }).ToList()
        };

        return View(model);
    }
}
