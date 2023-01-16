using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BugtrackerAPI.Controllers;
using BugtrackerAPI.DTOs.UserRole;
using DataAccess.Data.Interfaces;
using DataAccess.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace BugtrackerAPI.Tests;

public class UserRolesControllerTests
{
    private readonly Mock<IUserRoleData> _repositoryStub = new();
    private readonly Mock<ILogger<UserRolesController>> _loggerStub = new();
    private readonly UserRolesController _controller;

    public UserRolesControllerTests()
    {
        _controller = new(_loggerStub.Object, _repositoryStub.Object);
    }

    [Fact]
    public async Task GetUserRoleAsync_WithExistingUserRole_ReturnsUserRole()
    {
        var expectedUserRole = CreateRandomUserRole();

        _repositoryStub.Setup(r => r.GetUserRoleAsync(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(expectedUserRole);

        var result = await _controller.GetUserRoleAsync(1,1);

        result.Result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task GetUserRoleAsync_WithUnexisting_ReturnsNotFound()
    {
        _repositoryStub.Setup(r => r.GetUserRoleAsync(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync((UserRole)null);

        var result = await _controller.GetUserRoleAsync(1, 1);

        result.Result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task GetUserRolesAsync_WithExistingUserRoles_ReturnsUserRoles()
    {
        var expectedUserRoles = new[] { CreateRandomUserRole() , CreateRandomUserRole(), CreateRandomUserRole()};

        _repositoryStub.Setup(r => r.GetUserRolesAsync(It.IsAny<int>()))
            .ReturnsAsync(expectedUserRoles);

        var result = await _controller.GetUserRolesAsync(1);

        result.Result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task CreateUserRoleAsync_WithUserRole_ReturnsCreatedAtAction()
    {
        var userRole = CreateRandomUserRole();

        CreateUserRoleDto createdUserRole = new()
        {
            UserId = 1,
            RoleId = 1
        };

        var result = await _controller.CreateUserRoleAsync(createdUserRole);

        result.Result.Should().BeOfType<CreatedAtActionResult>();
    }

    [Fact]
    public async Task CreateUserRoleAsync_WithWrongModelState_ReturnsBadRequest()
    {
        var badmodel = new CreateUserRoleDto();

        _controller.ModelState.AddModelError("Error", "Error");

        var result = await _controller.CreateUserRoleAsync(badmodel);

        result.Result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task DeleteUserRoleAsync_WithExistingUserRole_ReturnsNoContent()
    {
        var ur = CreateRandomUserRole();

        _repositoryStub.Setup(repo => repo.GetUserRoleAsync(It.IsAny<int>(), It.IsAny<int>())).
            ReturnsAsync(ur);

        DeleteUserRoleDto deleteUserRole = new()
        {
            UserId = ur.UserId,
            RoleId = ur.RoleId
        };

        var result = await _controller.DeleteUserRoleAsync(deleteUserRole);

        result.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task DeleteUserRoleAsync_WithUnexistingUserRole_ReturnsNotFound()
    {
        _repositoryStub.Setup(repo => repo.GetUserRoleAsync(It.IsAny<int>(), It.IsAny<int>())).
            ReturnsAsync((UserRole)null);

        DeleteUserRoleDto deleteUserRole = new()
        {
            UserId = 1,
            RoleId = 1
        };

        var result = await _controller.DeleteUserRoleAsync(deleteUserRole);

        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task DeleteUserRoleAsync_WithBadModel_ReturnsBadRequest()
    {
        var badmodel = new DeleteUserRoleDto();

        _controller.ModelState.AddModelError("Error", "Error");

        var result = await _controller.DeleteUserRoleAsync(badmodel);

        result.Should().BeOfType<BadRequestObjectResult>();
    }

    private UserRole CreateRandomUserRole()
    {
        return new UserRole
        {
            UserId = new Random().Next(1, 10),
            RoleId = new Random().Next(1, 10)
        };
    }
}
