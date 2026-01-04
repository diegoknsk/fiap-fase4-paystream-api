using FluentAssertions;
using FastFood.PayStream.Domain.Common.Enums;

namespace FastFood.PayStream.Tests.Unit.Domain.Common.Enums;

public class EnumPaymentStatusTests
{
    [Fact]
    public void NotStarted_ShouldHaveValueZero()
    {
        // Act & Assert
        ((int)EnumPaymentStatus.NotStarted).Should().Be(0);
    }

    [Fact]
    public void Started_ShouldHaveValueOne()
    {
        // Act & Assert
        ((int)EnumPaymentStatus.Started).Should().Be(1);
    }

    [Fact]
    public void QrCodeGenerated_ShouldHaveValueTwo()
    {
        // Act & Assert
        ((int)EnumPaymentStatus.QrCodeGenerated).Should().Be(2);
    }

    [Fact]
    public void Approved_ShouldHaveValueThree()
    {
        // Act & Assert
        ((int)EnumPaymentStatus.Approved).Should().Be(3);
    }

    [Fact]
    public void Rejected_ShouldHaveValueFour()
    {
        // Act & Assert
        ((int)EnumPaymentStatus.Rejected).Should().Be(4);
    }

    [Fact]
    public void Canceled_ShouldHaveValueFive()
    {
        // Act & Assert
        ((int)EnumPaymentStatus.Canceled).Should().Be(5);
    }

    [Fact]
    public void Enum_ShouldHaveAllExpectedValues()
    {
        // Act
        var values = Enum.GetValues<EnumPaymentStatus>();

        // Assert
        values.Should().HaveCount(6);
        values.Should().Contain(EnumPaymentStatus.NotStarted);
        values.Should().Contain(EnumPaymentStatus.Started);
        values.Should().Contain(EnumPaymentStatus.QrCodeGenerated);
        values.Should().Contain(EnumPaymentStatus.Approved);
        values.Should().Contain(EnumPaymentStatus.Rejected);
        values.Should().Contain(EnumPaymentStatus.Canceled);
    }

    [Fact]
    public void Enum_CanBeParsedFromInt()
    {
        // Act
        var status = (EnumPaymentStatus)3;

        // Assert
        status.Should().Be(EnumPaymentStatus.Approved);
    }
}
