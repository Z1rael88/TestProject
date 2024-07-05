using Domain.Enums;

namespace Application.Dtos.TaskDtos;

public record TaskResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public Status Status { get; set; }

}