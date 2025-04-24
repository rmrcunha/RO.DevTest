using MediatR;
using Microsoft.AspNetCore.Identity;
using RO.DevTest.Application.Contracts.Infrastructure;
using RO.DevTest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RO.DevTest.Application.Features.Auth.Commands.RegisterCommand;

public class RegisterCommandHandler(UserManager<Domain.Entities.User> userManager, IJwtService jwtService): IRequestHandler<RegisterCommand, RegisterResult>
{
    private readonly UserManager<Domain.Entities.User> _userManager = userManager;
    private readonly IJwtService _jwtService = jwtService;

    public async Task<RegisterResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = new Domain.Entities.User
        {
            UserName = request.Email,
            Email = request.Email,
            Name = request.FullName
        };
        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded) throw new Exception($"User creation failed => {string.Join("; ", result.Errors.Select(e=> e.Description))}");

        await _userManager.AddToRoleAsync(user, "Customer");

        var roles = await _userManager.GetRolesAsync(user);

        var token = _jwtService.GenerateAccessToken(user, roles.ToList());

        return new RegisterResult
        {
            UserId = user.Id.ToString(),
            FullName = user.Name,
            Email = user.Email,
            Token = token,
            CreatedAt = DateTime.UtcNow
        };
    }
}