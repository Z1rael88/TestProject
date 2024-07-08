using Domain;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data.DataConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IOptions<ModelValidationOptions> validationOptions)
    : DbContext(options), IApplicationDbContext
{
    public DbSet<ProjectModel> Projects { get; set; }
    public DbSet<TaskModel> Tasks { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new ProjectConfiguration(validationOptions.Value));
        modelBuilder.ApplyConfiguration(new TaskConfiguration(validationOptions.Value));
    }
}