using Microsoft.AspNetCore.Mvc;
using EurovisionTotalizer.Domain.Calculators;
using EurovisionTotalizer.Domain.Rankers;
using EurovisionTotalizer.Domain.Models;
using EurovisionTotalizer.Domain.Persistence.Repositories;
using EurovisionTotalizer.API.ViewModels;

namespace EurovisionTotalizer.API.Controllers;

public class LeaderboardController : Controller
{
    private readonly IScoreController _scoreController;
    private readonly IParticipantRanker _participantRanker;
    private readonly IJsonStorageRepository<Participant> _participantRepo;

    public LeaderboardController(
        IScoreController scoreController,
        IParticipantRanker participantRanker,
        IJsonStorageRepository<Participant> participantRepo)
    {
        _scoreController = scoreController ?? throw new ArgumentNullException(nameof(scoreController));
        _participantRanker = participantRanker ?? throw new ArgumentNullException(nameof(participantRanker));
        _participantRepo = participantRepo ?? throw new ArgumentNullException(nameof(participantRepo));
    }

    public IActionResult Leaderboard()
    {
        var scoredParticipants = _participantRepo.GetAll();
        scoredParticipants = _scoreController.CalculateTotalPoints(scoredParticipants);

        var model = new LeaderboardViewModel
        {
            Participants = _participantRanker.GetRankedParticipants(_participantRepo.GetAll())
        };

        return View(model);
    }
}
