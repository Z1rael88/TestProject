using Domain.Interfaces;
using Domain.Models;

namespace Infrastructure.Data;

public class DataSeeder(IApplicationDbContext dbContext) : IDataSeeder
{
    public void SeedData()
    {
        if (!dbContext.Projects.Any())
        {
            dbContext.Projects.AddRange(
                new ProjectModel { Name = "Project 1", Description = "First project", StartDate = DateOnly.FromDateTime(DateTime.Today).AddMonths(3)},
                new ProjectModel { Name = "Project 2", Description = "Second project", StartDate = DateOnly.FromDateTime(DateTime.Today).AddDays(7) },
                new ProjectModel { Name = "Project 3", Description = "Third project", StartDate = DateOnly.FromDateTime(DateTime.Today) });
        }

        dbContext.SaveChangesAsync();
    }
}