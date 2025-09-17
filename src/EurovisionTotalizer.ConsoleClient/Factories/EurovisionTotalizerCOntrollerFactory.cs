using EurovisionTotalizer.ConsoleClient.Controllers;
using EurovisionTotalizer.ConsoleClient.Factories;

namespace EurovisionTotalizer.ConsoleClient.Factories;

public class EurovisionTotalizerCOntrollerFactory
{
    public EurovisionTotalizerCOntroller Create()
    {
        var _consoleActions = ConsoleActionsFactory.Create();
        return new EurovisionTotalizerCOntroller(_consoleActions);
    }
}
