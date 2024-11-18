using Backend.Core.Application.UseCases.User.GetUser;
using Microsoft.AspNetCore.Http;

namespace Backend.Core.Application.UseCases.User.UpdateUser;

public class UpdateUserHandler(
    GetUserUseCase getUserUseCase,
    UpdateUserUseCase updateUserUseCase
)
{
    private readonly GetUserUseCase _getUserUseCase = getUserUseCase;
    private readonly UpdateUserUseCase _updateUserUseCase = updateUserUseCase;

    public async Task<IResult> Handle(int id, UpdateUserRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _getUserUseCase.Execute(id, cancellationToken);

            if (!string.IsNullOrWhiteSpace(request.FirstName))
                user.FirstName = request.FirstName;

            if (!string.IsNullOrWhiteSpace(request.LastName))
                user.LastName = request.LastName;

            if (!string.IsNullOrWhiteSpace(request.Email))
                user.Email = request.Email;

            if (!string.IsNullOrWhiteSpace(request.Password))
                user.Password = request.Password;

            await _updateUserUseCase.Execute(user, cancellationToken);

            var data = new UpdateUserResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.Role.ToString()
            };

            return Results.Ok(new { success = true, message = "User updated successfully", data });
        }
        catch (Exception ex)
        {
            return Results.BadRequest(new { success = false, message = ex.Message });
        }
    }
}
