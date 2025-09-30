using EurovisionTotalizer.Application.DTOs;
using EurovisionTotalizer.Domain.Enums;
using EurovisionTotalizer.Domain.Models;
using EurovisionTotalizer.Domain.Persistence.Repositories;

namespace EurovisionTotalizer.Application.Services.Home;

public class HomeService : IHomeService
{
    private readonly IJsonStorageRepository<Participant> _participantRepo;
    private readonly IJsonStorageRepository<Country> _countryRepo;

    public HomeService(
        IJsonStorageRepository<Participant> participantRepo,
        IJsonStorageRepository<Country> countryRepo)
    {
        _participantRepo = participantRepo;
        _countryRepo = countryRepo;
    }

    public RepositoriesDto GetRepositories()
    {
        var participants = _participantRepo.GetAll();
        var countries = _countryRepo.GetAll();

        var tables = new List<TableDto>
        {
            CreateTable(SemiFinal.First, "Semifinal 1", participants, countries),
            CreateTable(SemiFinal.Second, "Semifinal 2", participants, countries),
            CreateFinalTable(participants, countries)
        };

        return new RepositoriesDto { Tables = tables };
    }

    private TableDto CreateTable(SemiFinal semiFinal, string title, IEnumerable<Participant> participants, IEnumerable<Country> countries)
    {
        var rows = countries
            .Where(c => c.SemiFinal == semiFinal)
            .Select(c => new TableRowDto
            {
                CountryName = c.Name,
                Predictions = participants.ToDictionary(
                    p => p.Name,
                    p =>
                    {
                        var prediction = p.SemifinalPredictions.FirstOrDefault(x => x.Country.Name == c.Name);
                        return prediction?.Type == PredictionType.DoesNotReachFinal ? "X" : "-";
                    })
            }).ToList();

        return new TableDto
        {
            Title = title,
            Rows = rows,
            ParticipantNames = participants.Select(p => p.Name).Distinct().ToList()
        };
    }

    private TableDto CreateFinalTable(IEnumerable<Participant> participants, IEnumerable<Country> countries)
    {
        var rows = countries
            .Where(c => c.IsInFinal)
            .Select(c => new TableRowDto
            {
                CountryName = c.Name,
                Predictions = participants.ToDictionary(
                    p => p.Name,
                    p =>
                    {
                        var prediction = p.FinalPredictions.FirstOrDefault(x => x.Country.Name == c.Name);
                        if (prediction?.Type == PredictionType.Last3InFinal)
                            return "Bottom3";
                        if (prediction?.Type == PredictionType.ExactPlaceInFinal && prediction.Place >= 1 && prediction.Place <= 10)
                            return prediction.Place.ToString();
                        return "-";
                    })
            }).ToList();

        return new TableDto
        {
            Title = "Final",
            Rows = rows,
            ParticipantNames = participants.Select(p => p.Name).Distinct().ToList()
        };
    }
}
