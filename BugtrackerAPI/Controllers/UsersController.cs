using BugtrackerAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BugtrackerAPI.Controllers;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly IUserData _data;

    public UsersController(IUserData data)
    {
        _data = data;
    }

    [HttpGet]
    public ActionResult<UserDto> GetUsers()
    {
        var users = _data.GetUsers();

        return Ok(users);
    }
}
