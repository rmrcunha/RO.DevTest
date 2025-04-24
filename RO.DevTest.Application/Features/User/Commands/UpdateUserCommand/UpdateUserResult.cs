namespace RO.DevTest.Application.Features.User.Commands.UpdateUserCommand;

public record UpdateUserResult
{
    public string UserId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}