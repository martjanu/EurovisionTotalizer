using EurovisionTotalizer.Domain.Calculators;
using EurovisionTotalizer.Domain.Enums;
using EurovisionTotalizer.Domain.Models;

namespace EurovisionTotalizer.Tests.Calculators;

public class ScoreControllerTests
{
    private readonly ScoreController _sut = new();

    #region ScoreSemifinalPredictions

    [Fact]
    public void ScoreSemifinalPredictions_ShouldThrow_WhenParticipantsNull()
    {
        Assert.Throws<ArgumentNullException>(() =>
            _sut.ScoreSemifinalPredictions(null!, new List<Country>()));
    }

    [Fact]
    public void ScoreSemifinalPredictions_ShouldThrow_WhenCountriesNull()
    {
        Assert.Throws<ArgumentNullException>(() =>
            _sut.ScoreSemifinalPredictions(new List<Participant>(), null!));
    }

    [Fact]
    public void ScoreSemifinalPredictions_ShouldDoNothing_WhenParticipantsEmpty()
    {
        var countries = new List<Country> { new() { Name = "A", IsInFinal = true } };

        _sut.ScoreSemifinalPredictions(new List<Participant>(), countries);

        Assert.True(true); 
    }

    [Fact]
    public void ScoreSemifinalPredictions_ShouldAwardPoint_WhenCorrectlyPredictedDoesNotReachFinal()
    {
        var participants = new List<Participant>
        {
            new() { SemifinalPredictions = new List<SemifinalPrediction>
            {
                new SemifinalPrediction
                {
                    Country = new Country { Name = "A", IsInFinal = false },
                    Type = PredictionType.DoesNotReachFinal
                }
            }}
        };

        var countries = new List<Country>
        {
            new() { Name = "A", IsInFinal = false }
        };

        _sut.ScoreSemifinalPredictions(participants, countries);

        Assert.Equal(1, participants.First().SemifinalPredictions.First().Points);
    }

    [Fact]
    public void ScoreSemifinalPredictions_ShouldNotAwardPoint_WhenPredictionIncorrect()
    {
        var participants = new List<Participant>
        {
            new() { SemifinalPredictions = new List<SemifinalPrediction>
            {
                new SemifinalPrediction
                {
                    Country = new Country { Name = "A", IsInFinal = true },
                    Type = PredictionType.DoesNotReachFinal
                }
            }}
        };

        var countries = new List<Country>
        {
            new() { Name = "A", IsInFinal = true }
        };

        _sut.ScoreSemifinalPredictions(participants, countries);

        Assert.Equal(0, participants.First().SemifinalPredictions.First().Points);
    }

    #endregion

    #region ScoreFinalPredictions

    [Fact]
    public void ScoreFinalPredictions_ShouldThrow_WhenParticipantsNull()
    {
        Assert.Throws<ArgumentNullException>(() =>
            _sut.ScoreFinalPredictions(null!, new List<Country>()));
    }

    [Fact]
    public void ScoreFinalPredictions_ShouldThrow_WhenCountriesNull()
    {
        Assert.Throws<ArgumentNullException>(() =>
            _sut.ScoreFinalPredictions(new List<Participant>(), null!));
    }

    [Fact]
    public void ScoreFinalPredictions_ShouldDoNothing_WhenParticipantsEmpty()
    {
        var countries = new List<Country> { new() { Name = "A", IsInFinal = true, PlaceInFinal = 1 } };

        _sut.ScoreFinalPredictions(new List<Participant>(), countries);

        Assert.True(true);
    }

    [Fact]
    public void ScoreFinalPredictions_ShouldAwardPoint_WhenBottom3Correct()
    {
        var participants = new List<Participant>
        {
            new()
            {
                FinalPredictions = new List<FinalPrediction>
                {
                    new FinalPrediction
                    {
                        Country = new Country { Name = "A" },
                        IsBottom3 = true
                    }
                }
            }
        };

        var countries = new List<Country>
        {
            new() { Name = "A", IsInFinal = true, PlaceInFinal = 25 }
        };

        _sut.ScoreFinalPredictions(participants, countries);

        Assert.Equal(1, participants.First().FinalPredictions.First().Points);
    }

