using BugtrackerAPI.DTOs.Project;
using Microsoft.AspNetCore.Mvc;

namespace BugtrackerAPI.Controllers;


[ApiController]
[Route("api/projects")]
public class ProjectsController : ControllerBase
{
    private IProjectData _data;
    private ILogger<ProjectsController> _logger;

    public ProjectsController(ILogger<ProjectsController> logger, IProjectData data)
    {
        _data = data;
        _logger = logger;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectDto>> GetProjectAsync(int id)
    {
        var project = await _data.GetProjectAsync(id);

        return Ok(project);
    }
}
