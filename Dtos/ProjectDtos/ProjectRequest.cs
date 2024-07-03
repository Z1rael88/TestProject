using System.ComponentModel.DataAnnotations;
using WebApplication1.Models;

namespace WebApplication1.Dto;

public record ProjectRequest
{
    public string Name { get; set; }= String.Empty;
    [Required]
    [MaxLength(50)]
    public string Description { get; set; }= String.Empty;
    [Required]
    public DateTime StartDate { get; set; }

    
}