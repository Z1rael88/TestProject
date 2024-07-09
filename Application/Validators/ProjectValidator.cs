using Application.Dtos.ProjectDtos;
using Domain.ValidationOptions;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Application.Validators;

public class ProjectValidator : AbstractValidator<ProjectRequest>
{
    public ProjectValidator(IOptions<ProjectValidationOptions> validationOptions)
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(validationOptions.Value.NameMaxLength)
            .WithMessage($"Name cannot exceed {validationOptions.Value.NameMaxLength} characters");
        RuleFor(p => p.Description)
            .NotEmpty().WithMessage("Description is required")
            .MaximumLength(validationOptions.Value.DescriptionMaxLength)
            .WithMessage($"Description cannot exceed {validationOptions.Value.DescriptionMaxLength} characters");
        RuleFor(p => p.StartDate)
            .NotEmpty().WithMessage("Start Date is required")
            .Must(date => date >= DateOnly.FromDateTime(DateTime.Today))
            .WithMessage("Start Date cant be before today");
    }
}