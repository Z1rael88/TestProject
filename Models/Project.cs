using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public record Project
{
    public int Id { get; set; }
    [Required]
    [MaxLength(12)]
    public string Name { get; set; }= String.Empty;
    [Required]
    [MaxLength(50)]
    public string Description { get; set; }= String.Empty;
    [Required]
    public DateTime StartDate { get; set; }
}