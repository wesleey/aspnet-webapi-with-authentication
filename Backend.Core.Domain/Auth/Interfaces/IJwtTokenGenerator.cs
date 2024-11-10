namespace Backend.Core.Domain.Auth.Interfaces;

public interface IJwtTokenGenerator
{
    string Generate(int id, string email);
    bool Validate(string token);
    int GetId(string token);
}
