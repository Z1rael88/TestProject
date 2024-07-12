using Domain.Interfaces;
using Domain.Models;
using Domain.ValidationOptions;
using Infrastructure.Data.DataConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infrastructure.Data;

public class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options,
    IOptions<ProjectValidationOptions> projectValidationOptions,
    IOptions<TaskValidationOptions> taskValidationOptions)
    : DbContext(options), IApplicationDbContext
{
    public DbSet<ProjectModel> Projects { get; set; }
    public DbSet<TaskModel> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new ProjectConfiguration(projectValidationOptions.Value));
        modelBuilder.ApplyConfiguration(new TaskConfiguration(taskValidationOptions.Value));
    }
    public DbSet<TEntity> GetDbSet<TEntity>() where TEntity : BaseModel
    {
        return base.Set<TEntity>();
    }
}