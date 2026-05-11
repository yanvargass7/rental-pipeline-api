using Microsoft.EntityFrameworkCore.Storage;
using RentalPipeline.Application.Interfaces;

namespace RentalPipeline.Infrastructure.Persistence;

public class UnitOfWork(RentalDbContext context): IUnitOfWork
{
    private IDbContextTransaction? transaction;

    public async Task BeginTransactionAsync()
    {
        transaction = await context.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        if (transaction is not null)
            await transaction.CommitAsync();
    }
}