using EurovisionTotalizer.Domain.Models;
using EurovisionTotalizer.Domain.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;
using EurovisionTotalizer.API.ViewModels;
using EurovisionTotalizer.Domain.Enums;

public class DataEntryController : Controller
{
    private readonly IJsonStorageRepository<Participant> _participantRepo;
    private readonly IJsonStorageRepository<Country> _countryRepo;

    public DataEntryController(
        IJsonStorageRepository<Participant> participantRepo,
        IJsonStorageRepository<Country> countryRepo)
    {
        _participantRepo = participantRepo;
        _countryRepo = countryRepo;
    }

    // GET: /DataEntry
    public IActionResult DataEntry()
    {
        var model = new DataEntryViewModel
        {
            Participants = _participantRepo.GetAll(),
            Countries = _countryRepo.GetAll()
        };

        return View(model);
    }

    // POST: /DataEntry/Delete
    [HttpPost]
    public IActionResult Delete(string name)
    {
        if (string.IsNullOrEmpty(name)) return RedirectToAction("Index");

        var parts = name.Split(':');
        if (parts.Length != 2) return RedirectToAction("Index");

        var type = parts[0];
        var itemName = parts[1];

        if (type == "participant")
        {
            var participant = _participantRepo.GetAll()
                .FirstOrDefault(p => p.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
            if (participant != null)
                _participantRepo.Delete(participant);
        }
        else if (type == "country")
        {
            var country = _countryRepo.GetAll()
                .FirstOrDefault(c => c.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
            if (country != null)
                _countryRepo.Delete(country);
        }

        return RedirectToAction("DataEntry");
    }

    // POST: /DataEntry/Create
    [HttpPost]
    public IActionResult Create(string Type, string Name, SemiFinal Additional)
    {
        if (string.IsNullOrEmpty(Type)) return RedirectToAction("DataEntry");

        if (Type == "participant")
        {
           _participantRepo.Add(new Participant { Name = Name });
        }
        else if (Type == "country")
        {
            _countryRepo.Add(new Country { Name = Name, SemiFinal = Additional});
        }

        return RedirectToAction("DataEntry");
    }

    // GET: /DataEntry/GetCountry/Name
    [HttpGet]
    public IActionResult GetCountry(string Name)
    {
        var country = _countryRepo.GetByName(Name);
        if (country == null) return NotFound();

        return Json(new
        {
            name = country.Name,
            isInFinal = country.IsInFinal,
            semiFinal = (int)country.SemiFinal,
            placeInFinal = country.PlaceInFinal
        });
    }

    // POST: /DataEntry/Update
    [HttpPost]
    public IActionResult Update(string Type, string Name, bool IsInFinal, SemiFinal SemiFinal, int PlaceInFinal)
    {
        if (Type == "country")
        {
            var oldCountry = _countryRepo.GetByName(Name);

            if (oldCountry == null) return NotFound();

            var newCountry = new Country
            {
                Name = Name,
                IsInFinal = IsInFinal,
                SemiFinal = SemiFinal,
                PlaceInFinal = PlaceInFinal
            };

            _countryRepo.Update(oldCountry, newCountry);
        }

        return RedirectToAction("DataEntry");
    }
}
