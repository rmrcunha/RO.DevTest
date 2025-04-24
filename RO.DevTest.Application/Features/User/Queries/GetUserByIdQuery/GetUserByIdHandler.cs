using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RO.DevTest.Application.Features.User.Queries.GetUserByIdQuery;

public class GetUserByIdHandler(UserManager<Domain.Entities.User> userManager): IRequestHandler<GetUserByIdQuery, GetUserByIdResult>
{
    public async Task<GetUserByIdResult> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.Id);
        if (user == null) throw new Exception("User not found");

        var roles = await userManager.GetRolesAsync(user);

        return new GetUserByIdResult
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = roles.FirstOrDefault() ?? string.Empty
        };
    }
}
