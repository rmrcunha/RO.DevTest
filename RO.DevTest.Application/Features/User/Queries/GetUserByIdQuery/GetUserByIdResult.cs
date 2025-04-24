﻿namespace RO.DevTest.Application.Features.User.Queries.GetUserByIdQuery;

public class GetUserByIdResult
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}