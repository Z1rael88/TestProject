using Application.Dtos.TaskDtos;
using FluentValidation;

namespace Application.Validators;

public class CreateTaskValidator : AbstractValidator<CreateTaskRequest>
{
    public CreateTaskValidator()
    {
        RuleFor(p => p.ProjectId)
            .NotEmpty().WithMessage("Task must have Project Id");
    }
}