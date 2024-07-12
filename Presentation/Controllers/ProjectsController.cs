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
        public async Task<IEnumerable<ProjectResponse>> GetAllAsync([FromQuery] ProjectSearchParams projectSearchParams)
        {
            logger.LogInformation($"Started retrieving projects from {nameof(GetAllAsync)} request");
            var responses = await _projectService.GetAllAsync(projectSearchParams);
            logger.LogInformation($"Successfully retrieved projects from {nameof(GetAllAsync)} request");
            return responses;
        }

        [HttpGet("{id}")]
        public async Task<ProjectResponse> GetByIdAsync(Guid id)
        {
            logger.LogInformation($"Started retrieving project with Id : {id} from {nameof(GetAllAsync)} request");
            var response = await _projectService.GetByIdAsync(id);
            logger.LogInformation($"Successfully retrieved project with Id : {id} from {nameof(GetByIdAsync)} request");
            return response;
        }

        [HttpPost]
        public async Task<ProjectResponse> CreateProjectAsync(ProjectRequest projectRequest)
        {
            logger.LogInformation($"Started creating project from {nameof(GetAllAsync)} request");
            var response = await _projectService.CreateAsync(projectRequest);
            logger.LogInformation(
                $"Successfully created project with Id : {response.Id} from {nameof(CreateProjectAsync)} request");
            return response;
        }

        [HttpPut("{id}")]
        public async Task<ProjectResponse> UpdateProjectAsync(Guid id, ProjectRequest projectRequest)
        {
            logger.LogInformation($"Started updating project with Id : {id} from {nameof(GetAllAsync)} request");
            var response = await _projectService.UpdateAsync(id, projectRequest);
            logger.LogInformation(
                $"Successfully updated project with Id : {id} from {nameof(UpdateProjectAsync)} request");
            return response;
        }

        [HttpDelete("{id}")]
        public async Task DeleteProjectAsync(Guid id)
        {
            logger.LogInformation($"Started deleting project with Id : {id} from {nameof(GetAllAsync)} request");
            await _projectService.DeleteAsync(id);
            logger.LogInformation(
                $"Successfully deleted project with Id : {id} from {nameof(DeleteProjectAsync)} request");
        }
    }
}