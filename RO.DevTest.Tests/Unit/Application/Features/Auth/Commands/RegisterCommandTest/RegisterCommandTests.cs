using Microsoft.AspNetCore.Identity;
using Moq;
using RO.DevTest.Application.Contracts.Infrastructure;
using RO.DevTest.Application.Features.Auth.Commands.RegisterCommand;
using RO.DevTest.Domain.Enums;
using RO.DevTest.Domain.Exception;

namespace RO.DevTest.Tests.Unit.Application.Features.Auth.Commands.RegisterCommandTest;

public class RegisterCommandHandlerTests
{
    private readonly Mock<UserManager<Domain.Entities.User>> _userManagerMock;
    private readonly Mock<IJwtService> _jwtServiceMock;
    private readonly RegisterCommandHandler _handler;

    public RegisterCommandHandlerTests()
    {
        var store = new Mock<IUserStore<Domain.Entities.User>>();
        _userManagerMock = new Mock<UserManager<Domain.Entities.User>>(
            store.Object, null, null, null, null, null, null, null, null
        );

        _jwtServiceMock = new Mock<IJwtService>();

        _handler = new RegisterCommandHandler(
            _userManagerMock.Object,
            _jwtServiceMock.Object
        );
    }

    [Fact]
    public async Task Handle_ValidRequest_ShouldReturnRegisterResponse()
    {
        // Arrange
        var cmd = new RegisterCommand
        {
            FullName = "Teste Cliente",
            Email = "cliente@rota.com",
            Password = "SenhaForte!1",
        };

        _userManagerMock
            .Setup(um => um.CreateAsync(It.IsAny<Domain.Entities.User>(), cmd.Password))
            .ReturnsAsync(IdentityResult.Success)
            .Callback<Domain.Entities.User, string>((u, p) =>
            {
                u.Id = "usuario-123";
                u.UserName = cmd.Email;
                u.Email = cmd.Email;
                u.Name = cmd.FullName;
            });

        _userManagerMock
            .Setup(um => um.AddToRoleAsync(
                It.IsAny<Domain.Entities.User>(),
                UserRoles.Customer.ToString()
            ))
            .ReturnsAsync(IdentityResult.Success);

        _userManagerMock
            .Setup(um => um.GetRolesAsync(It.IsAny<Domain.Entities.User>()))
            .ReturnsAsync(new List<string> { UserRoles.Customer.ToString() });

        _jwtServiceMock
            .Setup(js => js.GenerateAccessToken(
                It.IsAny<Domain.Entities.User>(),
                It.IsAny<IList<string>>()
            ))
            .Returns("fake-jwt-token");

        // Act
        var result = await _handler.Handle(cmd, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("usuario-123", result.UserId);
        Assert.Equal(cmd.FullName, result.FullName);
        Assert.Equal(cmd.Email, result.Email);
        Assert.Equal("fake-jwt-token", result.Token);

        _userManagerMock.Verify(um => um.CreateAsync(It.IsAny<Domain.Entities.User>(), cmd.Password), Times.Once);
        _userManagerMock.Verify(um => um.AddToRoleAsync(It.IsAny<Domain.Entities.User>(), UserRoles.Customer.ToString()), Times.Once);
        _jwtServiceMock.Verify(js => js.GenerateAccessToken(It.IsAny<Domain.Entities.User>(), It.IsAny<IList<string>>()), Times.Once);
    }

    [Fact]
    public async Task Handle_CreateFails_ShouldThrowBadRequestException()
    {
        // Arrange
        var cmd = new RegisterCommand
        {
            FullName = "Teste Cliente",
            Email = "cliente@rota.com",
            Password = "SenhaFraca"
        };

        _userManagerMock
            .Setup(um => um.CreateAsync(It.IsAny<Domain.Entities.User>(), cmd.Password))
            .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Senha não atende requisitos" }));

        // Act / Assert
        var ex = await Assert.ThrowsAsync<BadRequestException>(() =>
            _handler.Handle(cmd, CancellationToken.None)
        );

        Assert.Contains("Senha não atende requisitos", ex.Message);
    }
}