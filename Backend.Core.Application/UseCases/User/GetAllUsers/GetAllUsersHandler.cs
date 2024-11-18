using Microsoft.AspNetCore.Http;

namespace Backend.Core.Application.UseCases.User.GetAllUsers;

public class GetAllUsersHandler(GetAllUsersUseCase getAllUsersUseCase)
{
    private readonly GetAllUsersUseCase _getAllUsersUseCase = getAllUsersUseCase;

    public async Task<IResult> Handle(CancellationToken cancellationToken)
    {
        try
        {
            var users = await _getAllUsersUseCase.Execute(cancellationToken);

            var data = users.Select(user => new GetAllUsersResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.Role.ToString()
            });

            return Results.Ok(new { success = true, data });
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}
