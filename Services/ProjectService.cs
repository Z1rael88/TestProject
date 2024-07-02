using Microsoft.AspNetCore.Mvc;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Services;

public class ProjectService : IProjectService
{
    List<Project> projects = new List<Project>
    {
        new Project { Id = 1, Name = "Project 1", Description = "Description 1", DateAdded = DateTime.Now },
        new Project { Id = 2, Name = "Project 2", Description = "Description 2", DateAdded = DateTime.Now }
    };
    public List<Project> GetAll()
    {
       var allProjects= projects.ToList();
       return allProjects;
    }

    public Project GetById(int id)
    {
        var project = projects.FirstOrDefault(p => p.Id == id);
        return project;
    }

    public Project Create(Project project)
    {
        var newProject = project with { Id = projects.Any() ? projects.Max(p => p.Id) + 1 : 1, DateAdded = DateTime.Now };
        projects.Add(newProject);
        return newProject;
    }

    public int Delete(int id)
    {
        var projectToDelete = projects.FirstOrDefault(p => p.Id == id);
        if (projectToDelete != null)
        {
            projects.Remove(projectToDelete);  
        }
        return id;
    }

    public Project Update(int id, Project project)
    {
        var projectToUpdate = projects.FirstOrDefault(p => p.Id == id);
        project.Name = projectToUpdate.Name;
        project.Description = projectToUpdate.Description;
        project.DateAdded = DateTime.Now;
        return projectToUpdate;
    }
}
    
    