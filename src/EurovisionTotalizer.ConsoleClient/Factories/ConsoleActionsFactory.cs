using EurovisionTotalizer.ConsoleClient.UserActons;

namespace EurovisionTotalizer.ConsoleClient.Factories;

public class ConsoleActionsFactory
{
    public static IConsoleActions Create() 
        => new ConsoleActions();
}
