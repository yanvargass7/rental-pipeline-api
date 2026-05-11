using Microsoft.EntityFrameworkCore;
using RentalPipeline.Application.Interfaces.Repositories;
using RentalPipeline.Domain.Models;
using RentalPipeline.Infrastructure.Persistence;

namespace RentalPipeline.Infrastructure.Repositories;

public class PropertyRepository : Repository<Property>, IPropertyRepository
{
    public PropertyRepository( RentalDbContext context) : base(context)
    {}

    public async Task<Property?> GetByIdForUpdateAsync(int id)
    {
        return await Context.Properties
        .FromSqlInterpolated($@"
            SELECT *
            FROM public.""Properties""
            WHERE ""Id"" = {id}
            FOR UPDATE")
        .FirstOrDefaultAsync();
    }
}