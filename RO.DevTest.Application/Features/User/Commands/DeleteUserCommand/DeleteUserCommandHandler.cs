using MediatR;
using Microsoft.AspNetCore.Identity;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RO.DevTest.Application.Features.User.Commands.DeleteUserCommand;

public class DeleteUserCommandHandler(UserManager<Domain.Entities.User> userManager) : IRequestHandler<DeleteUserCommand, Unit>
{
    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.id);
        if (user == null) throw new Exception("User not found");

        var result = await userManager.DeleteAsync(user);
        if (!result.Succeeded) throw new Exception("Failed to delete user");

        return Unit.Value;
    }
}