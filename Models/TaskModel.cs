using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace WebApplication1.Models;

public record TaskModel : BaseModel
{
    [Required]
    [MaxLength(12)]
    public string Title { get; set; }= String.Empty;
    [Required]
    [MaxLength(25)]
    public string Description { get; set; } = String.Empty;
    [Required]
    [JsonConverter(typeof(StringEnumConverter))]
    public Status Status { get; set; }
    public Guid ProjectId { get; set; }//TODO:PROJECT BY IT SELF
}

public enum Status
{
    Completed,
    Started,
    InProgress
}