using Backend.Core.Application.UseCases.User.CreateUser;
using Backend.Core.Application.UseCases.User.DeleteUser;
using Backend.Core.Application.UseCases.User.GetAllUsers;
using Backend.Core.Application.UseCases.User.GetUser;
using Backend.Core.Application.UseCases.User.UpdateUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Presentation.Api.Controllers;

[ApiController]
[Route("user")]
public class UserController(
    CreateUserHandler createUserHandler,
    UpdateUserHandler updateUserHandler,
    DeleteUserHandler deleteUserHandler,
    GetAllUsersHandler getAllUsersHandler,
    GetUserHandler getUserHandler
) : ControllerBase
{
    private readonly CreateUserHandler _createUserHandler = createUserHandler;
    private readonly UpdateUserHandler _updateUserHandler = updateUserHandler;
    private readonly DeleteUserHandler _deleteUserHandler = deleteUserHandler;
    private readonly GetAllUsersHandler _getAllUsersHandler = getAllUsersHandler;
    private readonly GetUserHandler _getUserHandler = getUserHandler;

    [Authorize(Policy = "Admin")]
    [HttpPost]
    public async Task<IResult> Create([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
                => await _createUserHandler.Handle(request, cancellationToken);

    [Authorize(Policy = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<IResult> Update(int id, [FromBody] UpdateUserRequest request, CancellationToken cancellationToken)
        => await _updateUserHandler.Handle(id, request, cancellationToken);

    [Authorize(Policy = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IResult> Delete(int id, CancellationToken cancellationToken)
        => await _deleteUserHandler.Handle(id, cancellationToken);

    [Authorize(Policy = "User")]
    [Authorize]
    [HttpGet]
    public async Task<IResult> GetAll(CancellationToken cancellationToken)
        => await _getAllUsersHandler.Handle(cancellationToken);

    [Authorize(Policy = "User")]
    [HttpGet("{id:int}")]
    public async Task<IResult> Get(int id, CancellationToken cancellationToken)
        => await _getUserHandler.Handle(id, cancellationToken);
}
