using System.ComponentModel.DataAnnotations;
using WebApplication1.Models;

namespace WebApplication1.Dto;

public record TaskResponse
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    [MaxLength(12)]
    public string Title { get; set; }= String.Empty;
    [Required]
    [MaxLength(50)]
    public string Description { get; set; } = String.Empty;
    [Required]
    public Status Status { get; set; }
}