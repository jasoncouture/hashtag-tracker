using FluentAssertions;

using StrangeSoft.HashTagTracker.Core;

namespace StrangeSoft.HashTagTracker.UnitTests;

public class HashTagServiceUserTest
{
    readonly Identity _testIdentity = (Identity)"test";

    [Fact]
    public void HashTagUserIdMatchesInput()
    {
        var user = new HashTagServiceUser(_testIdentity);
        user.Id.Should().Be(_testIdentity);
    }


    [Fact]
    public void HashTagUserReplacesEmptyNameWithNull()
    {
        var user = new HashTagServiceUser(_testIdentity, string.Empty);
        user.Name.Should().BeNull();
    }

    [Fact]
    public void HashTagUserReplacesWhitespaceNameWithNull()
    {
        var user = new HashTagServiceUser(_testIdentity, " ");
        user.Name.Should().BeNull();
    }

    [Fact]
    public void HashTagUserTrimsWhitespaceFromName()
    {
        var user = new HashTagServiceUser(_testIdentity, " abc ");
        user.Name.Should().Be("abc");
    }

    [Fact]
    public void HashTagUserThrowsArgumentExceptionWhenNameIsTooLong()
    {
        FluentActions.Invoking(() =>
                new HashTagServiceUser(_testIdentity, new string('a', HashTagServiceUser.MaxNameLength + 1)))
            .Should()
            .Throw<ArgumentException>();
    }

    [Fact]
    public void HashTagUserAcceptsMaxLengthName()
    {
        FluentActions.Invoking(
                () => new HashTagServiceUser(
                    _testIdentity,
                    new string('a', HashTagServiceUser.MaxNameLength))
            )
            .Should()
            .NotThrow();
    }
}