using EurovisionTotalizer.API.ViewModels;
using EurovisionTotalizer.Domain.Enums;
using EurovisionTotalizer.Domain.Models;
using EurovisionTotalizer.Domain.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EurovisionTotalizer.API.Controllers;

public class PredictionsCOntroller : Controller
{
    private readonly IJsonStorageRepository<Participant> _participantRepo;
    private readonly IJsonStorageRepository<Country> _countryRepo;

    public PredictionsCOntroller(
         IJsonStorageRepository<Participant> participantRepo,
         IJsonStorageRepository<Country> countryRepo)
    {
        _participantRepo = participantRepo;
        _countryRepo = countryRepo;
    }

    // GET: /DataEntry
    public IActionResult Predictions()
    {
        var model = new DataEntryViewModel
        {
            Participants = _participantRepo.GetAll(),
            Countries = _countryRepo.GetAll()
        };

        return View(model);
    }

    // POST: /DataEntry/UpdatePredictions
    [HttpPost]
    public IActionResult UpdatePredictions(string participantName, Dictionary<string, string> SemiPredictions, Dictionary<string, string> FinalPredictions)
    {
        var oldParticipant = _participantRepo.GetByName(participantName);
        var newParticipant = oldParticipant;

        if (newParticipant != null)
        {
            newParticipant.SemifinalPredictions = SemiPredictions.Select(kv => new SemifinalPrediction
            {
                Country = _countryRepo.GetByName(kv.Key),
                Type = Enum.Parse<PredictionType>(kv.Value.Replace(" ", ""), ignoreCase: true)
            }).ToList();

            newParticipant.FinalPredictions = FinalPredictions.Select(kv => new FinalPrediction
            {
                Country = _countryRepo.GetByName(kv.Key),
                Type = kv.Value == "Bottom3" ? PredictionType.Last3InFinal : PredictionType.ExactPlaceInFinal,
                PlaceInFinal = int.TryParse(kv.Value, out var place) ? place : 0,
                IsBottom3 = kv.Value == "Bottom3"
            }).ToList();

            _participantRepo.Update(oldParticipant, newParticipant);
        }

        return RedirectToAction("Predictions");
    }

    // GET: /Predictions/GetPredictions
    [HttpGet]
    public IActionResult GetPredictions(string participantName)
    {
        var participant = _participantRepo.GetByName(participantName);

        if (participant == null) return Json(new { success = false });

        return Json(new
        {
            semi = participant.SemifinalPredictions.Select(p => new
            {
                country = p.Country?.Name,
                type = p.Type.ToString()
            }),
            final = participant.FinalPredictions.Select(p => new
            {
                country = p.Country?.Name,
                type = p.Type.ToString(),
                placeInFinal = p.PlaceInFinal
            })
        });
    }

}
