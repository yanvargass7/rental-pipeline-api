using RentalPipeline.Domain.Models;

namespace RentalPipeline.Application.Interfaces.Repositories;

public interface IPropertyRepository : IRepository<Property>
{
    Task<Property?> GetByIdForUpdateAsync(int id);
}