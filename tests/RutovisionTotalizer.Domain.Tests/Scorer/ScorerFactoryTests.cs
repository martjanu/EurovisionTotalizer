using FluentAssertions;
using EurovisionTotalizer.Domain.Scorers;

namespace EurovisionTotalizer.Tests;

public class ScorerFactoryTests
{
    private readonly ScorerFactory _factory;

    public ScorerFactoryTests()
    {
        _factory = new ScorerFactory();
    }

    [Fact]
    public void CreateBottom3PredictionScorer_Should_Return_Bottom3PredictionScorer()
    {
        // Act
        var scorer = _factory.CreateBottom3PredictionScorer();

        // Assert
        scorer.Should().BeOfType<Bottom3PredictionScorer>();
    }

    [Fact]
    public void CreateTop10PredictionScorer_Should_Return_Top10PredictionScorer()
    {
        // Act
        var scorer = _factory.CreateTop10PredictionScorer();

        // Assert
        scorer.Should().BeOfType<Top10PredictionScorer>();
    }

    [Fact]
    public void CreateFinalPredictionScorer_Should_Return_FinalPredictionScorer_With_Dependencies()
    {
        // Act
        var scorer = _factory.CreateFinalPredictionScorer();

        // Assert
        scorer.Should().BeOfType<FinalPredictionScorer>();

        var finalScorer = scorer as FinalPredictionScorer;
        finalScorer.Should().NotBeNull();
    }

    [Fact]
    public void CreateSemiFinalPredictionScorer_Should_Return_SemiFinalPredictionScorer()
    {
        // Act
        var scorer = _factory.CreateSemiFinalPredictionScorer();

        // Assert
        scorer.Should().BeOfType<SemiFinalPredictionScorer>();
    }
}
