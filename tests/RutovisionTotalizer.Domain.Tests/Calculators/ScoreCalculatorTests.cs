using EurovisionTotalizer.Domain.Calculators;
using EurovisionTotalizer.Domain.Models;
using EurovisionTotalizer.Domain.Scorers;
using EurovisionTotalizer.Domain.Enums;
using Moq;

namespace EurovisionTotalizer.Tests.Domain.Calculators;

public class ScoreCalculatorTests
{
    private readonly Mock<IPredictionScorer<SemiFinalPrediction>> _semiFinalScorerMock;
    private readonly Mock<IPredictionScorer<FinalPrediction>> _finalScorerMock;
    private readonly ScoreCalculator _scoreCalculator;

    public ScoreCalculatorTests()
    {
        _semiFinalScorerMock = new Mock<IPredictionScorer<SemiFinalPrediction>>();
        _finalScorerMock = new Mock<IPredictionScorer<FinalPrediction>>();
        _scoreCalculator = new ScoreCalculator(_semiFinalScorerMock.Object, _finalScorerMock.Object);
    }

    #region ScoreSemifinalPredictions Tests

    [Fact]
    public void ScoreSemifinalPredictions_NullParticipants_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            _scoreCalculator.ScoreSemifinalPredictions(null, new List<Country>()));
    }

    [Fact]
    public void ScoreSemifinalPredictions_NullCountries_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            _scoreCalculator.ScoreSemifinalPredictions(new List<Participant>(), null));
    }

    [Fact]
    public void ScoreSemifinalPredictions_EmptyParticipants_ReturnsSame()
    {
        var participants = new List<Participant>();
        var result = _scoreCalculator.ScoreSemifinalPredictions(participants, new List<Country>());
        Assert.Equal(participants, result);
    }

    [Fact]
    public void ScoreSemifinalPredictions_CallsScorer_ForEachPrediction()
    {
        var countries = new List<Country>
        {
            new Country { Name = "CountryA" }
        };

        var participants = new List<Participant>
        {
            new Participant
            {
                SemifinalPredictions = new List<SemiFinalPrediction>
                {
                    new SemiFinalPrediction { Country = new Country { Name = "CountryA" } }
                }
            }
        };

        _scoreCalculator.ScoreSemifinalPredictions(participants, countries);

        _semiFinalScorerMock.Verify(s => s.ScorePrediction(It.IsAny<SemiFinalPrediction>(), It.IsAny<Country>(), countries), Times.Once);
    }

    #endregion

    #region ScoreFinalPredictions Tests

    [Fact]
    public void ScoreFinalPredictions_NullParticipants_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            _scoreCalculator.ScoreFinalPredictions(null, new List<Country>()));
    }

    [Fact]
    public void ScoreFinalPredictions_NullCountries_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            _scoreCalculator.ScoreFinalPredictions(new List<Participant>(), null));
    }

    [Fact]
    public void ScoreFinalPredictions_NoFinalCountries_ReturnsSame()
    {
        var participants = new List<Participant>
        {
            new Participant { FinalPredictions = new List<FinalPrediction>() }
        };

        var countries = new List<Country>
        {
            new Country { Name = "CountryA", IsInFinal = false }
        };

        var result = _scoreCalculator.ScoreFinalPredictions(participants, countries);

        Assert.Equal(participants, result);
    }

    [Fact]
    public void ScoreFinalPredictions_CallsScorer_ForEachPrediction()
    {
        var countries = new List<Country>
        {
            new Country { Name = "CountryA", IsInFinal = true }
        };

        var participants = new List<Participant>
        {
            new Participant
            {
                FinalPredictions = new List<FinalPrediction>
                {
                    new FinalPrediction { Country = new Country { Name = "CountryA" } }
                }
            }
        };

        _scoreCalculator.ScoreFinalPredictions(participants, countries);

        _finalScorerMock.Verify(s => s.ScorePrediction(It.IsAny<FinalPrediction>(), It.IsAny<Country>(), countries), Times.Once);
    }

    #endregion

    #region ResetAllPoints Tests

    [Fact]
    public void ResetAllPoints_NullParticipants_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => _scoreCalculator.ResetAllPoints(null));
    }

    [Fact]
    public void ResetAllPoints_ResetsPoints()
    {
        var participants = new List<Participant>
        {
            new Participant
            {
                TotalPoints = 10,
                SemiFinal1Points = 5,
                SemiFinal2Points = 5,
                FinalPoints = 10,
                SemifinalPredictions = new List<SemiFinalPrediction>
                {
                    new SemiFinalPrediction { Points = 3 }
                },
                FinalPredictions = new List<FinalPrediction>
                {
                    new FinalPrediction { Points = 7 }
                }
            }
        };

        _scoreCalculator.ResetAllPoints(participants);

        Assert.All(participants, p =>
        {
            Assert.Equal(0, p.TotalPoints);
            Assert.Equal(0, p.SemiFinal1Points);
            Assert.Equal(0, p.SemiFinal2Points);
            Assert.Equal(0, p.FinalPoints);
            Assert.All(p.SemifinalPredictions, sp => Assert.Equal(0, sp.Points));
            Assert.All(p.FinalPredictions, fp => Assert.Equal(0, fp.Points));
        });
    }

    #endregion

    #region CalculateTotalPoints Tests

    [Fact]
    public void CalculateTotalPoints_NullParticipants_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => _scoreCalculator.CalculateTotalPoints(null));
    }

    [Fact]
    public void CalculateTotalPoints_CalculatesCorrectly()
    {
        var participants = new List<Participant>
        {
            new Participant
            {
                SemifinalPredictions = new List<SemiFinalPrediction>
                {
                    new SemiFinalPrediction { Points = 3, Country = new Country { SemiFinal = SemiFinal.First } },
                    new SemiFinalPrediction { Points = 2, Country = new Country { SemiFinal = SemiFinal.Second } }
                },
                FinalPredictions = new List<FinalPrediction>
                {
                    new FinalPrediction { Points = 5 }
                }
            }
        };

        _scoreCalculator.CalculateTotalPoints(participants);

        var p = participants.First();

        Assert.Equal(10, p.TotalPoints);
        Assert.Equal(3, p.SemiFinal1Points);
        Assert.Equal(2, p.SemiFinal2Points);
        Assert.Equal(5, p.FinalPoints);
    }

    #endregion
}
