using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;
using System;
using System.Collections.Generic;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController(ProjectService projectService) : ControllerBase
    {
        private readonly ProjectService _projectService = projectService ?? throw new ArgumentNullException(nameof(projectService));

        [HttpGet]
        public List<Project> GetAll()
        {
            return _projectService.GetAll();
        }

        [HttpGet("{id}")]
        public Project GetById(int id)
        {
            return _projectService.GetById(id);
        }

        [HttpPost]
        public Project CreateProject(Project project)
        {
            return _projectService.Create(project);
        }

        [HttpPut("{id}")]
        public Project UpdateProject(int id, Project project)
        {
            return _projectService.Update(id, project);
        }

        [HttpDelete("{id}")]
        public int DeleteProject(int id)
        {
            return _projectService.Delete(id);
        }
    }
}