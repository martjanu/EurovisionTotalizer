using FluentAssertions;
using EurovisionTotalizer.Domain.Rankers;

namespace EurovisionTotalizer.Tests;

public class RankingFactoryTests
{
    [Fact]
    public void CreateParticipantRanker_Should_Return_ParticipantRanker_Instance()
    {
        // Arrange
        var factory = new RankingFactory();
        var strategy = factory.CreateRankingStratagy();

        // Act
        var ranker = factory.CreateParticipantRanker(strategy);

        // Assert
        ranker.Should().NotBeNull();
        ranker.Should().BeOfType<ParticipantRanker>();
    }

    [Fact]
    public void CreateRankingStratagy_Should_Return_RankingStrategy_Instance()
    {
        // Arrange
        var factory = new RankingFactory();

        // Act
        var strategy = factory.CreateRankingStratagy();

        // Assert
        strategy.Should().NotBeNull();
        strategy.Should().BeOfType<RankingStrategy>();
    }
}
