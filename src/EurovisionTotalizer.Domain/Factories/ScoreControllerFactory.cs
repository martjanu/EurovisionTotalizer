using EurovisionTotalizer.Domain.Calculators;
using EurovisionTotalizer.Domain.Models;

namespace EurovisionTotalizer.Domain.Factories;

public class ScoreControllerFactory
{
    public IScoreCalculator CreateScoreController()
        => new ScoreCalculator();
}
