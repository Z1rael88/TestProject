using Application.Dtos.TaskDtos;
using Domain.ValidationOptions;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Application.Validators;

public class TaskValidator : AbstractValidator<UpdateTaskRequest>
{
    public TaskValidator(IOptions<TaskValidationOptions> validationOptions)
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(validationOptions.Value.NameMaxLength)
            .WithMessage($"Name cannot exceed {validationOptions.Value.NameMaxLength} characters");
        RuleFor(p => p.Description)
            .NotEmpty().WithMessage("Description is required")
            .MaximumLength(validationOptions.Value.DescriptionMaxLength)
            .WithMessage($"Description cannot exceed {validationOptions.Value.DescriptionMaxLength} characters");
        RuleFor(p => p.Status).NotNull().WithMessage("Status is required");
    }
}