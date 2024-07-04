using System.ComponentModel.DataAnnotations;
using WebApplication1.Models;

namespace WebApplication1.Dtos.ProjectDtos;

public record ProjectDto
{
    [Required] [MaxLength(12)] public string Name { get; set; } = string.Empty;
    [Required] [MaxLength(50)] public string Description { get; set; } = string.Empty;

    public List<TaskModel>? Tasks { get; set; }
}