using Microsoft.AspNetCore.Identity;
using Moq;
using RO.DevTest.Application.Features.User.Commands.UpdateUserCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RO.DevTest.Tests.Unit.Application.Features.User.Commands;

public class UpdateUserCommandHandlerTests
{
    [Fact]
    public async Task Handle_ValidUpdate_ShouldUpdateUserAndReturnResult()
    {
        // Arrange
        var userId = Guid.NewGuid().ToString();
        var user = new Domain.Entities.User
        {
            Id = userId,
            Email = "client@example.com",
            UserName = "client@example.com",
            Name = "Old Name"
        };

        var command = new UpdateUserCommand
        {
            UserId = userId,
            Name = "New Name"
        };

        var mockUserStore = new Mock<IUserStore<Domain.Entities.User>>();
        var mockUserManager = new Mock<UserManager<Domain.Entities.User>>(
            mockUserStore.Object, null, null, null, null, null, null, null, null);

        mockUserManager.Setup(um => um.FindByIdAsync(userId))
            .ReturnsAsync(user);

        mockUserManager.Setup(um => um.UpdateAsync(It.IsAny<Domain.Entities.User>()))
            .ReturnsAsync(IdentityResult.Success);

        var handler = new UpdateUserCommandHandler(mockUserManager.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(command.Name, result.Name);
        mockUserManager.Verify(um => um.UpdateAsync(It.Is<Domain.Entities.User>(u => u.Name == "New Name")), Times.Once);
    }
}
