using Domain.Models;
using Domain.ValidationOptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.DataConfigurations;

public class ProjectConfiguration(ProjectValidationOptions validationOptions) : IEntityTypeConfiguration<ProjectModel>
{
    public void Configure(EntityTypeBuilder<ProjectModel> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(validationOptions.NameMaxLength);
        builder.Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(validationOptions.DescriptionMaxLength);
        builder.Property(p => p.StartDate)
            .IsRequired();
        builder.HasMany(p => p.Tasks)
            .WithOne(t => t.Project)
            .HasForeignKey(t => t.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}