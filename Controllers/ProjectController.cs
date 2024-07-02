using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;
using System;
using System.Collections.Generic;
using WebApplication1.Interfaces;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController(IProjectService projectService) : ControllerBase
    {
        private readonly IProjectService _projectService = projectService ?? throw new ArgumentNullException(nameof(projectService));

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
        public Project CreateProject(ProjectDto projectDto)
        {
            return _projectService.Create(projectDto);
        }

        [HttpPut("{id}")]
        public Project UpdateProject(int id, ProjectDto projectDto)
        {
            return _projectService.Update(id, projectDto);
        }

        [HttpDelete("{id}")]
        public int DeleteProject(int id)
        {
            return _projectService.Delete(id);
        }
    }
}