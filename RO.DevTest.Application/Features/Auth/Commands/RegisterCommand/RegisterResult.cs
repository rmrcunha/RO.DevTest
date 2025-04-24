namespace RO.DevTest.Application.Features.Auth.Commands.RegisterCommand
{
    public record RegisterResult
    {
        public string UserId { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public RegisterResult() { }

        public RegisterResult(Domain.Entities.User customer)
        {
            UserId = customer.Id.ToString();
            FullName = customer.Name;
            Email = customer.Email;
        }
    }
}