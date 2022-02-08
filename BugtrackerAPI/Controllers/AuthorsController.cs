using BugtrackerAPI.DTOs.Author;
using BugtrackerAPI.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BugtrackerAPI.Controllers;

[ApiController]
[Route("api/authors")]
public class AuthorsController : ControllerBase
{
    private readonly ILogger<AuthorsController> _logger;
    private readonly IAuthorData _data;

    public AuthorsController(ILogger<AuthorsController> logger, IAuthorData data)
    {
        _logger = logger;
        _data = data;
    }

    // GET api/authors
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAuthorsAsync()
    {
        var authors = await _data.GetAuthorsAsync();
            if (authors is null) return NotFound();

        var authorsList = authors.Select(authors => authors.AuthorAsDto());

        return Ok(authorsList);
    }

    // GET /api/authors/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<AuthorDto>> GetAuthorAsync(int id)
    {
        var author = await _data.GetAuthorAsync(id);

        if (author is null) return NotFound();

        return Ok(author);
    }

    // GET api/authors/{byLastName}
    [HttpGet("byName")]
    public async Task<ActionResult<AuthorDto>> GetAuthorByNameAsync(string firstName, string lastName)
    {
        Author tmp = new()
        {
            FirstName = firstName,
            LastName = lastName
        };

        var author = await _data.GetAuthorByNameAsync(tmp);

        if (author is null) return NotFound();

        return Ok(author);
    }

    // POST authors
    [HttpPost]
    public async Task<ActionResult<AuthorDto>> CreateAuthorAsync(CreateAuthorDto createAuthor)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);

        Author author = new()
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
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var existingAuthor = await _data.GetAuthorAsync(author.Id);
        
        if (existingAuthor is null) return NotFound();

        Author updatedAuthor = existingAuthor with
        {
            FirstName = author.FirstName,
            LastName = author.LastName
        };

        await _data.UpdateAuthorAsync(updatedAuthor);

        return NoContent();
    }
}
