using FluentAssertions;
using Moq;
using RentalPipeline.Application.DTOs;
using RentalPipeline.Application.Interfaces;
using RentalPipeline.Application.Interfaces.Events;
using RentalPipeline.Application.Interfaces.Repositories;
using RentalPipeline.Application.Mappings;
using RentalPipeline.Application.Services;
using RentalPipeline.Domain.Models;
using static RentalPipeline.Domain.Models.RentalProposal;

namespace RentalPipeline.Tests.Domain;

public class ProposalStatusHistoryTests
{
    [Fact]
    public void Should_Create_Status_History()
    {
        // Arrange & Act
        var history = new ProposalStatusHistory(1, ProposalStatus.New, ProposalStatus.Active);

        // Assert
        history.ProposalId.Should().Be(1);
        history.PreviousStatus.Should().Be(ProposalStatus.New);
        history.CurrentStatus.Should().Be(ProposalStatus.Active);
        history.ChangedAt.Should().NotBe(default);
    }

    [Fact]
    public async Task Should_Create_Status_History_When_Status_Changes()
    {
        // Arrange
        var proposal = new RentalProposal(propertyId: 1, customerId: 1);
        var property = new Property("Apartment", "Nice apartment", "Main street", Property.PropertyType.Apartment);
        var proposalRepositoryMock = new Mock<IRentalProposalRepository>();
        var propertyRepositoryMock = new Mock<IPropertyRepository>();
        var historyRepositoryMock = new Mock<IProposalStatusHistoryRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();

        var eventPublisherMock = new Mock<IEventPublisher>();
        proposalRepositoryMock.Setup(x => x.GetByIdAsync(proposal.Id))
                              .ReturnsAsync(proposal);

        propertyRepositoryMock.Setup(x => x.GetByIdAsync(proposal.PropertyId))
                              .ReturnsAsync(property);

        var service = new RentalProposalService(
                      unitOfWorkMock.Object,
                      new RentalProposalMapper(),
                      eventPublisherMock.Object,
                      new ProposalStatusHistoryMapper(),
                      propertyRepositoryMock.Object,
                      proposalRepositoryMock.Object,
                      historyRepositoryMock.Object);

        var dto = new UpdateProposalStatusDto
        {
            Status = RentalProposal.ProposalStatus.CreditAnalysis
        };

        // Act
        await service.UpdateStatusAsync(proposal.Id, dto);

        // Assert
        historyRepositoryMock.Verify(
            x => x.InsertAsync(
                It.Is<ProposalStatusHistory>(h =>
                    h.ProposalId == proposal.Id &&
                    h.PreviousStatus == RentalProposal.ProposalStatus.New &&
                    h.CurrentStatus == RentalProposal.ProposalStatus.CreditAnalysis)),
                Times.Once);
    }
}