using MediatR;
using Microsoft.AspNetCore.Identity;
using RO.DevTest.Application.Contracts.Infrastructure;
using RO.DevTest.Domain.Entities;

namespace RO.DevTest.Application.Features.Auth.Commands.LoginCommand;

public class LoginCommandHandler(UserManager<Domain.Entities.User> userManager, IJwtService jwtService) : IRequestHandler<LoginCommand, LoginResponse> {
    private readonly UserManager<Domain.Entities.User> _userManager = userManager;
    private readonly IJwtService _jwtService = jwtService;
    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken) 
    {

        var user = await _userManager.FindByNameAsync(request.Username);
        if (user == null) throw new Exception("User not found");


        var result = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!result) throw new Exception("Invalid password");

        var roles = await _userManager.GetRolesAsync(user);
        var token = _jwtService.GenerateAccessToken(user, roles.ToList());
        if (string.IsNullOrEmpty(token)) throw new Exception("Token generation failed");

        return new LoginResponse
        {
            UserId = user.Id.ToString(),
            Email = user.Email,
            Roles = roles.ToList(),
            Token = token
        };
    }
}