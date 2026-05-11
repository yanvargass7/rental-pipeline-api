using FluentAssertions;
using RentalPipeline.Domain.Models;
using static RentalPipeline.Domain.Models.RentalProposal;

namespace RentalPipeline.Tests.Domain;

public class RentalProposalTests
{
    [Fact]
    public void Should_Create_Proposal()
    {
        // Arrange & Act
        var proposal = new RentalProposal(propertyId: 1, customerId: 1);

        // Assert
        proposal.PropertyId.Should().Be(1);
        proposal.CustomerId.Should().Be(1);
        proposal.Status.Should().Be(ProposalStatus.New);
        proposal.CreatedAt.Should().NotBe(default);
    }

    [Fact]
    public void Should_Move_From_New_To_CreditAnalysis()
    {
        // Arrange
        var proposal = new RentalProposal(propertyId: 1, customerId: 1);

        // Act
        proposal.MoveTo(
            ProposalStatus.CreditAnalysis);

        // Assert
        proposal.Status.Should().Be(ProposalStatus.CreditAnalysis);
    }

    [Fact]
    public void Should_Throw_Exception_When_Transition_Is_Invalid()
    {
        // Arrange
        var proposal = new RentalProposal(propertyId: 1, customerId: 1);

        // Act
        Action action = () => proposal.MoveTo( ProposalStatus.Signed);

        // Assert
        action.Should()
              .Throw<Exception>()
              .WithMessage("Invalid transition*");
    }
}