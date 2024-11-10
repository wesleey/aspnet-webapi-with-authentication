using Backend.Infra.Auth.Jwt;
using Backend.Core.Application;
using Backend.Infra.Persistence;
using Backend.Presentation.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();

builder.Services.ConfigureApplicationApp();
builder.Services.ConfigurePersistenceApp(builder.Configuration);
builder.Services.ConfigureAuthApp(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.UseCors();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.Run();
