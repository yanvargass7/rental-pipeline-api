using RentalPipeline.Application.DTOs;
using RentalPipeline.Application.Events;
using RentalPipeline.Application.Interfaces;
using RentalPipeline.Application.Interfaces.Events;
using RentalPipeline.Application.Interfaces.Repositories;
using RentalPipeline.Application.Interfaces.Services;
using RentalPipeline.Application.Mappings;
using RentalPipeline.Domain.Exceptions;
using static RentalPipeline.Domain.Models.Property;
using static RentalPipeline.Domain.Models.RentalProposal;

namespace RentalPipeline.Application.Services;

public class RentalProposalService(IUnitOfWork unitOfWork,
                                   RentalProposalMapper mapper,
                                   IEventPublisher eventPublisher,
                                   ProposalStatusHistoryMapper historyMapper,
                                   IPropertyRepository propertyRepository,
                                   IRentalProposalRepository proposalRepository,
                                   IProposalStatusHistoryRepository historyRepository)
                                   : IRentalProposalService
{
    public async Task CreateAsync(CreateRentalProposalDto dto)
    {
        await unitOfWork.BeginTransactionAsync();

        var property = await propertyRepository.GetByIdForUpdateAsync(dto.PropertyId);
        if (property is null)
            throw new NotFoundException("Property not found");

        if (property.Status != PropertyStatus.Available)
            throw new BusinessRuleException("Property is already in negotiation");

        property.MarkAsInNegotiation();
        var proposal = mapper.ToModel(dto);
        await proposalRepository.InsertAsync(proposal);

        await unitOfWork.CommitAsync();
    }

    public async Task<IEnumerable<ProposalHistoryResponseDto>> GetHistoryAsync(int proposalId)
    {
        var history = await historyRepository.GetByProposalIdAsync(proposalId);
        return history.Select(historyMapper.ToResponse);
    }

    public async Task UpdateStatusAsync(int proposalId, UpdateProposalStatusDto dto)
    {
        var proposal = await proposalRepository.GetByIdAsync(proposalId);

        if (proposal is null)
            throw new NotFoundException("Proposal not found");

        var previousStatus = proposal.Status;

        proposal.MoveTo(dto.Status);

        var history = historyMapper.ToModel(
            proposal.Id,
            previousStatus,
            dto.Status);

        await historyRepository.InsertAsync(history);
        var property = await propertyRepository.GetByIdAsync(proposal.PropertyId);

        if (property is null)
            throw new NotFoundException("Property not found");

        if (dto.Status == ProposalStatus.Active)
        {
            property.MarkAsRented();
            var contractActivatedEvent = new ContractActivatedEvent
            {
                ProposalId = proposal.Id,
                PropertyId = proposal.PropertyId,
                CustomerId = proposal.CustomerId,
                ActivatedAt = DateTime.UtcNow
            };

            await eventPublisher.PublishAsync(contractActivatedEvent);
        }

        if (dto.Status == ProposalStatus.Rejected ||
            dto.Status == ProposalStatus.Cancelled)
        {
            property.MarkAsAvailable();
        }

        proposalRepository.Update(proposal);
    }
}