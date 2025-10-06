namespace EurovisionTotalizer.ConsoleClient.UserActons;

public class ConsoleActions : IConsoleActions
{
    private IEnumerable<string> _commands = new List<string>
    {
        "1. View Rankings",
        "2. Add Prediction or Country",
        "3. View Predictions or Countries",
        "4. Delete Participant or Country",
        "5. Exit"
    };

    public void ShowCommands()
    {
        Console.Clear();
        Console.WriteLine("Eurovision Totalizer");
        foreach (var command in _commands)
        {
            Console.WriteLine(command);
        }
    }

    public string? ReadInput()
    {
        return Console.ReadLine();
    }

    public void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }
}
