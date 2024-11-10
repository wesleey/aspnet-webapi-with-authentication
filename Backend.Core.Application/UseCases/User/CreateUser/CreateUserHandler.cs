using Microsoft.AspNetCore.Http;

namespace Backend.Core.Application.UseCases.User.CreateUser;

public class CreateUserHandler(CreateUserUseCase createUserUseCase)
{
    private readonly CreateUserUseCase _createUserUseCase = createUserUseCase;

    public async Task<IResult> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var user = CreateUserMapper.MapTo(request);
            await _createUserUseCase.Execute(user, cancellationToken);
            var data = CreateUserMapper.MapFrom(user);
            return Results.Json(new { success = true, message = "User created successfully", data }, statusCode: 201);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(new { success = false, message = ex.Message });
        }
    }
}
