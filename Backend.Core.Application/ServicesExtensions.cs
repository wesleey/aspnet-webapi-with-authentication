using Backend.Core.Application.UseCases.Auth.Login;
using Backend.Core.Application.UseCases.User.CreateUser;
using Backend.Core.Application.UseCases.User.DeleteUser;
using Backend.Core.Application.UseCases.User.GetAllUsers;
using Backend.Core.Application.UseCases.User.GetUser;
using Backend.Core.Application.UseCases.User.UpdateUser;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Core.Application;

public static class ServicesExtensions
{
    public static void ConfigureApplicationApp(this IServiceCollection services)
    {
        services.AddScoped<CreateUserUseCase>();
        services.AddScoped<CreateUserHandler>();
        services.AddScoped<UpdateUserUseCase>();
        services.AddScoped<UpdateUserHandler>();
        services.AddScoped<DeleteUserUseCase>();
        services.AddScoped<DeleteUserHandler>();
        services.AddScoped<GetAllUsersUseCase>();
        services.AddScoped<GetAllUsersHandler>();
        services.AddScoped<GetUserUseCase>();
        services.AddScoped<GetUserHandler>();
        services.AddScoped<LoginUseCase>();
        services.AddScoped<LoginHandler>();
    }
}
