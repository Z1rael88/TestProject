using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dto;
using WebApplication1.Dtos;
using WebApplication1.Dtos.ProjectDtos;
using WebApplication1.Interfaces;
using WebApplication1.Interfaces.ProjectRepositories;
using WebApplication1.Interfaces.TaskRepositories;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController(IProjectService projectService, ITaskService taskService) : ControllerBase
    {
        private readonly IProjectService _projectService = projectService ?? throw new ArgumentNullException(nameof(projectService));
        [HttpGet]
        public ICollection<ProjectResponse> GetAll([FromQuery]SearchDto searchDto )
        {
            return _projectService.GetAll(searchDto);
        }
        
        [HttpGet("{id}")]
        public ProjectResponse GetById(Guid id)
        {
            return _projectService.GetById(id);
        }
        
        [HttpPost]
        public ProjectResponse CreateProject(ProjectRequest projectRequest)
        {
            return _projectService.Create(projectRequest);
        }
        
        [HttpPut("{id}")]
        public ProjectResponse UpdateProject(Guid id, ProjectRequest projectRequest)
        {
            return _projectService.Update(id, projectRequest);
        }
        
        [HttpDelete("{id}")]
        public bool DeleteProject(Guid id)
        {
            return _projectService.Delete(id);
        }
    }
}
