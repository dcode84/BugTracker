using System;
using System.Threading.Tasks;
using BugtrackerAPI.Controllers;
using BugtrackerAPI.DTOs.Credential;
using DataAccess.Data.Interfaces;
using DataAccess.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace BugtrackerAPI.Tests;

public class CredentialsControllerTests
{
    private readonly Mock<ICredentialData> _repositoryStub = new();
    private readonly Mock<ILogger<CredentialsController>> _loggerStub = new();
    private readonly CredentialsController _controller;

    public CredentialsControllerTests()
    {
        _controller = new(_loggerStub.Object, _repositoryStub.Object);
    }

    [Fact]
    public async Task GetCredentialAsync_WithExistingCredentials_ReturnsCredentials()
    {
        var creds = CreateRandomCredentials();

        _repositoryStub.Setup(r => r.GetCredentialAsync(It.IsAny<int>()))
            .ReturnsAsync(creds);

        var result = await _controller.GetCredentialAsync(1);

        result.Result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task GetCredentialAsync_WithNullCreds_ReturnsNotFound()
    {
        _repositoryStub.Setup(r => r.GetCredentialAsync(It.IsAny<int>()))
            .ReturnsAsync((Credential)null);

        var result = await _controller.GetCredentialAsync(1);

        result.Result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task CreateCredentialAsync_WithCredsToCreate_ReturnsCreatedAtAction()
    {
        var credsToCreate = new CreateCredentialDto()
        {
            PasswordSalt = Guid.NewGuid().ToString(),
            PasswordHash = Guid.NewGuid().ToString()
        };

        var result = await _controller.CreateCredentialAsync(credsToCreate);

        var createdCreds = (result.Result as CreatedResult).Value as CredentialDto;
        credsToCreate.Should().BeEquivalentTo(
            createdCreds,
            o => o.ComparingByMembers<CredentialDto>().ExcludingMissingMembers());

        createdCreds.PasswordSalt.Should().NotBeEmpty();
        createdCreds.PasswordHash.Should().NotBeEmpty();
    }

    [Fact]
    public async Task CreateCredentialAsync_WithBadModel_ReturnsBadRequest()
    {
        var creds = new CreateCredentialDto();

        _controller.ModelState.AddModelError("Error", "Error");

        var result = await _controller.CreateCredentialAsync(creds);

        result.Result.Should().BeOfType<BadRequestResult>();
    }

    [Fact]
    public async Task UpdateCredentialAsync_WithExistingCreds_ReturnsNoContent()
    {
        var creds = CreateRandomCredentials();

        _repositoryStub.Setup(r => r.GetCredentialAsync(It.IsAny<int>()))
            .ReturnsAsync(creds);

        var credsToUpdate = new UpdateCredentialDto()
        {
            PasswordSalt = creds.PasswordSalt,
            PasswordHash = creds.PasswordHash
        };

        var result = await _controller.UpdateCredentialAsync(credsToUpdate);

        result.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task UpdateCredentialAsync_WithBadModel_ReturnsBadRequest()
    {
        var badModel = new UpdateCredentialDto();

        _controller.ModelState.AddModelError("Error", "Error");

        var result = await _controller.UpdateCredentialAsync(badModel);

        result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task UpdateCredentialAsync_WithNullCreds_ReturnsNotFound()
    {
        var creds = new UpdateCredentialDto();

        _repositoryStub.Setup(r => r.GetCredentialAsync(It.IsAny<int>()))
            .ReturnsAsync((Credential)null);

        var result = await _controller.UpdateCredentialAsync(creds);

        result.Should().BeOfType<NotFoundResult>();
    }

    private Credential CreateRandomCredentials()
    {
        return new Credential()
        {
            PasswordHash = Guid.NewGuid().ToString(),
            PasswordSalt = Guid.NewGuid().ToString()
        };
    }
}