    [Fact]
    public void ScoreFinalPredictions_ShouldAwardTwoPoints_WhenExactTop10PlaceCorrect()
    {
        var participants = new List<Participant>
        {
            new()
            {
                FinalPredictions = new List<FinalPrediction>
                {
                    new FinalPrediction
                    {
                        Country = new Country { Name = "A" },
                        PlaceInFinal = 5
                    }
                }
            }
        };

        var countries = new List<Country>
        {
            new() { Name = "A", IsInFinal = true, PlaceInFinal = 5 }
        };

        _sut.ScoreFinalPredictions(participants, countries);

        Assert.Equal(2, participants.First().FinalPredictions.First().Points);
    }

    [Fact]
    public void ScoreFinalPredictions_ShouldAwardOnePoint_WhenTop10PlaceOffByOne()
    {
        var participants = new List<Participant>
        {
            new()
            {
                FinalPredictions = new List<FinalPrediction>
                {
                    new FinalPrediction
                    {
                        Country = new Country { Name = "A" },
                        PlaceInFinal = 5
                    }
                }
            }
        };

        var countries = new List<Country>
        {
            new() { Name = "A", IsInFinal = true, PlaceInFinal = 6 }
        };

        _sut.ScoreFinalPredictions(participants, countries);

        Assert.Equal(1, participants.First().FinalPredictions.First().Points);
    }

    [Fact]
    public void ScoreFinalPredictions_ShouldNotAwardPoints_WhenNotInTop10()
    {
        var participants = new List<Participant>
        {
            new()
            {
                FinalPredictions = new List<FinalPrediction>
                {
                    new FinalPrediction
                    {
                        Country = new Country { Name = "A" },
                        PlaceInFinal = 15
                    }
                }
            }
        };

        var countries = new List<Country>
        {
            new() { Name = "A", IsInFinal = true, PlaceInFinal = 15 }
        };

        _sut.ScoreFinalPredictions(participants, countries);

        Assert.Equal(0, participants.First().FinalPredictions.First().Points);
    }

    #endregion

    #region ResetAllPoints

    [Fact]
    public void ResetAllPoints_ShouldThrow_WhenParticipantsNull()
    {
        Assert.Throws<ArgumentNullException>(() => _sut.ResetAllPoints(null!));
    }

    [Fact]
    public void ResetAllPoints_ShouldDoNothing_WhenParticipantsEmpty()
    {
        _sut.ResetAllPoints(new List<Participant>());

        Assert.True(true);
    }

    [Fact]
    public void ResetAllPoints_ShouldResetPoints()
    {
        var participants = new List<Participant>
        {
            new()
            {
                TotalPoints = 10,
                SemifinalPredictions = new List<SemifinalPrediction>
                {
                    new SemifinalPrediction { Points = 1 }
                },
                FinalPredictions = new List<FinalPrediction>
                {
                    new FinalPrediction { Points = 2 }
                }
            }
        };

        _sut.ResetAllPoints(participants);

        var p = participants.First();
        Assert.Equal(0, p.TotalPoints);
        Assert.All(p.SemifinalPredictions, sp => Assert.Equal(0, sp.Points));
        Assert.All(p.FinalPredictions, fp => Assert.Equal(0, fp.Points));
    }

    #endregion

    #region CalculateTotalPoints

    [Fact]
    public void CalculateTotalPoints_ShouldThrow_WhenParticipantsNull()
    {
        Assert.Throws<ArgumentNullException>(() => _sut.CalculateTotalPoints(null!));
    }

    [Fact]
    public void CalculateTotalPoints_ShouldDoNothing_WhenParticipantsEmpty()
    {
        _sut.CalculateTotalPoints(new List<Participant>());
        Assert.True(true);
    }

