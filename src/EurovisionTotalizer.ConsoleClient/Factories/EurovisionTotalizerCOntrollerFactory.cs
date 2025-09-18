using EurovisionTotalizer.ConsoleClient.Controllers;
using EurovisionTotalizer.Domain.Factories;
using EurovisionTotalizer.Domain.Models;
using EurovisionTotalizer.Domain.Persistence.Factories;

namespace EurovisionTotalizer.ConsoleClient.Factories;

public class EurovisionTotalizerCOntrollerFactory
{
    public EurovisionTotalizerCOntroller Create()
    {
        var _consoleActionsFactory = new ConsoleActionsFactory();
        var _consoleActions = _consoleActionsFactory.Create();

        var _serializerFactory = new JsonSerializerFactory();
        var _serializer = _serializerFactory.Create();

        var _participantRepositoryFactory = new JsonStorageFactory();
        var _participantRepository = _participantRepositoryFactory.Create<Participant>("participant.json", _serializer);

        var _countryRepositoryFactory = new JsonStorageFactory();
        var _countryRepository = _participantRepositoryFactory.Create<Country>("country.json", _serializer);

        var _participantRankerFactory = new ParticipantRankerFactory();
        var _participantRanker = _participantRankerFactory.Create(_participantRepository);

        var _scoreControllerFactory = new ScoreControllerFactory();
        var _scoreController = _scoreControllerFactory.CreateScoreController();

        return new EurovisionTotalizerCOntroller(
            _consoleActions,
            _participantRanker,
            _countryRepository,
            _participantRepository,
            _scoreController);
    }
}
