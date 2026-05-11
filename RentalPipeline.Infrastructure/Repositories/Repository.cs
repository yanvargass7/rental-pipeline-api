using RentalPipeline.Application.Interfaces.Repositories;
using RentalPipeline.Infrastructure.Persistence;

namespace RentalPipeline.Infrastructure.Repositories;

public class Repository<T>( RentalDbContext context) : IRepository<T> where T : class
{
    protected readonly RentalDbContext Context = context;

    public async Task<T?> GetByIdAsync(int id)
    {
        return await Context.Set<T>().FindAsync(id);
    }

    public async Task InsertAsync(T entity)
    {
        await Context.Set<T>().AddAsync(entity);
        await Context.SaveChangesAsync();
    }

    public void Update(T entity)
    {
        Context.Set<T>().Update(entity);
        Context.SaveChanges();
    }
}