using Microsoft.AspNetCore.Identity;
using Moq;
using RO.DevTest.Application.Features.User.Commands.DeleteUserCommand;
using RO.DevTest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RO.DevTest.Tests.Unit.Application.Features.User.Commands;

public class DeleteUserCommandHandlerTests
{
    [Fact]
    public async Task Handle_ValidDelete_ShouldDeleteUserAndReturnResult()
    {
        // Arrange
        var userId = Guid.NewGuid().ToString();
        var user = new Domain.Entities.User
        {
            Id = userId,
            Email = "user@example.com",
            UserName = "user@example.com"
        };

        var command = new DeleteUserCommand(userId);

        var mockUserStore = new Mock<IUserStore<Domain.Entities.User>>();
        var mockUserManager = new Mock<UserManager<Domain.Entities.User>>(
            mockUserStore.Object, null, null, null, null, null, null, null, null);

        mockUserManager.Setup(um => um.FindByIdAsync(userId))
            .ReturnsAsync(user);

        mockUserManager.Setup(um => um.DeleteAsync(user))
            .ReturnsAsync(IdentityResult.Success);

        var handler = new DeleteUserCommandHandler(mockUserManager.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        result.Equals(true);
        mockUserManager.Verify(um => um.DeleteAsync(user), Times.Once);
    }
}
