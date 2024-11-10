using Entities = Backend.Core.Domain.Entities;
using Backend.Core.Domain.Interfaces;
using Backend.Core.Application.UseCases.User.GetAllUsers;
using AutoFixture;
using Moq;

namespace Backend.Tests.Core.Application.UseCases.User;

public class GetAllUsersUseCaseTests
{
    private readonly Fixture _fixture = new();
    private readonly Mock<IUserRepository> _repository = new();

    [Fact]
    public async Task Should_ReturnAllUsers()
    {
        _fixture.Customize<Entities.User>(x => x.With(u => u.Email, "john.doe@email.com"));

        var users = new List<Entities.User>
        {
            _fixture.Create<Entities.User>(),
        };

        _repository.Setup(x => x.GetAllAsync(CancellationToken.None)).ReturnsAsync(users);

        var useCase = new GetAllUsersUseCase(_repository.Object);
        var result = await useCase.Execute(CancellationToken.None);

        Assert.Equal(users, result);
    }
}
