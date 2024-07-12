using Application.Dtos.TaskDtos;
using Domain.ValidationOptions;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Application.Validators;

public class CreateTaskValidator : AbstractValidator<CreateTaskRequest>
{
    public CreateTaskValidator(IOptions<TaskValidationOptions> validationOptions)
    {
        Include(new TaskValidator(validationOptions));
        RuleFor(p => p.ProjectId)
            .NotEmpty().WithMessage("Task must have Project Id")
            .Must(id => Guid.TryParse(id.ToString(), out _))
            .WithMessage("Project Id must be a valid GUID");
    }
}