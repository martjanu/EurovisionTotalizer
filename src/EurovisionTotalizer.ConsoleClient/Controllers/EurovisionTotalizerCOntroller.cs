using EurovisionTotalizer.Domain.Services;

namespace EurovisionTotalizer.ConsoleClient.Controllers;

public class EurovisionTotalizerCOntroller
{
    private readonly RankingServices _rankingServices;
    private readonly ScoreCalculationServices _scoreCalculationServices;
    private readonly DataCrudServices _dataCrudServices;

    public EurovisionTotalizerCOntroller(
        RankingServices rankingServices,
        ScoreCalculationServices scoreCalculationServices,
        DataCrudServices dataCrudServices)
    {
        _rankingServices = rankingServices;
        _scoreCalculationServices = scoreCalculationServices;
        _dataCrudServices = dataCrudServices;
    }

    public void Run()
    {
        bool isRunning = true;
        while (isRunning)
        {
            Console.Clear();
            Console.WriteLine("Eurovision Totalizer");
            Console.WriteLine("1. View Rankings");
            Console.WriteLine("2. Add Participant");
            Console.WriteLine("3. Exit");
            Console.Write("Select an option: ");
            var input = Console.ReadLine();

            switch (input)
            {
                
            }
        }
    }
}
