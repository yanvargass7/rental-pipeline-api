using RentalPipeline.Application.Interfaces.Repositories;
using RentalPipeline.Domain.Models;
using RentalPipeline.Infrastructure.Persistence;

namespace RentalPipeline.Infrastructure.Repositories;

public class CustomerRepository: Repository<Customer>,ICustomerRepository
{
    public CustomerRepository(RentalDbContext context) : base(context)
    {
    }
}