using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RO.DevTest.Application.Features.User.Commands.UpdateUserCommand;

public class UpdateUserCommandHandler (UserManager<Domain.Entities.User> userManager):IRequestHandler<UpdateUserCommand, UpdateUserResult>
{
    public async Task<UpdateUserResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.UserId);
        if (user == null) throw new Exception("User not found");

        user.Name = request.Name;
        user.Email = request.Email;
        user.UserName = request.Email;

        var result = await userManager.UpdateAsync(user);
        if (!result.Succeeded) throw new Exception($"Failed to update user -> {string.Join(" ", result.Errors.Select(e=>e.Description))}");
        

        return new UpdateUserResult
        {
            UserId = user.Id,
            Name = user.Name,
            Email = user.Email
        };
    }
}
