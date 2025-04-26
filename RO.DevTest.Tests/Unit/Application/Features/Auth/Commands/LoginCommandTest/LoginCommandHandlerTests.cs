using Microsoft.AspNetCore.Identity;
using Moq;
using RO.DevTest.Application.Contracts.Infrastructure;
using RO.DevTest.Application.Features.Auth.Commands.LoginCommand;
using RO.DevTest.Domain.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RO.DevTest.Tests.Unit.Application.Features.Auth.Commands.LoginCommandTest;

public class LoginCommandHandlerTests
{
    private readonly Mock<UserManager<Domain.Entities.User>> _userManagerMock;
    private readonly Mock<IJwtService> _jwtServiceMock;
    private readonly LoginCommandHandler _handler;

    public LoginCommandHandlerTests()
    {
        var store = new Mock<IUserStore<Domain.Entities.User>>();
        _userManagerMock = new Mock<UserManager<Domain.Entities.User>>(
            store.Object, null, null, null, null, null, null, null, null
        );

        _jwtServiceMock = new Mock<IJwtService>();

        _handler = new LoginCommandHandler(
            _userManagerMock.Object,
            _jwtServiceMock.Object
        );
    }

    [Fact]
    public async Task Handle_ValidCredentials_ReturnsLoginResponse()
    {

        // Arrange
        var cmd = new LoginCommand
        {
            Username = "user@rota.com",
            Password = "Senha123!"
        };
        var user = new Domain.Entities.User
        {
            Id = "user-1",
            Email = cmd.Username,
            Name = "User Name",
            UserName = cmd.Username
        };
        var roles = new List<string> { "Cliente" };

        _userManagerMock
            .Setup(um => um.FindByNameAsync(cmd.Username))
            .ReturnsAsync(user);
        _userManagerMock
            .Setup(um => um.CheckPasswordAsync(user, cmd.Password))
            .ReturnsAsync(true);
        _userManagerMock
            .Setup(um => um.GetRolesAsync(user))
            .ReturnsAsync(roles);
        _jwtServiceMock
            .Setup(js => js.GenerateAccessToken(user, roles))
            .Returns("fake-jwt-token");

        // Act
        var result = await _handler.Handle(cmd, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(user.Id, result.UserId);
        Assert.Equal(user.Email, result.Email);
        Assert.Equal(roles, result.Roles);
        Assert.Equal("fake-jwt-token", result.Token);

        _userManagerMock.Verify(um => um.FindByNameAsync(cmd.Username), Times.Once);
        _userManagerMock.Verify(um => um.CheckPasswordAsync(user, cmd.Password), Times.Once);
        _jwtServiceMock.Verify(js => js.GenerateAccessToken(user, roles), Times.Once);
    }

    [Theory]
    [InlineData(null, "any", "Invalid credentials")]        // user not found
    [InlineData("user@rota.com", "wrongpass", "Invalid credentials")] // bad password
    public async Task Handle_InvalidCredentials_ThrowsBadRequestException(
        string? email, string password, string expectedMessage)
    {
        // Arrange
        var cmd = new LoginCommand
        {
            Username = email,
            Password = password
        };
        Domain.Entities.User? user = email is null
            ? null
            : new Domain.Entities.User { Id = "u", Email = email, UserName = email };

        _userManagerMock
            .Setup(um => um.FindByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(user);
        _userManagerMock
            .Setup(um => um.CheckPasswordAsync(user!, It.IsAny<string>()))
            .ReturnsAsync(false);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<BadRequestException>(()
            => _handler.Handle(cmd, CancellationToken.None));
        Assert.Contains(expectedMessage, ex.Message);
    }
}
