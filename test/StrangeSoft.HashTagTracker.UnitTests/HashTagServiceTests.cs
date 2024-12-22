using System.ComponentModel;

using FluentAssertions;

using StrangeSoft.HashTagTracker.Core;

namespace StrangeSoft.HashTagTracker.UnitTests;

public class HashTagServiceTests
{
    [Fact]
    public void HashTagServiceReturnsSameObjectWhenProvidedSameObjectMultipleTimes()
    {
        var testObject = new object();
        var firstInstance = HashTagService.For(testObject);
        var secondInstance = HashTagService.For(testObject);
        firstInstance.Should().NotBeNull();
        firstInstance.Should().BeSameAs(secondInstance);
    }

    [Fact]
    public void HashTagServiceReturnsSameObjectWhenProvidedSameTypeMultipleTimes()
    {
        var testType = typeof(string);

        var firstInstance = HashTagService.For(testType);
        var secondInstance = HashTagService.For(testType);

        firstInstance.Should().NotBeNull();
        firstInstance.Should().BeSameAs(secondInstance);
    }

    [Fact]
    public void HashTagServiceReturnsSameObjectWhenProvidedSameTypeAsGenericArgumentMultipleTimes()
    {
        var firstInstance = HashTagService.For<object>();
        var secondInstance = HashTagService.For<object>();

        firstInstance.Should().NotBeNull();
        firstInstance.Should().BeSameAs(secondInstance);
    }

    [Fact]
    public void HashTagServiceReturnsSameObjectWhenTypeIsSameAcrossMethods()
    {
        var obj = new object();
        var type = obj.GetType();

        var firstInstance = HashTagService.For(obj);
        var secondInstance = HashTagService.For(type);
        var thirdInstance = HashTagService.For<object>();

        firstInstance.Should().NotBeNull();
        firstInstance.Should().BeSameAs(secondInstance);
        firstInstance.Should().BeSameAs(thirdInstance);
    }

    [Fact]
    public void HashTagServiceRespectsDisplayNameAttributeUsingGenericArgument()
    {
        var instance = HashTagService.For<TestClass>();
        instance.Should().NotBeNull();
        instance.Name.Should().Be(ExpectedDisplayName);
    }

    [Fact]
    public void HashTagServiceRespectsDisplayNameAttributeUsingType()
    {
        var instance = HashTagService.For(typeof(TestClass));
        instance.Should().NotBeNull();
        instance.Name.Should().Be(ExpectedDisplayName);
    }

    [Fact]
    public void HashTagServiceRespectsDisplayNameAttributeUsingObject()
    {
        var instance = HashTagService.For(new TestClass());
        instance.Should().NotBeNull();
        instance.Name.Should().Be(ExpectedDisplayName);
    }

    [Fact]
    public void HashTagServiceThrowsArgumentNullExceptionWhenObjectIsNull()
    {
        this.Invoking(static _ => HashTagService.For((object)null!))
            .Should()
            .Throw<ArgumentNullException>();
    }

    [Fact]
    public void HashTagServiceThrowsArgumentNullExceptionWhenTypeIsNull()
    {
        this.Invoking(static _ => HashTagService.For((Type)null!))
            .Should()
            .Throw<ArgumentNullException>();
    }

    private const string ExpectedDisplayName = "Test Name";

    [DisplayName(ExpectedDisplayName)]
    private class TestClass;
}