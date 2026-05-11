using Microsoft.AspNetCore.Mvc;
using RentalPipeline.Application.DTOs;
using RentalPipeline.Application.Interfaces.Services;

namespace RentalPipeline.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController (ICustomerService _service) : ControllerBase
{   
    /// <summary>
    /// Creates a new customer.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateCustomerDto dto)
    {
        await _service.CreateAsync(dto);
        return Created();
    }

    /// <summary>
    /// Retrieves a customer by its unique identifier.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await _service.GetByIdAsync(id));
    }
}