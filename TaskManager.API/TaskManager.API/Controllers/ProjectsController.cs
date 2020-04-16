using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.API.Data;
using TaskManager.API.Models;

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly DataContext _context;

        public ProjectsController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
            var projects = await _context.Projects.ToListAsync();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProject(int id)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(x => x.ProjectID == id);
            return Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> PostProject([FromBody]Project project)
        {
             _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return Ok(project);
        }

        [HttpPut]
        public async Task<IActionResult> EditProject([FromBody]Project project)
        {
            Project existingProject = await _context.Projects.Where(x => x.ProjectID == project.ProjectID).FirstOrDefaultAsync();
            if (existingProject != null)
            {
                existingProject.ProjectName = project.ProjectName;
                existingProject.DateOfStart = project.DateOfStart;
                existingProject.TeamSize = project.TeamSize;
                await _context.SaveChangesAsync();
                return Ok(existingProject);
            }
            else
            {
                return null;
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProject(int ProjectID)
        {
            Project existingProject = await _context.Projects.Where(x => x.ProjectID == ProjectID).FirstOrDefaultAsync();
            if (existingProject != null)
            {
                _context.Projects.Remove(existingProject);
                await _context.SaveChangesAsync();
                return Ok(ProjectID);
            }
            else
            {
                return Ok(-1);
            }
        }

    }
}