using System;
using System.Threading.Tasks;
using BugtrackerAPI.Controllers;
using BugtrackerAPI.DTOs.Author;
using DataAccess.Data.Interfaces;
using DataAccess.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace BugtrackerAPI.Tests;

public class AuthorsControllerTests
{
    private readonly Mock<IAuthorData> _repositoryStub = new();
    private readonly Mock<ILogger<AuthorsController>> _loggerStub = new();
    private readonly AuthorsController _controller;

    public AuthorsControllerTests()
    {
        _controller = new(_loggerStub.Object, _repositoryStub.Object);
    }

    [Fact]
    public async Task GetAuthorsAsync_WithExistingAuthors_ReturnsAllAuthors()
    {
        var expectedAuthors = new[] { CreateRandomAuthor(), CreateRandomAuthor(), CreateRandomAuthor() };
    
        _repositoryStub.Setup(repo => repo.GetAuthorsAsync()).ReturnsAsync(expectedAuthors);

        var actualAuthors = await _controller.GetAuthorsAsync();

        actualAuthors.Result.Should().BeOfType<OkObjectResult>();
    } 

    [Fact]
    public async Task GetAuthorsAsync_WithNullAuthor_ReturnsNotFound()
    {
        _repositoryStub.Setup(repo => repo.GetAuthorsAsync()).ReturnsAsync((Author[])null);

        var result = await _controller.GetAuthorsAsync();

        result.Result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task GetAuthorAsync_WithExistingAuthor_ReturnsAuthor()
    {
        var expectedAuthor = CreateRandomAuthor();

        _repositoryStub.Setup(repo => repo.GetAuthorAsync(It.IsAny<int>()))
            .ReturnsAsync(expectedAuthor);

        var result = await _controller.GetAuthorAsync(1);

        result.Result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task GetAuthorAsync_WithNullAuthor_ReturnsNotFound()
    {
        _repositoryStub.Setup(repo => repo.GetAuthorAsync(It.IsAny<int>()))
            .ReturnsAsync((Author)null);

        var result = await _controller.GetAuthorAsync(1);

        result.Result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task GetAuthorByNameAsync_WithExistingUser_ReturnsAuthor()
    {
        var author = CreateRandomAuthor();

        _repositoryStub.Setup(repo => repo.GetAuthorByNameAsync(It.IsAny<Author>()))
            .ReturnsAsync(author);

        var result = await _controller.GetAuthorByNameAsync("heh", "heh");

        result.Result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task GetAuthorByNameAsync_WithNullAuthor_ReturnsNotFound()
    {
        var author = new Author();

        _repositoryStub.Setup(repo => repo.GetAuthorByNameAsync(author)).ReturnsAsync((Author)null);

        var result = await _controller.GetAuthorByNameAsync(It.IsAny<string>(), It.IsAny<string>());

        result.Result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task CreateAuthorAsync_WithAuthorToCreate_ReturnsCreatedAtAction()
    {
        var authorToCreate = new CreateAuthorDto()
        {
            FirstName = Guid.NewGuid().ToString(),
            LastName = Guid.NewGuid().ToString()
        };

        var result = await _controller.CreateAuthorAsync(authorToCreate);

        var createdAuthor = (result.Result as CreatedAtActionResult).Value as AuthorDto;
        authorToCreate.Should().BeEquivalentTo(
            createdAuthor, 
            options => options.ComparingByMembers<AuthorDto>().ExcludingMissingMembers());

        createdAuthor.FirstName.Should().NotBeEmpty();
        createdAuthor.LastName.Should().NotBeEmpty();
    }

    [Fact]
    public async Task CreateAuthorAsync_WithBadModel_ReturnsBadRequest()
    {
        var badModel = new CreateAuthorDto();

        _controller.ModelState.AddModelError("Error", "Error");

        var result = await _controller.CreateAuthorAsync(badModel);

        result.Result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task UpdateAuthorAsync_WithExistingAuthor_ReturnsNoContent()
    {
        var existingAuthor = CreateRandomAuthor();

        _repositoryStub.Setup(repo => repo.GetAuthorAsync(It.IsAny<int>()))
            .ReturnsAsync(existingAuthor);

        var authorId = existingAuthor.Id;
        var authorToUpdate = new UpdateAuthorDto()
        {
            Id = authorId,
            FirstName = existingAuthor.FirstName,
            LastName = existingAuthor.LastName
        };

        var result = await _controller.UpdateAuthorAsync(authorToUpdate);

        result.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task UpdateAuthorAsync_WithBadModel_ReturnsBadRequest()
    {
        var badModel = new UpdateAuthorDto();

        _controller.ModelState.AddModelError("Error", "Error");

        var result = await _controller.UpdateAuthorAsync(badModel);

        result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task UpdateAuthorAsync_WithNullAuthor_ReturnsNotFound()
    {
        var author = new UpdateAuthorDto();

        _repositoryStub.Setup(repo => repo.GetAuthorAsync(It.IsAny<int>()))
            .ReturnsAsync((Author)null);

        var result = await _controller.UpdateAuthorAsync(author);

        result.Should().BeOfType<NotFoundResult>();
    }


    private Author CreateRandomAuthor()
    {
        return new()
        {
            Id = new Random().Next(1, 10),
            FirstName = Guid.NewGuid().ToString(),
            LastName = Guid.NewGuid().ToString(),
            ModifiedAt = DateTime.UtcNow            
        };
    }
}
