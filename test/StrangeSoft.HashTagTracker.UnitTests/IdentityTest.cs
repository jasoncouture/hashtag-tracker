using FluentAssertions;

using StrangeSoft.HashTagTracker.Core;

namespace StrangeSoft.HashTagTracker.UnitTests;

public class IdentityTest
{
    [Fact]
    public void WhenStringIsNull_ThenThrowsArgumentNullException()
    {
        this.Invoking(_ => (Identity)null!)
            .Should()
            .Throw<ArgumentNullException>();
    }

    [Fact]
    public void WhenStringIsEmpty_ThenThrowsArgumentException()
    {
        this.Invoking(_ => (Identity)string.Empty)
            .Should()
            .Throw<ArgumentException>();
    }

    [Fact]
    public void WhenStringIsWhitespace_ThenThrowsArgumentException()
    {
        this.Invoking(_ => (Identity)" ")
            .Should()
            .Throw<ArgumentException>();
    }

    [Fact]
    public void WhenStringIsTooLong_ThenThrowsArgumentException()
    {
        this.Invoking(_ => (Identity)new string('a', Identity.MaxIdLength + 1))
            .Should()
            .Throw<ArgumentException>();
    }

    [Fact]
    public void WhenStringIsMaxLength_ThenDoesNotThrow()
    {
        this.Invoking(_ => (Identity)new string('a', Identity.MaxIdLength))
            .Should()
            .NotThrow();
    }

    [Fact]
    public void IdentityToStringMatchesIdentityConvertedToString()
    {
        const string expected = "testString";
        var identity = (Identity)expected;
        var actual = identity.ToString();
        actual.Should().Be(expected);
    }

    [Fact]
    public void IdentityCastToStringMatchesIdentityConvertedToString()
    {
        const string expected = "testString";
        var identity = (Identity)expected;
        var actual = (string)identity;
        actual.Should().Be(expected);
    }
}