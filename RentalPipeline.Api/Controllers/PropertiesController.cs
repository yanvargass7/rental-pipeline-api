using Microsoft.AspNetCore.Mvc;
using RentalPipeline.Application.DTOs;
using RentalPipeline.Application.Interfaces.Services;

namespace RentalPipeline.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertiesController(IPropertyService service) : ControllerBase
{
    /// <summary>
    /// Creates a new property available for rental negotiation.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse),
    StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreatePropertyDto dto)
    {
        await service.CreateAsync(dto);
        return Created();
    }

    /// <summary>
    /// Retrieves a property by its unique identifier.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await service.GetByIdAsync(id));
    }
}