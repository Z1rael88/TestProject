using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Data;

public class DatabaseInitializer
{
    private static IServiceProvider _serviceProvider;

    public static void SetServiceProvider(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public static void Initialize()
    {
        using var scope = _serviceProvider.CreateScope();
        var seeder = scope.ServiceProvider.GetRequiredService<IDataSeeder>();
        seeder.SeedData();
    }
}