using Application.Dtos.ProjectDtos;
using Application.Interfaces;
using Domain.SearchParams;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController(IProjectService projectService, ILogger<ProjectsController> logger)
        : ControllerBase
    {
        private readonly IProjectService _projectService =
            projectService ?? throw new ArgumentNullException(nameof(projectService));

        [HttpGet]
        public async Task<IEnumerable<ProjectResponse>> GetAllAsync(ProjectSearchParams projectSearchParams)
        {
            var responses = await _projectService.GetAllAsync(projectSearchParams);
            logger.LogInformation($"Successfully retrieved projects from {nameof(GetAllAsync)} request");
            return responses;
        }

        [HttpGet("{id}")]
        public async Task<ProjectResponse> GetByIdAsync(Guid id)
        {
            var response = await _projectService.GetByIdAsync(id);
            logger.LogInformation($"Successfully retrieved project with id {id} from {nameof(GetByIdAsync)} request");
            return response;
        }

        [HttpPost]
        public async Task<ProjectResponse> CreateProjectAsync(ProjectRequest projectRequest)
        {
            var response = await _projectService.CreateAsync(projectRequest);
            logger.LogInformation($"Successfully created project  from {nameof(CreateProjectAsync)} request");
            return response;
        }

        [HttpPut("{id}")]
        public async Task<ProjectResponse> UpdateProjectAsync(Guid id, ProjectRequest projectRequest)
        {
            var response = await _projectService.UpdateAsync(id, projectRequest);
            logger.LogInformation(
                $"Successfully updated project with id {id} from {nameof(UpdateProjectAsync)} request");
            return response;
        }

        [HttpDelete("{id}")]
        public async Task DeleteProjectAsync(Guid id)
        {
            await _projectService.DeleteAsync(id);
            logger.LogInformation(
                $"Successfully deleted project with id {id} from {nameof(DeleteProjectAsync)} request");
        }
    }
}