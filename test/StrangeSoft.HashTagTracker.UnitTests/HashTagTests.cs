using FluentAssertions;

using StrangeSoft.HashTagTracker.Core;

namespace StrangeSoft.HashTagTracker.UnitTests;

public class HashTagTests
{
    readonly Identity _testIdentity = (Identity)"test";

    [Fact]
    public void HashTagDoesNotModifyValue()
    {
        var result = new HashTag(_testIdentity, "#hashtag");
        result.Value.Should().Be("#hashtag");
    }

    [Fact]
    public void WhenValueDoesNotStartWithHash_ThenThrowsArgumentException()
    {
        this.Invoking(_ => new HashTag(_testIdentity, "hashtag"))
            .Should()
            .Throw<ArgumentException>();
    }

    [Fact]
    public void HashTagThrowsArgumentNullExceptionWhenValueIsNull()
    {
        this.Invoking(_ => new HashTag(_testIdentity, null!))
            .Should()
            .Throw<ArgumentNullException>();
    }

    [Fact]
    public void HashTagThrowsArgumentExceptionWhenValueIsEmpty()
    {
        this.Invoking(_ => new HashTag(_testIdentity, string.Empty))
            .Should()
            .Throw<ArgumentException>();
    }

    [Fact]
    public void HashTagThrowsArgumentExceptionWhenValueIsWhitespace()
    {
        this.Invoking(_ => new HashTag(_testIdentity, " "))
            .Should()
            .Throw<ArgumentException>();
    }
}