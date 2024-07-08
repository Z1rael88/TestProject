using System.ComponentModel.DataAnnotations;
using Domain.Constants;

namespace Application.Dtos.ProjectDtos;

public class ProjectRequest
{
   
    public string Name { get; set; } = string.Empty;

    
    public string Description { get; set; } = string.Empty;

     public DateOnly StartDate { get; set; }
}