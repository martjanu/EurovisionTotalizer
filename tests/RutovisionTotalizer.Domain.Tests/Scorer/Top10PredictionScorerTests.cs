using FluentAssertions;
using EurovisionTotalizer.Domain.Scorers;
using EurovisionTotalizer.Domain.Models;

namespace EurovisionTotalizer.Tests;

public class Top10PredictionScorerTests
{
    private readonly Top10PredictionScorer _scorer;

    public Top10PredictionScorerTests()
    {
        _scorer = new Top10PredictionScorer();
    }

    [Fact]
    public void ScorePrediction_Should_SetPointsToTwo_WhenPredictionPlaceMatchesCountryPlaceInFinal()
    {
        // Arrange
        var prediction = new FinalPrediction
        {
            Country = new Country { Name = "A" },
            Place = 5,
            Points = 0
        };

        var country = new Country
        {
            Name = "A",
            PlaceInFinal = 5
        };

        var allCountries = new List<Country> { country };

        // Act
        _scorer.ScorePrediction(prediction, country, allCountries);

        // Assert
        prediction.Points.Should().Be(2);
    }

    [Fact]
    public void ScorePrediction_Should_SetPointsToOne_WhenPredictionPlaceIsOneOffCountryPlaceInFinal()
    {
        // Arrange
        var prediction = new FinalPrediction
        {
            Country = new Country { Name = "B" },
            Place = 4,
            Points = 0
        };

        var country = new Country
        {
            Name = "B",
            PlaceInFinal = 5
        };

        var allCountries = new List<Country> { country };

        // Act
        _scorer.ScorePrediction(prediction, country, allCountries);

        // Assert
        prediction.Points.Should().Be(1);
    }

    [Fact]
    public void ScorePrediction_Should_SetPointsToZero_WhenPredictionPlaceIsNotMatchingOrOneOff()
    {
        // Arrange
        var prediction = new FinalPrediction
        {
            Country = new Country { Name = "C" },
            Place = 3,
            Points = 5
        };

        var country = new Country
        {
            Name = "C",
            PlaceInFinal = 6
        };

        var allCountries = new List<Country> { country };

        // Act
        _scorer.ScorePrediction(prediction, country, allCountries);

        // Assert
        prediction.Points.Should().Be(0);
    }

    [Fact]
    public void ScorePrediction_Should_SetPointsToZero_WhenCountryPlaceInFinalGreaterThanTen()
    {
        // Arrange
        var prediction = new FinalPrediction
        {
            Country = new Country { Name = "D" },
            Place = 1,
            Points = 5
        };

        var country = new Country
        {
            Name = "D",
            PlaceInFinal = 11
        };

        var allCountries = new List<Country> { country };

        // Act
        _scorer.ScorePrediction(prediction, country, allCountries);

        // Assert
        prediction.Points.Should().Be(0);
    }
}
