using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugtrackerAPI.Controllers;
using DataAccess.Data.Interfaces;
using DataAccess.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace BugtrackerAPI.Tests;

public class ProjectsControllerTests
{
    private readonly Mock<IProjectData> _repositoryStub = new();
    private readonly Mock<ILogger<ProjectsController>> _loggerStub = new();
    private readonly ProjectsController _controller;

    public ProjectsControllerTests()
    {
        _controller = new(_loggerStub.Object, _repositoryStub.Object);
    }

    [Fact]
    public async Task GetProjectAsync_WithExistingProject_ReturnsProject()
    {
        var project = CreateRandomProject();

        _repositoryStub.Setup(repo => repo.GetProjectAsync(It.IsAny<int>())).ReturnsAsync(project);

        var result = await _controller.GetProjectAsync(project.Id);

        result.Result.Should().BeOfType<OkObjectResult>();
    }

    private Project CreateRandomProject()
    {
        return new()
        {
            Id = new Random().Next(1, 10),
            Name = "",
            Description = "",
            ManagerId = 1
        };
    }
}
