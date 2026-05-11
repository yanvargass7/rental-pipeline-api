using Microsoft.AspNetCore.Mvc;
using RentalPipeline.Application.DTOs;
using RentalPipeline.Application.Interfaces.Services;

namespace RentalPipeline.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProposalsController(IRentalProposalService service) : ControllerBase
{
    /// <summary>
    /// Creates a new rental proposal for a property.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType( typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType( typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create(
        [FromBody] CreateRentalProposalDto dto)
    {
        await service.CreateAsync(dto);
        return Created();
    }

    /// <summary>
    /// Updates the workflow status of a rental proposal.
    /// Allowed transitions:
    /// New -> "CreditAnalysis"
    /// CreditAnalysis -> "Approved" or "Rejected"
    /// Approved -> "ContractIssued"
    /// ContractIssued -> "Signed"
    /// Signed -> "Active"
    /// </summary>
    [HttpPatch("{id}/status")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateStatus(int id, UpdateProposalStatusDto dto)
    {
        await service.UpdateStatusAsync(id, dto);
        return NoContent();
    }

    /// <summary>
    /// Retrieves the status transition history of a rental proposal.
    /// </summary>
    [HttpGet("{id}/history")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetHistory(int id)
    {
        return Ok(await service.GetHistoryAsync(id));
    }
}