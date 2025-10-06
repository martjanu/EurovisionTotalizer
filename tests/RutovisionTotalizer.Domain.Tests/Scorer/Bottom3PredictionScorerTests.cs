using FluentAssertions;
using EurovisionTotalizer.Domain.Scorers;
using EurovisionTotalizer.Domain.Models;

namespace EurovisionTotalizer.Tests;

public class Bottom3PredictionScorerTests
{
    [Fact]
    public void ScorePrediction_Should_SetPoints_When_IsBottom3_And_Country_In_Bottom3()
    {
        // Arrange
        var scorer = new Bottom3PredictionScorer();

        var prediction = new FinalPrediction { IsBottom3 = true };
        var country = new Country { PlaceInFinal = 8, IsInFinal = true };
        var allCountries = new List<Country>
        {
            new Country { IsInFinal = true },
            new Country { IsInFinal = true },
            new Country { IsInFinal = true },
            new Country { IsInFinal = true },
            new Country { IsInFinal = true },
            new Country { IsInFinal = true },
            new Country { IsInFinal = true },
            country // placeInFinal = 8
        };

        // Act
        scorer.ScorePrediction(prediction, country, allCountries);

        // Assert
        prediction.Points.Should().Be(1);
    }

    [Fact]
    public void ScorePrediction_Should_Not_SetPoints_When_IsBottom3_False()
    {
        // Arrange
        var scorer = new Bottom3PredictionScorer();

        var prediction = new FinalPrediction { IsBottom3 = false };
        var country = new Country { PlaceInFinal = 8, IsInFinal = true };
        var allCountries = new List<Country>
        {
            new Country { IsInFinal = true },
            new Country { IsInFinal = true },
            new Country { IsInFinal = true },
            new Country { IsInFinal = true },
            new Country { IsInFinal = true },
            new Country { IsInFinal = true },
            new Country { IsInFinal = true },
            country
        };

        // Act
        scorer.ScorePrediction(prediction, country, allCountries);

        // Assert
        prediction.Points.Should().Be(0);
    }

    [Fact]
    public void ScorePrediction_Should_Not_SetPoints_When_Country_Not_In_Bottom3()
    {
        // Arrange
        var scorer = new Bottom3PredictionScorer();

        var prediction = new FinalPrediction { IsBottom3 = true };
        var country = new Country { PlaceInFinal = 5, IsInFinal = true };
        var allCountries = new List<Country>
        {
            new Country { IsInFinal = true },
            new Country { IsInFinal = true },
            new Country { IsInFinal = true },
            new Country { IsInFinal = true },
            new Country { IsInFinal = true },
            country
        };

        // Act
        scorer.ScorePrediction(prediction, country, allCountries);

        // Assert
        prediction.Points.Should().Be(1);
    }

    [Fact]
    public void ScorePrediction_Should_Handle_Multiple_Countries_Correctly()
    {
        // Arrange
        var scorer = new Bottom3PredictionScorer();

        var allCountries = new List<Country>
        {
            new Country { PlaceInFinal = 1, IsInFinal = true },
            new Country { PlaceInFinal = 2, IsInFinal = true },
            new Country { PlaceInFinal = 3, IsInFinal = true },
            new Country { PlaceInFinal = 4, IsInFinal = true },
            new Country { PlaceInFinal = 5, IsInFinal = true },
            new Country { PlaceInFinal = 6, IsInFinal = true },
            new Country { PlaceInFinal = 7, IsInFinal = true },
            new Country { PlaceInFinal = 8, IsInFinal = true },
            new Country { PlaceInFinal = 9, IsInFinal = true }
        };

        var prediction = new FinalPrediction { IsBottom3 = true };

        // Act & Assert
        foreach (var country in allCountries)
        {
            prediction.Points = 0;
            scorer.ScorePrediction(prediction, country, allCountries);

            if (country.PlaceInFinal >= allCountries.Count(c => c.IsInFinal) - 2)
            {
                prediction.Points.Should().Be(1, $"Country {country.PlaceInFinal} should be bottom 3");
            }
            else
            {
                prediction.Points.Should().Be(0, $"Country {country.PlaceInFinal} should not be bottom 3");
            }
        }
    }
}
