using FluentAssertions;
using EurovisionTotalizer.Domain.Rankers;
using EurovisionTotalizer.Domain.Models;

namespace EurovisionTotalizer.Tests;

public class RankingStrategyTests
{
    [Fact]
    public void Rank_Should_Throw_When_Participants_Is_Null()
    {
        // Arrange
        var strategy = new RankingStrategy();

        // Act
        Action act = () => strategy.Rank(null!);

        // Assert
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("*participants*");
    }

    [Fact]
    public void Rank_Should_Return_Empty_List_When_No_Participants()
    {
        // Arrange
        var strategy = new RankingStrategy();
        var participants = new List<Participant>();

        // Act
        var result = strategy.Rank(participants);

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void Rank_Should_Order_By_TotalPoints_Descending()
    {
        // Arrange
        var strategy = new RankingStrategy();

        var participants = new List<Participant>
        {
            new Participant { Name = "A", TotalPoints = 10 },
            new Participant { Name = "B", TotalPoints = 20 },
            new Participant { Name = "C", TotalPoints = 15 }
        };

        // Act
        var result = strategy.Rank(participants).ToList();

        // Assert
        result[0].Name.Should().Be("B");
        result[1].Name.Should().Be("C");
        result[2].Name.Should().Be("A");
    }

    [Fact]
    public void Rank_Should_ThenBy_FinalPoints_When_TotalPoints_AreEqual()
    {
        // Arrange
        var strategy = new RankingStrategy();

        var participants = new List<Participant>
        {
            new Participant { Name = "A", TotalPoints = 10, FinalPoints = 5 },
            new Participant { Name = "B", TotalPoints = 10, FinalPoints = 15 },
            new Participant { Name = "C", TotalPoints = 10, FinalPoints = 10 }
        };

        // Act
        var result = strategy.Rank(participants).ToList();

        // Assert
        result[0].Name.Should().Be("B"); // Highest FinalPoints
        result[1].Name.Should().Be("C");
        result[2].Name.Should().Be("A");
    }

    [Fact]
    public void Rank_Should_ThenBy_SemiFinalPoints_When_TotalPoints_And_FinalPoints_AreEqual()
    {
        // Arrange
        var strategy = new RankingStrategy();

        var participants = new List<Participant>
        {
            new Participant { Name = "A", TotalPoints = 10, FinalPoints = 5, SemiFinal1Points = 8, SemiFinal2Points = 2 },
            new Participant { Name = "B", TotalPoints = 10, FinalPoints = 5, SemiFinal1Points = 5, SemiFinal2Points = 5 },
            new Participant { Name = "C", TotalPoints = 10, FinalPoints = 5, SemiFinal1Points = 6, SemiFinal2Points = 4 }
        };

        // Act
        var result = strategy.Rank(participants).ToList();

        // Assert
        result[0].Name.Should().Be("A"); // 8+2=10
        result[1].Name.Should().Be("C"); // 6+4=10 but less SemiFinal1Points
        result[2].Name.Should().Be("B"); // 5+5=10 but less SemiFinal1Points
    }

    [Fact]
    public void Rank_Should_ThenBy_SemiFinal1Points_When_All_Other_AreEqual()
    {
        // Arrange
        var strategy = new RankingStrategy();

        var participants = new List<Participant>
        {
            new Participant { Name = "A", TotalPoints = 10, FinalPoints = 5, SemiFinal1Points = 8, SemiFinal2Points = 2 },
            new Participant { Name = "B", TotalPoints = 10, FinalPoints = 5, SemiFinal1Points = 9, SemiFinal2Points = 1 }
        };

        // Act
        var result = strategy.Rank(participants).ToList();

        // Assert
        result[0].Name.Should().Be("B"); // Higher SemiFinal1Points
        result[1].Name.Should().Be("A");
    }
}
