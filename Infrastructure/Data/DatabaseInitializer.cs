using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Data;

public class DatabaseInitializer(IServiceProvider serviceProvider)
{
    public void Initialize()
    {
        using var scope = serviceProvider.CreateScope();
        var seeder = scope.ServiceProvider.GetRequiredService<IDataSeeder>();
        seeder.SeedData();
    }
}