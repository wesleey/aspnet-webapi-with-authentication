using Microsoft.AspNetCore.Http;

namespace Backend.Core.Application.UseCases.User.DeleteUser;

public class DeleteUserHandler(
    DeleteUserUseCase deleteUserUseCase
)
{
    private readonly DeleteUserUseCase _deleteUserUseCase = deleteUserUseCase;

    public async Task<IResult> Handle(int id, CancellationToken cancellationToken)
    {
        try
        {
            await _deleteUserUseCase.Execute(id, cancellationToken);
            return Results.Ok(new { success = true, message = "User deleted successfully" });
        }
        catch (Exception ex)
        {
            return Results.BadRequest(new { success = false, message = ex.Message });
        }
    }
}
