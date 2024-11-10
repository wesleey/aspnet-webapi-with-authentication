namespace Backend.Core.Application.UseCases.User.UpdateUser;

public sealed record UpdateUserResponse
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
}
