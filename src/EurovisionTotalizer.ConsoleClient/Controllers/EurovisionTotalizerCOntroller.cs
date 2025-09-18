using EurovisionTotalizer.Domain.Services;
using EurovisionTotalizer.ConsoleClient.UserActons;
using EurovisionTotalizer.Domain.Rankers;
using EurovisionTotalizer.Domain.Models;

namespace EurovisionTotalizer.ConsoleClient.Controllers;

public class EurovisionTotalizerCOntroller
{
    private readonly IConsoleActions _consoleClient;
    private readonly IParticipantRanker _participantRanker;
    private readonly IDataCrudService<Country> _countryService;
    private readonly IDataCrudService<Participant> _participantService;

    private bool isRunning = true;

    public EurovisionTotalizerCOntroller(
        IConsoleActions consoleClient,
        IParticipantRanker participantRanker,
         IDataCrudService<Country> countryService,
        IDataCrudService<Participant> participantService)
    {
        _consoleClient = consoleClient;
        _participantRanker = participantRanker;
        _participantService = participantService;
        _countryService = countryService;
        _participantService = participantService;
    }

    public void Run()
    {
        _consoleClient.ShowCommands();
        while (isRunning)
        {
            var input = _consoleClient.ReadInput();
            var result = input switch
            {
                "1" => ShowRankings(),
                "8" => Exit("Programs is ended"),
                _ => CommandNotFound(input ?? string.Empty)
            };
        }
    }

    private bool CommandNotFound(string command)
    {
        _consoleClient.ShowMessage($"Command '{command}' not found. Please try again.");
        return true;
    }

    private bool Exit(string message)
    {
        _consoleClient.ShowMessage(message);
        return !isRunning;
    }

    private bool ShowRankings()
    {
        var rankedParticipants = _participantRanker.GetRankedParticipants();
        foreach (var participant in rankedParticipants)
        {
            _consoleClient.ShowMessage($"{participant.Name} - {participant.TotalPoints} points");
        }
        return true;
    }
}
