using System.ComponentModel.DataAnnotations;
using WebApplication1.Enums;

namespace WebApplication1.Dtos.TaskDtos;

public record TaskRequest
{
    [Required] [MaxLength(12)] public string Title { get; set; } = string.Empty;
    [Required] [MaxLength(50)] public string Description { get; set; } = string.Empty;
    [Required] public Status Status { get; set; }
}