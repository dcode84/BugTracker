using BugtrackerAPI.DTOs.UserRole;
using BugtrackerAPI.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BugtrackerAPI.Controllers;

[ApiController]
[Route("api/userroles")]
public class UserRolesController : ControllerBase
{
    private readonly ILogger<UserRolesController> _logger;
    private readonly IUserRoleData _data;

    public UserRolesController(ILogger<UserRolesController> logger, IUserRoleData data)
    {
        _logger = logger;
        _data = data;
    }

    [HttpGet]
    public async Task<ActionResult<UserRoleDto>> GetUserRoleAsync(int userId, int roleId)
    {
        var ur = await _data.GetUserRoleAsync(userId, roleId);
            if (ur == null) return NotFound();

        return Ok(ur);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<UserRoleDto>>> GetUserRolesAsync(int id)
    {
        var userroles = await _data.GetUserRolesAsync(id);

        var userroleslist = userroles.Select(ur => ur.UserRoleAsDto());

        return Ok(userroleslist);
    }

    // POST /api/userroles
    [HttpPost]
    public async Task<ActionResult<UserRoleDto>> CreateUserRoleAsync([FromBody]CreateUserRoleDto createUserRole)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        UserRole ur = new()
        {
            UserId = createUserRole.UserId,
            RoleId = createUserRole.RoleId
        };

        await _data.CreateUserRoleAsync(ur);

        //return Created("api/userroles", ur.UserRoleAsDto());
        return CreatedAtAction(nameof(GetUserRoleAsync), new { userId = ur.UserId, roleId = ur.RoleId }, ur.UserRoleAsDto());
    }

    // DELETE /api/userroles
    [HttpDelete]
    public async Task<ActionResult> DeleteUserRoleAsync(DeleteUserRoleDto deleteUserRole)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var ur = await _data.GetUserRoleAsync(deleteUserRole.UserId, deleteUserRole.RoleId);
            if (ur is null) return NotFound();

        UserRole roleToDelete = new()
        {
            UserId = deleteUserRole.UserId,
            RoleId = deleteUserRole.RoleId
        };

        await _data.DeleteUserRoleAsync(roleToDelete);

        return NoContent();
    }
}
