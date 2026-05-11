using FluentAssertions;
using RentalPipeline.Domain.Models;

namespace RentalPipeline.Tests.Domain;

public class PropertyTests
{
    [Fact]
    public void Should_Create_Property()
    {
        // Arrange & Act
        var property = new Property("Modern Apartment",
                                    "Nice apartment",
                                    "Main street",
                                    Property.PropertyType.Apartment);
        // Assert
        property.Title.Should().Be("Modern Apartment");
        property.Description.Should().Be("Nice apartment");
        property.Address.Should().Be("Main street");
        property.Type.Should().Be(Property.PropertyType.Apartment);
        property.Status.Should().Be(Property.PropertyStatus.Available);
        property.CreatedAt.Should().NotBe(default);
    }

    [Fact]
    public void Should_Mark_Property_As_InNegotiation()
    {
        // Arrange
        var property = new Property("Apartment",
                                    "Nice apartment",
                                    "Main street",
                                    Property.PropertyType.Apartment);
        // Act
        property.MarkAsInNegotiation();

        // Assert
        property.Status.Should().Be(Property.PropertyStatus.InNegotiation);
    }

    [Fact]
    public void Should_Mark_Property_As_Rented()
    {
        // Arrange
        var property = new Property("Apartment",
                                    "Nice apartment",
                                    "Main street",
                                    Property.PropertyType.Apartment);
        // Act
        property.MarkAsRented();

        // Assert
        property.Status.Should().Be(Property.PropertyStatus.Rented);
    }

    [Fact]
    public void Should_Not_Allow_InNegotiation_When_Property_Is_Not_Available()
    {
        // Arrange
        var property = new Property("Apartment",
                                    "Nice apartment",
                                    "Main street",
                                    Property.PropertyType.Apartment);
        property.MarkAsInNegotiation();

        // Act
        Action action = () => property.MarkAsInNegotiation();

        // Assert
        action.Should()
              .Throw<Exception>()
              .WithMessage("Property is not available");
    }

    [Fact]
    public void Should_Mark_Property_As_Available()
    {
        // Arrange
        var property = new Property("Apartment",
                                    "Nice apartment",
                                    "Main street",
                                    Property.PropertyType.Apartment);
        property.MarkAsInNegotiation();

        // Act
        property.MarkAsAvailable();

        // Assert
        property.Status.Should().Be(Property.PropertyStatus.Available);
    }
}