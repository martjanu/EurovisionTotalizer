namespace EurovisionTotalizer.ConsoleClient.UserActons
{
    public interface IConsoleActions
    {
        string? ReadInput();
        void ShowCommands();
        void ShowMessage(string message);
    }
}