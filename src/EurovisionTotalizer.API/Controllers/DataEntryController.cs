using EurovisionTotalizer.Domain.Models;
using EurovisionTotalizer.Domain.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;
using EurovisionTotalizer.API.ViewModels;

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
}
