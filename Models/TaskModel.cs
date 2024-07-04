using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace WebApplication1.Models;

public class TaskModel : BaseModel
{
    [Required]
    [MaxLength(12)]
    public string Title { get; set; }= String.Empty;
    [Required]
    [MaxLength(25)]
    public string Description { get; set; } = String.Empty;
    [Required]
    public Status Status { get; set; }
    public Guid ProjectId { get; set; }
}

public enum Status
{
    Completed,
    Started,
    InProgress
}