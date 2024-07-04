using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dtos.ProjectDtos;
using WebApplication1.Dtos.SearchParams;
using WebApplication1.Interfaces.ProjectRepositories;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController(IProjectService projectService) : ControllerBase
    {
        private readonly IProjectService _projectService =
            projectService ?? throw new ArgumentNullException(nameof(projectService));

        [HttpGet]
        public async Task<IEnumerable<ProjectResponse>> GetAllAsync([FromQuery] ProjectSearchParams projectSearchParams)
        {
            return await _projectService.GetAllAsync(projectSearchParams);
        }

        [HttpGet("{id}")]
        public async Task<ProjectResponse> GetByIdAsync(Guid id)
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
        public async Task DeleteProjectAsync(Guid id)
        {
            await _projectService.DeleteAsync(id);
        }
    }
}