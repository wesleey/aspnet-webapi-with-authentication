using Backend.Core.Domain.Enums;

namespace Backend.Infra.Auth.Jwt.Interfaces;

public interface IJwtTokenGenerator
{
    string Generate(int id, string email, Role role);
    bool Validate(string token);
    bool IsAdmin(string token);
    int GetId(string token);
}
