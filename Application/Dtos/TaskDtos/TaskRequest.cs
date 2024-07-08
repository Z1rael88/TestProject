using System.ComponentModel.DataAnnotations;
using Domain.Constants;
using Domain.Enums;

namespace Application.Dtos.TaskDtos;

public class TaskRequest
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public Status Status { get; set; }
}