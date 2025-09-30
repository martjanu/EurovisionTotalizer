using EurovisionTotalizer.Domain.Calculators;
using EurovisionTotalizer.Domain.Models;
using EurovisionTotalizer.Domain.Scorers;
using Moq;

namespace EurovisionTotalizer.Tests.Domain.Calculators;

public class ScoreCalculatorFactoryTests
{
    [Fact]
    public void CreateScoreController_ReturnsScoreCalculatorInstance()
    {
        // Arrange
        var semiFinalScorerMock = new Mock<IPredictionScorer<SemiFinalPrediction>>();
        var finalScorerMock = new Mock<IPredictionScorer<FinalPrediction>>();

        var factory = new ScoreCalculatorFactory();

        // Act
        var calculator = factory.CreateScoreController(semiFinalScorerMock.Object, finalScorerMock.Object);

        // Assert
        Assert.NotNull(calculator);
        Assert.IsType<ScoreCalculator>(calculator);
    }

    [Fact]
    public void CreateScoreController_SetsDependenciesCorrectly()
    {
        // Arrange
        var semiFinalScorerMock = new Mock<IPredictionScorer<SemiFinalPrediction>>();
        var finalScorerMock = new Mock<IPredictionScorer<FinalPrediction>>();

        var factory = new ScoreCalculatorFactory();

        // Act
        var calculator = factory.CreateScoreController(semiFinalScorerMock.Object, finalScorerMock.Object);

        // Assert
        var scoreCalculator = Assert.IsType<ScoreCalculator>(calculator);

        // Naudojame Reflection, kad patikrintume priklausomybes
        var semiFinalField = typeof(ScoreCalculator)
            .GetField("_semifinalScorer", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        var finalField = typeof(ScoreCalculator)
            .GetField("_finalScorer", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        Assert.Equal(semiFinalScorerMock.Object, semiFinalField.GetValue(scoreCalculator));
        Assert.Equal(finalScorerMock.Object, finalField.GetValue(scoreCalculator));
    }
}
