using System.ComponentModel.DataAnnotations;
using WebApplication1.Dto;

namespace WebApplication1.Dtos.ProjectDtos;

public record ProjectResponse
{
    public Guid Id { get; set; }
    [Required]
    [MaxLength(12)]
    public string Name { get; set; }= String.Empty;
    [Required]
    [MaxLength(50)]
    public string Description { get; set; }= String.Empty;
    [Required]
    public DateTime StartDate { get; set; }

    public ICollection<TaskResponse> Tasks { get; set; } 

}