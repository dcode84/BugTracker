using System;
using System.Collections;
using System.Threading.Tasks;
using BugtrackerAPI.Controllers;
using BugtrackerAPI.DTOs.User;
using DataAccess.Data.Interfaces;
using DataAccess.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace BugtrackerAPI.Tests;


public class UsersControllerTests
{
    private readonly Mock<IUserData> _repositoryStub = new();
    private readonly Mock<ILogger<UsersController>> _loggerStub = new();
    private readonly UsersController _controller;

    public UsersControllerTests()
    {
        // Setup
        _controller = new(_loggerStub.Object, _repositoryStub.Object);
    }

    [Fact(Skip="")]
    public async Task GetUsersAsync_WithExistingUsers_ReturnsAllUsers()
    {
        var expectedUsers = new[] { CreateRandomUser(), CreateRandomUser(), CreateRandomUser() };

        _repositoryStub.Setup(repo => repo.GetUsersAsync()).ReturnsAsync(expectedUsers);

        var actualUsers = await _controller.GetUsersAsync();

        actualUsers.Result.Should().BeOfType<OkObjectResult>();
    }

    [Fact(Skip="")]
    public async Task GetUserAsync_WithUnexistingUser_ReturnsNotFound()
    {
        _repositoryStub.Setup(repo => repo.GetUserAsync(It.IsAny<int>()))
            .ReturnsAsync((User)null);

        var result = await _controller.GetUserAsync(1);

        result.Result.Should().BeOfType<NotFoundResult>();
    }

    [Fact(Skip = "")]
    public async Task GetUserAsync_WithExistingUser_ReturnsExpectedUser()
    {
        var expectedUser = CreateRandomUser();

        _repositoryStub.Setup(repo => repo.GetUserAsync(It.IsAny<int>()))
            .ReturnsAsync(expectedUser);

        var result = await _controller.GetUserAsync(1);

        result.Result.Should().BeOfType<OkObjectResult>();
    }
    
    [Fact(Skip = "")]
    public async Task GetUserByNameAsync_WithUnexistingUser_ReturnsNotFound()
    {
        _repositoryStub.Setup(repo => repo.GetUserByNameAsync(It.IsAny<string>()))
            .ReturnsAsync((User)null);

        var result = await _controller.GetUserByNameAsync("oop");

        result.Result.Should().BeOfType<NotFoundResult>();
    }

    [Fact(Skip="")]
    public async Task GetUserByNameAsync_WithExistingUser_ReturnsExpectedUser()
    {
        var expectedUser = CreateRandomUser();

        _repositoryStub.Setup(repo => repo.GetUserByNameAsync(It.IsAny<string>()))
            .ReturnsAsync(expectedUser);

        var result = await _controller.GetUserByNameAsync("oop");

        result.Result.Should().BeOfType<OkObjectResult>();
    }

    [Fact(Skip="")]
    public async Task CreateUserAsync_WithWrongModelState_ReturnsBadRequest()
    {
        var badModel = new CreateUserDto();

        _controller.ModelState.AddModelError("Error", "Error");

        var result = await _controller.CreateUserAsync(badModel);
        
        result.Result.Should().BeOfType<BadRequestObjectResult>();      
    }

    [Fact(Skip="")]
    public async Task CreateUserAsync_WithUserToCreate_ReturnsCreatedAtAction()
    {
        var userToCreate = new CreateUserDto()
        {
            Username = Guid.NewGuid().ToString(),
            Email = "pew@pew.de"
        };
    
        var result = await _controller.CreateUserAsync(userToCreate);

        var createdUser = (result.Result as CreatedAtActionResult).Value as UserDto;
        userToCreate.Should().BeEquivalentTo(
            createdUser, 
            options => options.ComparingByMembers<UserDto>().ExcludingMissingMembers()
        );

        createdUser.Username.Should().NotBeEmpty();
        createdUser.Email.Should().NotBeEmpty();
    }

    [Fact(Skip="")]
    public async Task UpdateUserAsync_WithWrongModelState_ReturnsBadRequest()
    {
        var badModel = new UpdateUserDto();

        _controller.ModelState.AddModelError("Error", "Error");

        var result = await _controller.UpdateUserAsync(badModel);

        result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact(Skip="")]
    public async Task UpdateUserAsync_WithUnexistingUser_ReturnsNotFound()
    {
        var userToUpdate = new UpdateUserDto
        {
            Id = 1,
            Username = Guid.NewGuid().ToString(),
            Email = "pew@pew.de"
        };

        _repositoryStub.Setup(repo => repo.GetUserAsync(It.IsAny<int>()))
            .ReturnsAsync((User)null);

        var result = _controller.UpdateUserAsync(userToUpdate);

        result.Result.Should().BeOfType<NotFoundResult>();
    }

    [Fact(Skip="")]
    public async Task UpdateUserAsync_WithExistingUser_ReturnsNoContent()
    {
        var existingUser = CreateRandomUser();

        _repositoryStub.Setup(repo => repo.GetUserAsync(It.IsAny<int>()))
            .ReturnsAsync(existingUser);

        var userId = existingUser.Id;
        var userToUpdate = new UpdateUserDto() 
        { 
            Username = Guid.NewGuid().ToString(), 
            Email = existingUser.Email
        };

        var result = await _controller.UpdateUserAsync(userToUpdate);

        result.Should().BeOfType<NoContentResult>();
    }

    [Fact(Skip="")]
    public async Task DeleteUserAsync_WithExistingUser_ReturnsNoContent()
    {
        var existingUser = CreateRandomUser();

        _repositoryStub.Setup(repo => repo.GetUserAsync(It.IsAny<int>()))
            .ReturnsAsync(existingUser);

        var result = await _controller.DeleteUserAsync(existingUser.Id);

        result.Should().BeOfType<NoContentResult>();
    }

    [Fact(Skip="")]
    public async Task DeleteUserAsync_WithUnexistingUser_ReturnsNotFound()
    {
        _repositoryStub.Setup(repo => repo.GetUserAsync(It.IsAny<int>()))
            .ReturnsAsync((User)null);

        var result = await _controller.DeleteUserAsync(1);

        result.Should().BeOfType<NotFoundResult>();
    }

    private User CreateRandomUser()
    {
        return new()
        {
            Id = new Random().Next(1,10),
            Username = Guid.NewGuid().ToString(),
            Email = "kekekek@kek.eu",
            DateCreated = DateTimeOffset.UtcNow,
            IsValidated = true,
            ModifiedAt = DateTimeOffset.UtcNow, //.AddDays(1).AddHours(10).AddSeconds(42),
            DeletedAt = DateTimeOffset.UtcNow
        };
    }
}
