using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dto;
using WebApplication1.Dtos.ProjectDtos;
using WebApplication1.Interfaces;
using WebApplication1.Interfaces.ProjectRepositories;
using WebApplication1.Interfaces.TaskRepositories;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController(IProjectService projectService, ITaskService taskService) : ControllerBase
    {
        private readonly IProjectService _projectService = projectService ?? throw new ArgumentNullException(nameof(projectService));
        [HttpGet]
        public async Task<ICollection<ProjectResponse>> GetAll()
        {
            return await _projectService.GetAllAsync();
        }
        
        [HttpGet("{id}")]
        public async Task<ProjectResponse> GetById(Guid id)
        {
            return await _projectService.GetByIdAsync(id);
        }
        
        [HttpPost]
        public async Task<ProjectResponse> CreateProject(ProjectRequest projectRequest)
        {
            return await _projectService.CreateAsync(projectRequest);
        }
        
        [HttpPut("{id}")]
        public async Task<ProjectResponse> UpdateProject(Guid id, ProjectRequest projectRequest)
        {
            return await _projectService.UpdateAsync(id, projectRequest);
        }
        
        [HttpDelete("{id}")]
        public async Task<bool> DeleteProject(Guid id)
        {
            return await _projectService.DeleteAsync(id);
        }
    }
}
