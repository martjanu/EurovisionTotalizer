using Moq;
using FluentAssertions;
using EurovisionTotalizer.Domain.Rankers;
using EurovisionTotalizer.Domain.Models;

namespace EurovisionTotalizer.Tests;

public class ParticipantRankerTests
{
    [Fact]
    public void Constructor_Should_Throw_When_RankingStrategy_Is_Null()
    {
        // Act
        Action act = () => new ParticipantRanker(null!);

        // Assert
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("*rankingStrategy*");
    }

    [Fact]
    public void GetRankedParticipants_Should_Throw_When_Participants_Is_Null()
    {
        // Arrange
        var strategyMock = new Mock<IRankingStrategy>();
        var ranker = new ParticipantRanker(strategyMock.Object);

        // Act
        Action act = () => ranker.GetRankedParticipants(null!);

        // Assert
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("*participants*");
    }

    [Fact]
    public void GetRankedParticipants_Should_Call_Rank_On_Strategy()
    {
        // Arrange
        var strategyMock = new Mock<IRankingStrategy>();
        var participants = new List<Participant>
        {
            new Participant { Name = "A" },
            new Participant { Name = "B" }
        };

        strategyMock.Setup(s => s.Rank(It.IsAny<IEnumerable<Participant>>()))
            .Returns((IEnumerable<Participant> p) => p.OrderBy(p => p.Name));

        var ranker = new ParticipantRanker(strategyMock.Object);

        // Act
        var result = ranker.GetRankedParticipants(participants);

        // Assert
        strategyMock.Verify(s => s.Rank(It.Is<IEnumerable<Participant>>(p => p.SequenceEqual(participants))), Times.Once);

        result.Should().NotBeNull();
        result.Should().BeInAscendingOrder(p => p.Name);
    }

    [Fact]
    public void GetRankedParticipants_Should_Return_Ranked_List()
    {
        // Arrange
        var strategyMock = new Mock<IRankingStrategy>();
        var participants = new List<Participant>
        {
            new Participant { Name = "C" },
            new Participant { Name = "A" },
            new Participant { Name = "B" }
        };

        strategyMock.Setup(s => s.Rank(It.IsAny<IEnumerable<Participant>>()))
            .Returns((IEnumerable<Participant> p) => p.OrderBy(p => p.Name));

        var ranker = new ParticipantRanker(strategyMock.Object);

        // Act
        var result = ranker.GetRankedParticipants(participants).ToList();

        // Assert
        result.Count.Should().Be(3);
        result[0].Name.Should().Be("A");
        result[1].Name.Should().Be("B");
        result[2].Name.Should().Be("C");
    }
}
