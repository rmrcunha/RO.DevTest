using System.Text.Json.Serialization;

namespace RO.DevTest.Application.Features.Auth.Commands.LoginCommand;

public record LoginResponse {
    public string UserId { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public IList<string>? Roles { get; set; } = null;
    public string Token { get; set; } = string.Empty;
}
