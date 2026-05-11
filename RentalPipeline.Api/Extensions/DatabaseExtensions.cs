using Microsoft.EntityFrameworkCore;
using RentalPipeline.Infrastructure.Persistence;

namespace RentalPipeline.API.Extensions;

public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<RentalDbContext>(options =>
        {
            options.UseNpgsql(
                configuration.GetConnectionString(
                    "DefaultConnection"));
        });

        return services;
    }
}