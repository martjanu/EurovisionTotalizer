using FluentAssertions;
using EurovisionTotalizer.Domain.Scorers;
using EurovisionTotalizer.Domain.Models;
using EurovisionTotalizer.Domain.Enums;

namespace EurovisionTotalizer.Tests;

public class SemiFinalPredictionScorerTests
{
    private readonly SemiFinalPredictionScorer _scorer;

    public SemiFinalPredictionScorerTests()
    {
        _scorer = new SemiFinalPredictionScorer();
    }

    [Fact]
    public void ScorePrediction_Should_SetPointsToZero_WhenPredictionOrCountryIsNull()
    {
        // Arrange
        var prediction = new SemiFinalPrediction { Points = 5, Country = null };
        Country? country = null;
        var allCountries = new List<Country>();

        // Act
        _scorer.ScorePrediction(prediction, country, allCountries);

        // Assert
        prediction.Points.Should().Be(0);

        // Case: prediction.Country is null but country is not
        prediction = new SemiFinalPrediction { Points = 5, Country = null };
        country = new Country();
        _scorer.ScorePrediction(prediction, country, allCountries);
        prediction.Points.Should().Be(0);
    }

    [Fact]
    public void ScorePrediction_Should_SetPointsToOne_WhenPredictionTypeDoesNotReachFinal_AndCountryIsNotInFinal()
    {
        // Arrange
        var prediction = new SemiFinalPrediction
        {
            Points = 0,
            Country = new Country { Name = "A" },
            Type = PredictionType.DoesNotReachFinal
        };
        var country = new Country { Name = "A", IsInFinal = false };
        var allCountries = new List<Country> { country };

        // Act
        _scorer.ScorePrediction(prediction, country, allCountries);

        // Assert
        prediction.Points.Should().Be(1);
    }

    [Fact]
    public void ScorePrediction_Should_KeepPointsZero_WhenPredictionTypeDoesNotReachFinal_ButCountryIsInFinal()
    {
        // Arrange
        var prediction = new SemiFinalPrediction
        {
            Points = 0,
            Country = new Country { Name = "A" },
            Type = PredictionType.DoesNotReachFinal
        };
        var country = new Country { Name = "A", IsInFinal = true };
        var allCountries = new List<Country> { country };

        // Act
        _scorer.ScorePrediction(prediction, country, allCountries);

        // Assert
        prediction.Points.Should().Be(0);
    }

    [Fact]
    public void ScorePrediction_Should_KeepPointsZero_WhenPredictionTypeIsNotDoesNotReachFinal()
    {
        // Arrange
        var prediction = new SemiFinalPrediction
        {
            Points = 5,
            Country = new Country { Name = "A" },
            Type = PredictionType.ExactPlaceInFinal
        };
        var country = new Country { Name = "A", IsInFinal = false };
        var allCountries = new List<Country> { country };

        // Act
        _scorer.ScorePrediction(prediction, country, allCountries);

        // Assert
        prediction.Points.Should().Be(0);
    }
}
