using FluentAssertions;
using Moq;
using EurovisionTotalizer.Domain.Scorers;
using EurovisionTotalizer.Domain.Models;

namespace EurovisionTotalizer.Tests;

public class FinalPredictionScorerTests
{
    [Fact]
    public void ScorePrediction_Should_Call_Bottom3Scorer_And_Top10Scorer()
    {
        // Arrange
        var bottom3Mock = new Mock<IPredictionScorer<FinalPrediction>>();
        var top10Mock = new Mock<IPredictionScorer<FinalPrediction>>();

        var scorer = new FinalPredictionScorer(bottom3Mock.Object, top10Mock.Object);

        var prediction = new FinalPrediction { Country = new Country() };
        var country = new Country();
        var allCountries = new List<Country>();

        // Act
        scorer.ScorePrediction(prediction, country, allCountries);

        // Assert
        bottom3Mock.Verify(x => x.ScorePrediction(prediction, country, allCountries), Times.Once);
        top10Mock.Verify(x => x.ScorePrediction(prediction, country, allCountries), Times.Once);
    }


    [Fact]
    public void ScorePrediction_Should_SetPoints_ToZero_BeforeScoring()
    {
        // Arrange
        var bottom3Mock = new Mock<IPredictionScorer<FinalPrediction>>();
        var top10Mock = new Mock<IPredictionScorer<FinalPrediction>>();

        bottom3Mock
            .Setup(x => x.ScorePrediction(It.IsAny<FinalPrediction>(), It.IsAny<Country>(), It.IsAny<IEnumerable<Country>>()))
            .Callback<FinalPrediction, Country, IEnumerable<Country>>((pred, _, __) => pred.Points += 5);

        top10Mock
            .Setup(x => x.ScorePrediction(It.IsAny<FinalPrediction>(), It.IsAny<Country>(), It.IsAny<IEnumerable<Country>>()))
            .Callback<FinalPrediction, Country, IEnumerable<Country>>((pred, _, __) => pred.Points += 3);

        var scorer = new FinalPredictionScorer(bottom3Mock.Object, top10Mock.Object);

        var prediction = new FinalPrediction { Points = 100, Country = new Country() };
        var country = new Country();
        var allCountries = new List<Country>();

        // Act
        scorer.ScorePrediction(prediction, country, allCountries);

        // Assert
        prediction.Points.Should().Be(8); // 0 + 5 + 3
    }

    [Fact]
    public void ScorePrediction_Should_DoNothing_IfPredictionOrCountryIsNull()
    {
        // Arrange
        var bottom3Mock = new Mock<IPredictionScorer<FinalPrediction>>();
        var top10Mock = new Mock<IPredictionScorer<FinalPrediction>>();

        var scorer = new FinalPredictionScorer(bottom3Mock.Object, top10Mock.Object);

        var prediction = new FinalPrediction { Points = 50 };
        Country? country = null;
        var allCountries = new List<Country>();

        // Act
        scorer.ScorePrediction(prediction, country, allCountries);

        // Assert
        prediction.Points.Should().Be(0);
        bottom3Mock.Verify(x => x.ScorePrediction(It.IsAny<FinalPrediction>(), It.IsAny<Country>(), It.IsAny<IEnumerable<Country>>()), Times.Never);
        top10Mock.Verify(x => x.ScorePrediction(It.IsAny<FinalPrediction>(), It.IsAny<Country>(), It.IsAny<IEnumerable<Country>>()), Times.Never);

        // Case where prediction.Country is null
        prediction = new FinalPrediction { Points = 50, Country = null };
        country = new Country();

        scorer.ScorePrediction(prediction, country, allCountries);

        prediction.Points.Should().Be(0);
        bottom3Mock.Verify(x => x.ScorePrediction(It.IsAny<FinalPrediction>(), It.IsAny<Country>(), It.IsAny<IEnumerable<Country>>()), Times.Never);
        top10Mock.Verify(x => x.ScorePrediction(It.IsAny<FinalPrediction>(), It.IsAny<Country>(), It.IsAny<IEnumerable<Country>>()), Times.Never);
    }
}
