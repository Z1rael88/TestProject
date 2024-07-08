using Domain;
using Domain.Models;
using FluentValidation;

namespace Infrastructure.Validators;

public class ProjectValidator : AbstractValidator<ProjectModel>
{
    public ProjectValidator(ModelValidationOptions validationOptions)
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(validationOptions.NameMaxLength)
            .WithMessage($"Name cannot exceed {validationOptions.NameMaxLength} characters");
        RuleFor(p => p.Description)
            .NotEmpty().WithMessage("Description is required")
            .MaximumLength(validationOptions.DescriptionMaxLength)
            .WithMessage($"Name cannot exceed {validationOptions.DescriptionMaxLength} characters");
        RuleFor(p => p.StartDate)
            .NotEmpty().WithMessage("Start Date is required");
    }
}