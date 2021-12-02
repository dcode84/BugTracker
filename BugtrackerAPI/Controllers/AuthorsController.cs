using BugtrackerAPI.DTOs.Author;
using BugtrackerAPI.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BugtrackerAPI.Controllers;

[ApiController]
[Route("api/authors")]
public class AuthorsController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly IAuthorData _data;

    public AuthorsController(ILogger<UsersController> logger, IAuthorData data)
    {
        _logger = logger;
        _data = data;
    }

    // GET authors
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAuthorsAsync()
    {
        var authors = await _data.GetAuthorsAsync();
            if (authors is null) return NotFound();

        var authorsList = authors.Select(authors => authors.AuthorAsDto());

        return Ok(authorsList);
    }

    // GET authors/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<AuthorDto>> GetAuthorAsync(int id)
    {
        var author = await _data.GetAuthorAsync(id);
            if(author is null) return NotFound();

        return Ok(author);
    }

    // GET authors/{byLastName}
    [HttpGet("byName")]
    public async Task<ActionResult<AuthorDto>> GetAuthorByNameAsync(string firstName, string lastName)
    {
        AuthorModel author = new()
        {
            FirstName = firstName,
            LastName = lastName
        };

        var tmp = await _data.GetAuthorByNameAsync(author);
            if (tmp is null) return NotFound();

        return Ok(tmp);
    }

    // POST authors
    [HttpPost]
    public async Task<ActionResult<AuthorDto>> CreateAuthorAsync(CreateAuthorDto createAuthor)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);

        AuthorModel author = new()
        {
            FirstName = createAuthor.FirstName,
            LastName = createAuthor.LastName
        };

        await _data.CreateAuthorAsync(author);

        return CreatedAtAction(nameof(GetAuthorByNameAsync), 
            new { firstName = author.FirstName, lastName = author.LastName }, 
            author.AuthorAsDto());
    }

    // PUT authors/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAuthorAsync(UpdateAuthorDto author)
    {
        if (!ModelState.IsValid) return BadRequest();

        var existingAuthor = await _data.GetAuthorAsync(author.Id);
            if (existingAuthor is null) return NotFound();

        AuthorModel updatedAuthor = existingAuthor with
        {
            FirstName = author.FirstName,
            LastName = author.LastName
        };

        await _data.UpdateAuthorAsync(updatedAuthor);

        return NoContent();
    }
}
