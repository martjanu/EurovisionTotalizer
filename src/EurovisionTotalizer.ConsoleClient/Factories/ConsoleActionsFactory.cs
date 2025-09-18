using EurovisionTotalizer.ConsoleClient.UserActons;

namespace EurovisionTotalizer.ConsoleClient.Factories;

public class ConsoleActionsFactory
{
    public IConsoleActions Create() 
        => new ConsoleActions();
}
