using FluentAssertions;
using RentalPipeline.Domain.Models;

namespace RentalPipeline.Tests.Domain;

public class CustomerTests
{
    [Fact]
    public void Should_Create_Customer()
    {
        // Arrange & Act
        var customer = new Customer(
            "John Doe",
            "john@email.com",
            "12345678900",
            "+551199999999");

        // Assert
        customer.Name.Should().Be("John Doe");
        customer.Email.Should().Be("john@email.com");
        customer.DocumentNumber.Should().Be("12345678900");
        customer.PhoneNumber.Should().Be("+551199999999");
    }
}