using Application.Dtos.ProjectDtos;
using Application.Interfaces;
using Domain.SearchParams;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController(IProjectService projectService, IValidator<ProjectRequest> validator)
        : ControllerBase
    {
        private readonly IProjectService _projectService =
            projectService ?? throw new ArgumentNullException(nameof(projectService));

        [HttpGet]
        public async Task<IEnumerable<ProjectResponse>> GetAllAsync(ProjectSearchParams projectSearchParams)
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
            await validator.ValidateAndThrowAsync(projectRequest);
            return await _projectService.CreateAsync(projectRequest);
        }

        [HttpPut("{id}")]
        public async Task<ProjectResponse> UpdateProjectAsync(Guid id, ProjectRequest projectRequest)
        {
            await validator.ValidateAndThrowAsync(projectRequest);
            return await _projectService.UpdateAsync(id, projectRequest);
        }

        [HttpDelete("{id}")]
        public async Task DeleteProjectAsync(Guid id)
        {
            await _projectService.DeleteAsync(id);
        }
    }
}