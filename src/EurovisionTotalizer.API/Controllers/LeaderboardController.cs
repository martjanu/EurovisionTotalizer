using Microsoft.AspNetCore.Mvc;
using EurovisionTotalizer.Domain.Rankers;
using EurovisionTotalizer.Domain.Models;
using EurovisionTotalizer.Domain.Persistence.Repositories;
using EurovisionTotalizer.API.ViewModels;
using EurovisionTotalizer.Domain.Calculators;

namespace EurovisionTotalizer.API.Controllers;

public class LeaderboardController : Controller
{
    private readonly IScoreCalculator _scoreController;
    private readonly IParticipantRanker _participantRanker;
    private readonly IJsonStorageRepository<Participant> _participantRepo;
    private readonly IJsonStorageRepository<Country> _countryRepo;

    public LeaderboardController(
        IScoreCalculator scoreController,
        IParticipantRanker participantRanker,
        IJsonStorageRepository<Participant> participantRepo,
        IJsonStorageRepository<Country> countryRepo)
    {
        _scoreController = scoreController ?? throw new ArgumentNullException(nameof(scoreController));
        _participantRanker = participantRanker ?? throw new ArgumentNullException(nameof(participantRanker));
        _participantRepo = participantRepo ?? throw new ArgumentNullException(nameof(participantRepo));
        _countryRepo = countryRepo ?? throw new ArgumentNullException(nameof(countryRepo));
    }

    public IActionResult Leaderboard()
    {
        var participants = _participantRepo.GetAll();
        var countries = _countryRepo.GetAll();

        participants = _scoreController.ResetAllPoints(participants);
        participants = _scoreController.ScoreSemifinalPredictions(participants, countries);
        participants = _scoreController.ScoreFinalPredictions(participants, countries);
        participants = _scoreController.CalculateTotalPoints(participants);

        var model = new LeaderboardViewModel
        {
            Participants = _participantRanker.GetRankedParticipants(participants).ToList()
        };

        return View(model);
    }
}
