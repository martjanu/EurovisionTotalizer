using EurovisionTotalizer.Domain.Services;
using EurovisionTotalizer.ConsoleClient.UserActons;

namespace EurovisionTotalizer.ConsoleClient.Controllers;

public class EurovisionTotalizerCOntroller
{
    private readonly IConsoleActions _consoleClient;
    private bool isRunning = true;

    public EurovisionTotalizerCOntroller(
        IConsoleActions consoleClient)
    {
        _consoleClient = consoleClient;
    }

    public void Run()
    {
        _consoleClient.ShowCommands();
        while (isRunning)
        {
            var input = _consoleClient.ReadInput();
            switch (input)
            {
                case "1":
                    _consoleClient.ShowMessage("View Rankings selected.");
                    break;
                case "2":
                    _consoleClient.ShowMessage("Add Prediction or Country selected.");
                    break;
                case "3":
                    _consoleClient.ShowMessage("View Predictions or Countries selected.");
                    break;
                case "4":
                    _consoleClient.ShowMessage("Delete Participant or Country selected.");
                    break;
                case "5":
                    _consoleClient.ShowMessage("Update Participant or Country selected.");
                    break;
                case "6":
                    _consoleClient.ShowMessage("Make Semifinal Predictions selected.");
                    break;
                case "7":
                    _consoleClient.ShowMessage("Make Final Predictions selected.");
                    break;
                case "8":
                    Exit();
                    break;
                default:
                    _consoleClient.ShowMessage("Invalid option. Please try again.");
                    break;
            }
        }
    }

    private void Exit()
    {
        isRunning = !isRunning;
    }
}
