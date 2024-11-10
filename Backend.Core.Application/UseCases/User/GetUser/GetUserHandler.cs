using Microsoft.AspNetCore.Http;

namespace Backend.Core.Application.UseCases.User.GetUser;

public class GetUserHandler(GetUserUseCase getUserUseCase)
{
    private readonly GetUserUseCase _getUserUseCase = getUserUseCase;

    public async Task<IResult> Handle(int id, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _getUserUseCase.Execute(id, cancellationToken);

            var data = new GetUserResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };

            return Results.Ok(new { success = true, data });
        }
        catch (Exception ex)
        {
            return Results.BadRequest(new { success = false, message = ex.Message });
        }
    }
}
