using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Interfaces;

public interface IProjectService
{
    public List<Project> GetAll();
    public Project GetById(int id);
    public Project Create(Project project);
    public int Delete(int id);
    public Project Update(int id, Project project);
}