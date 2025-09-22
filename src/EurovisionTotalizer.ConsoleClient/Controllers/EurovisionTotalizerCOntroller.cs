using EurovisionTotalizer.ConsoleClient.UserActons;
using EurovisionTotalizer.Domain.Rankers;
using EurovisionTotalizer.Domain.Models;
using EurovisionTotalizer.Domain.Persistence.Repositories;
using EurovisionTotalizer.Domain.Enums;
using EurovisionTotalizer.Domain.Calculators;

namespace EurovisionTotalizer.ConsoleClient.Controllers;

public class EurovisionTotalizerCOntroller
{
    private readonly IConsoleActions _consoleClient;
    private readonly IParticipantRanker _participantRanker;
    private readonly IJsonStorageRepository<Country> _countryRepository;
    private readonly IJsonStorageRepository<Participant> _participantRepository;
    private readonly IScoreController _scoreController;

    private bool isRunning = true;

    public EurovisionTotalizerCOntroller(
        IConsoleActions consoleClient,
        IParticipantRanker participantRanker,
        IJsonStorageRepository<Country> countryRepository,
        IJsonStorageRepository<Participant> participantRepository,
        IScoreController scoreController)
    {
        _consoleClient = consoleClient;
        _participantRanker = participantRanker;
        _countryRepository = countryRepository;
        _participantRepository = participantRepository;
        _scoreController = scoreController;
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
                "2" => AddNewItem(),
                "3" => ViewItems(),
                "4" => DeleteItem(),
                "5" => Exit("Programs is ended"),
                _ => CommandNotFound(input ?? string.Empty)
            };
        }
    }

    private bool AddNewItem()
    {
        _consoleClient.ShowMessage("Country(1) or Participant(2)");
        var input = _consoleClient.ReadInput();
        if (input == "1")
        {
            _consoleClient.ShowMessage("Enter country name:");
            var countryName = _consoleClient.ReadInput();
            _consoleClient.ShowMessage("Is in final? (yes/no):");
            var isInFinalInput = _consoleClient.ReadInput();
            _consoleClient.ShowMessage("Semi-final (1/2):");
            var semiFinalInput = _consoleClient.ReadInput();
            var newCountry = new Country
            {
                Name = countryName ?? "Missing Name",
                IsInFinal = isInFinalInput?.ToLower() == "yes",
                SemiFinal = semiFinalInput == "1" ? SemiFinal.First : SemiFinal.Second
            };
            _countryRepository.Add(newCountry);
        }
        else if (input == "2")
        {
            _consoleClient.ShowMessage("Enter participant name:");
            var participantName = _consoleClient.ReadInput();
            var newParticipant = new Participant
            {
                Name = participantName
            };
            _participantRepository.Add(newParticipant);
        }
        else
        {
            _consoleClient.ShowMessage("Command not found. Please try again.");
        }
        return true;
    }

    private bool ViewItems()
    {
        _consoleClient.ShowMessage("Country(1) or Participant(2)");
        var input = _consoleClient.ReadInput();
        if (input == "1")
        {
            var countries = _countryRepository.GetAll();
            foreach (var country in countries)
            {
                _consoleClient.ShowMessage($"{country.Name} - In Final: {country.IsInFinal} - SemiFinal: {country.SemiFinal} {country.PlaceInFinal}");
            }
        }
        else if (input == "2")
        {
            var participants = _participantRepository.GetAll();
            foreach (var participant in participants)
            {
                _consoleClient.ShowMessage($"{participant.Name}");
            }
        }
        else
        {
            _consoleClient.ShowMessage("Command not found. Please try again.");
        }
        return true;
    }

    private bool DeleteItem()
    {
        _consoleClient.ShowMessage("Country(1) or Participant(2)");
        var input = _consoleClient.ReadInput();
        if (input == "1")
        {
            _consoleClient.ShowMessage("Enter country name to delete:");
            var countryName = _consoleClient.ReadInput();
            var country = _countryRepository.GetAll().FirstOrDefault(c => c.Name.Equals(countryName, StringComparison.OrdinalIgnoreCase));
            if (country != null)
            {
                _countryRepository.Delete(country);
                _consoleClient.ShowMessage($"Country '{countryName}' deleted.");
            }
            else
            {
                _consoleClient.ShowMessage($"Country '{countryName}' not found.");
            }
        }
        else if (input == "2")
        {
            _consoleClient.ShowMessage("Enter participant name to delete:");
            var participantName = _consoleClient.ReadInput();
            var participant = _participantRepository.GetAll().FirstOrDefault(p => p.Name.Equals(participantName, StringComparison.OrdinalIgnoreCase));
            if (participant != null)
            {
                _participantRepository.Delete(participant);
                _consoleClient.ShowMessage($"Participant '{participantName}' deleted.");
            }
            else
            {
                _consoleClient.ShowMessage($"Participant '{participantName}' not found.");
            }
        }
        else
        {
            _consoleClient.ShowMessage("Command not found. Please try again.");
        }
        return true;
    }

    private bool CommandNotFound(string command)
    {
        _consoleClient.ShowMessage($"Command '{command}' not found. Please try again.");
        return true;
    }

    private bool Exit(string message)
    {
        _consoleClient.ShowMessage(message);
        return false;
    }

    private bool ShowRankings()
    {
        _scoreController.ResetAllPoints(_participantRepository.GetAll());

        _scoreController.ScoreSemifinalPredictions(
            _participantRepository.GetAll(), _countryRepository.GetAll());

        _scoreController.ScoreFinalPredictions(
            _participantRepository.GetAll(), _countryRepository.GetAll());

        var rankedParticipants = _participantRanker.GetRankedParticipants();
        foreach (var participant in rankedParticipants)
        {
            _consoleClient.ShowMessage($"{participant.Name} - {participant.TotalPoints} points " +
                $"{participant.SemiFinal1Points}-{participant.SemiFinal2Points}-{participant.FinalPoints}");
        }
        return true;
    }
}
