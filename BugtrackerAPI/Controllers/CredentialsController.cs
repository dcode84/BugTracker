using BugtrackerAPI.DTOs.Credential;
using BugtrackerAPI.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BugtrackerAPI.Controllers;

[ApiController]
[Route("api/credentials")]
public class CredentialsController : ControllerBase
{
    private readonly ILogger<CredentialsController> _logger;
    private readonly ICredentialData _data;

    public CredentialsController(ILogger<CredentialsController> logger, ICredentialData data)
    {
        _logger = logger;
        _data = data;
    }

    // GET /api/credentials/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<CredentialDto>> GetCredentialAsync(int id)
    {
        var creds = await _data.GetCredentialAsync(id);

        if (creds is null) return NotFound();

        return Ok(creds);
    }

    // POST /api/credentials
    [HttpPost]
    public async Task<ActionResult<CredentialDto>> CreateCredentialAsync([FromBody]CreateCredentialDto credsToCreate)
    {
        if (!ModelState.IsValid) return BadRequest();

        Credential creds = new()
        {
            PasswordSalt = credsToCreate.PasswordSalt,
            PasswordHash = credsToCreate.PasswordHash
        };

        await _data.CreateCredentialAsync(creds);

        return Created("/api/credentials", creds.CredentialAsDto());
    }


    // PUT /api/credentials/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCredentialAsync([FromBody]UpdateCredentialDto creds)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var existingCreds = await _data.GetCredentialAsync(creds.Id);

        if (existingCreds is null) return NotFound();

        Credential credsToUpdate = existingCreds with
        {
            Id = creds.Id,
            PasswordSalt = creds.PasswordSalt,
            PasswordHash = creds.PasswordHash
        };

        await _data.UpdateCredentialAsync(credsToUpdate);

        return NoContent();
    }
}
