using Moq;
using RentalPipeline.Application.DTOs;
using RentalPipeline.Application.Interfaces.Events;
using RentalPipeline.Application.Interfaces.Repositories;
using RentalPipeline.Application.Interfaces;
using RentalPipeline.Application.Mappings;
using RentalPipeline.Application.Services;
using RentalPipeline.Domain.Models;
using RentalPipeline.Application.Events;
using FluentAssertions;

namespace RentalPipeline.Tests.Services
{
    public class RentalProposalServiceTests
    {
        [Fact]
        public async Task Should_Publish_Event_When_Proposal_Becomes_Active()
        {
            // Arrange
            var proposal = new RentalProposal(propertyId: 1, customerId: 1);
            proposal.MoveTo(RentalProposal.ProposalStatus.CreditAnalysis);
            proposal.MoveTo( RentalProposal.ProposalStatus.ContractIssued);
            proposal.MoveTo(RentalProposal.ProposalStatus.Signed);

            var property = new Property("Apartment",
                                        "Nice apartment",
                                        "Main street",
                                        Property.PropertyType.Apartment);

            var proposalRepositoryMock = new Mock<IRentalProposalRepository>();
            var propertyRepositoryMock = new Mock<IPropertyRepository>();
            var historyRepositoryMock = new Mock<IProposalStatusHistoryRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var eventPublisherMock = new Mock<IEventPublisher>();

            proposalRepositoryMock
                .Setup(x => x.GetByIdAsync(proposal.Id))
                .ReturnsAsync(proposal);

            propertyRepositoryMock
                .Setup(x => x.GetByIdAsync(proposal.PropertyId))
                .ReturnsAsync(property);

            var service =
                new RentalProposalService(
                    unitOfWorkMock.Object,
                    new RentalProposalMapper(),
                    eventPublisherMock.Object,
                    new ProposalStatusHistoryMapper(),
                    propertyRepositoryMock.Object,
                    proposalRepositoryMock.Object,
                    historyRepositoryMock.Object);
            var dto = new UpdateProposalStatusDto
            { 
                Status = RentalProposal.ProposalStatus.Active
            };

            // Act
            await service.UpdateStatusAsync(proposal.Id, dto);

            // Assert
            eventPublisherMock.Verify(
                x => x.PublishAsync(
                    It.IsAny<ContractActivatedEvent>()),
                Times.Once);
        }

        [Fact]
        public async Task Should_Mark_Property_As_Rented_When_Proposal_Becomes_Active()
        {
            // Arrange
            var proposal = new RentalProposal(propertyId: 1, customerId: 1);
            proposal.MoveTo(RentalProposal.ProposalStatus.CreditAnalysis);
            proposal.MoveTo( RentalProposal.ProposalStatus.ContractIssued);
            proposal.MoveTo(RentalProposal.ProposalStatus.Signed);

            var property = new Property("Apartment",
                                        "Nice apartment",
                                        "Main street",
                                        Property.PropertyType.Apartment);

            var proposalRepositoryMock = new Mock<IRentalProposalRepository>();
            var propertyRepositoryMock = new Mock<IPropertyRepository>();
            var historyRepositoryMock = new Mock<IProposalStatusHistoryRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var eventPublisherMock = new Mock<IEventPublisher>();

            proposalRepositoryMock
                .Setup(x => x.GetByIdAsync(proposal.Id))
                .ReturnsAsync(proposal);

            propertyRepositoryMock
                .Setup(x => x.GetByIdAsync(proposal.PropertyId))
                .ReturnsAsync(property);

            var service =
                new RentalProposalService(
                    unitOfWorkMock.Object,
                    new RentalProposalMapper(),
                    eventPublisherMock.Object,
                    new ProposalStatusHistoryMapper(),
                    propertyRepositoryMock.Object,
                    proposalRepositoryMock.Object,
                    historyRepositoryMock.Object);
            var dto = new UpdateProposalStatusDto
            {
                Status = RentalProposal.ProposalStatus .Active
            };

            // Act
            await service.UpdateStatusAsync(proposal.Id, dto);

            // Assert
            property.Status.Should().Be(Property.PropertyStatus.Rented);
        }

        [Fact]
        public async Task Should_Mark_Property_As_Available_When_Proposal_Is_Rejected()
        {
            // Arrange
            var proposal = new RentalProposal(propertyId: 1, customerId: 1);

            var property =
                new Property(
                    "Apartment",
                    "Nice apartment",
                    "Main street",
                    Property.PropertyType.Apartment);

            property.MarkAsInNegotiation();
            var proposalRepositoryMock = new Mock<IRentalProposalRepository>();
            var propertyRepositoryMock = new Mock<IPropertyRepository>();
            var historyRepositoryMock = new Mock<IProposalStatusHistoryRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var eventPublisherMock = new Mock<IEventPublisher>();
            proposalRepositoryMock
                .Setup(x => x.GetByIdAsync(proposal.Id))
                .ReturnsAsync(proposal);

            propertyRepositoryMock
                .Setup(x => x.GetByIdAsync(proposal.PropertyId))
                .ReturnsAsync(property);

            var service =
                new RentalProposalService(
                    unitOfWorkMock.Object,
                    new RentalProposalMapper(),
                    eventPublisherMock.Object,
                    new ProposalStatusHistoryMapper(),
                    propertyRepositoryMock.Object,
                    proposalRepositoryMock.Object,
                    historyRepositoryMock.Object);

            var dto = new UpdateProposalStatusDto
            {
                Status = RentalProposal.ProposalStatus.Rejected
            };

            // Act
            await service.UpdateStatusAsync(proposal.Id, dto);

            // Assert
            property.Status.Should().Be(Property.PropertyStatus.Available);
        }

    }
}
