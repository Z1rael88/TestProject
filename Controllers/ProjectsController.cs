using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dto;
using WebApplication1.Dtos;
using WebApplication1.Dtos.ProjectDtos;
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
        public async Task<IEnumerable<ProjectResponse>> GetAllAsync([FromQuery]SearchDto searchDto )
        {
            return await _projectService.GetAllAsync(searchDto);
        }
        
        [HttpGet("{id}")]
        public async  Task<ProjectResponse> GetByIdAsync(Guid id)
        {
            return await _projectService.GetByIdAsync(id);
        }
        
        [HttpPost]
        public async Task<ProjectResponse> CreateProjectAsync(ProjectRequest projectRequest)
        {
            return await _projectService.CreateAsync(projectRequest);
        }
        
        [HttpPut("{id}")]
        public async Task<ProjectResponse> UpdateProjectAsync(Guid id, ProjectRequest projectRequest)
        {
            return await _projectService.UpdateAsync(id, projectRequest);
        }
        
        [HttpDelete("{id}")]
        public async Task<bool> DeleteProjectAsync(Guid id)
        {
            return await _projectService.DeleteAsync(id);
        }
    }
}
