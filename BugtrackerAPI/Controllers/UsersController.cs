using BugtrackerAPI.DTOs.User;
using Microsoft.AspNetCore.Mvc;
using BugtrackerAPI.Extensions;

namespace BugtrackerAPI.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly IUserData _data;

    public UsersController(ILogger<UsersController> logger, IUserData data)
    {
        _logger = logger;
        _data = data;
    }

    // GET /api/users
    [HttpGet()]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersAsync()
    {
        var users = await _data.GetUsersAsync();

        var userlist = users.Select(user => user.UserAsDto());

        return Ok(userlist);
    }

    // GET /api/users/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUserAsync(int id)
    {
        var user = await _data.GetUserAsync(id);
            if (user == null) return NotFound();

        return Ok(user);
    }

    // GET /api/users/{username}
    [HttpGet("byUsername")]
    public async Task<ActionResult<UserDto>> GetUserByNameAsync(string username)
    {
        var user = await _data.GetUserByNameAsync(username);
            if (user == null) return NotFound();

        return Ok(user);
    }

    // POST /api/users
    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUserAsync([FromBody]CreateUserDto createUser)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        User user = new()
        {
            Username = createUser.Username,
            Email = createUser.Email
        };

        await _data.CreateUserAsync(user);

        return CreatedAtAction(nameof(GetUserByNameAsync), new { username = user.Username }, user.UserAsDto());
    }

    // PUT /api/users/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateUserAsync(UpdateUserDto user)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);

        var existingUser = await _data.GetUserAsync(user.Id);

        if (existingUser is null) return NotFound();

        User updatedUser = existingUser with
        {
            Username = user.Username,
            Email = user.Email
        };

        await _data.UpdateUserAsync(updatedUser);

        return NoContent();
    }

    // DELETE /api/users/{id}
    [HttpDelete]
    public async Task<ActionResult> DeleteUserAsync(int id)
    {
        var user = await _data.GetUserAsync(id);

        if (user is null) return NotFound();

        await _data.DeleteUserAsync(id);

        return NoContent();
    }
}