    [Fact]
    public void CalculateTotalPoints_ShouldSumAllPointsIntoTotalPoints()
    {
        var participant = new Participant
        {
            SemifinalPredictions = new List<SemifinalPrediction>
            {
                new() { Points = 2, Country = new Country { SemiFinal = SemiFinal.First }, Type = PredictionType.DoesNotReachFinal },
                new() { Points = 3, Country = new Country { SemiFinal = SemiFinal.Second }, Type = PredictionType.DoesNotReachFinal }
            },
            FinalPredictions = new List<FinalPrediction>
            {
                new() { Points = 5 }
            }
        };

        _sut.CalculateTotalPoints(new[] { participant });

        Assert.Equal(10, participant.TotalPoints); // 2 + 3 + 5
    }

    [Fact]
    public void CalculateTotalPoints_ShouldCalculateSemiFinal1Points()
    {
        var participant = new Participant
        {
            SemifinalPredictions = new List<SemifinalPrediction>
            {
                new() { Points = 1, Country = new Country { SemiFinal = SemiFinal.First }, Type = PredictionType.DoesNotReachFinal },
                new() { Points = 2, Country = new Country { SemiFinal = SemiFinal.Second }, Type = PredictionType.DoesNotReachFinal },
                new() { Points = 3, Country = new Country { SemiFinal = SemiFinal.First }, Type = PredictionType.Last3InFinal }
            },
            FinalPredictions = new List<FinalPrediction>()
        };

        _sut.CalculateTotalPoints(new[] { participant });

        Assert.Equal(1, participant.SemiFinal1Points);
    }

    [Fact]
    public void CalculateTotalPoints_ShouldCalculateSemiFinal2Points()
    {
        var participant = new Participant
        {
            SemifinalPredictions = new List<SemifinalPrediction>
            {
                new() { Points = 4, Country = new Country { SemiFinal = SemiFinal.Second }, Type = PredictionType.DoesNotReachFinal },
                new() { Points = 2, Country = new Country { SemiFinal = SemiFinal.First }, Type = PredictionType.DoesNotReachFinal }
            },
            FinalPredictions = new List<FinalPrediction>()
        };

        _sut.CalculateTotalPoints(new[] { participant });

        Assert.Equal(4, participant.SemiFinal2Points);
    }

    [Fact]
    public void CalculateTotalPoints_ShouldCalculateFinalPoints()
    {
        var participant = new Participant
        {
            SemifinalPredictions = new List<SemifinalPrediction>(),
            FinalPredictions = new List<FinalPrediction>
            {
                new() { Points = 3 },
                new() { Points = 2 }
            }
        };

        _sut.CalculateTotalPoints(new[] { participant });

        Assert.Equal(5, participant.FinalPoints);
    }

    [Fact]
    public void CalculateTotalPoints_ShouldHandleMultipleParticipants()
    {
        var participants = new List<Participant>
        {
            new()
            {
                SemifinalPredictions = new List<SemifinalPrediction>
                {
                    new() { Points = 1, Country = new Country { SemiFinal = SemiFinal.First }, Type = PredictionType.DoesNotReachFinal }
                },
                FinalPredictions = new List<FinalPrediction>
                {
                    new() { Points = 2 }
                }
            },
            new()
            {
                SemifinalPredictions = new List<SemifinalPrediction>
                {
                    new() { Points = 3, Country = new Country { SemiFinal = SemiFinal.Second }, Type = PredictionType.DoesNotReachFinal }
                },
                FinalPredictions = new List<FinalPrediction>
                {
                    new() { Points = 4 }
                }
            }
        };

        _sut.CalculateTotalPoints(participants);

        Assert.Equal(3, participants[0].TotalPoints); // 1 + 2
        Assert.Equal(7, participants[1].TotalPoints); // 3 + 4
    }

    #endregion
}
