using System.ComponentModel.DataAnnotations;
using WebApplication1.Constants;
using WebApplication1.Dtos.TaskDtos;

namespace WebApplication1.Dtos.ProjectDtos;

public record ProjectResponse
{
    public Guid Id { get; set; }
    [Required] [MaxLength(ModelLimits.NameLimit)] public string Name { get; set; } = string.Empty;
    [Required] [MaxLength(ModelLimits.DescriptionLimit)] public string Description { get; set; } = string.Empty;
    [Required] public DateTime StartDate { get; set; }

    public IEnumerable<TaskResponse>? Tasks { get; set; }
}