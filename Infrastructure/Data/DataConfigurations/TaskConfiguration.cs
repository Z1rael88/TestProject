using Domain.Models;
using Domain.ValidationOptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.DataConfigurations;

public class TaskConfiguration(TaskValidationOptions validationOptions) : IEntityTypeConfiguration<TaskModel>
{
    public void Configure(EntityTypeBuilder<TaskModel> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(validationOptions.NameMaxLength);
        builder.Property(t => t.Description)
            .IsRequired()
            .HasMaxLength(validationOptions.DescriptionMaxLength);
        builder.Property(t => t.Status)
            .IsRequired();
    }
}