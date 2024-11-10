using AutoFixture;
using Entities = Backend.Core.Domain.Entities;
using Backend.Core.Domain.Interfaces;
using Backend.Core.Application.UseCases.User.GetUser;
using Moq;

namespace Backend.Tests.Core.Application.UseCases.User;

public class GetUserUseCaseTests
{
    private readonly Fixture _fixture = new();
    private readonly Mock<IUserRepository> _repository = new();

    [Fact]
    public async Task Should_ThrowException_When_UserNotFound()
    {
        _fixture.Customize<Entities.User>(x => x.With(u => u.Email, "john.doe@email.com"));
        var user = _fixture.Create<Entities.User>();

        _repository.Setup(x => x.GetAsync(It.IsAny<int>(), CancellationToken.None)).ReturnsAsync((Entities.User?)null);
        var useCase = new GetUserUseCase(_repository.Object);

        var exception = await Assert.ThrowsAsync<ArgumentException>(async () =>
            await useCase.Execute(user.Id, CancellationToken.None));

        Assert.Equal("User not found", exception.Message);
    }

    [Fact]
    public async Task Should_ReturnUser_When_UserFound()
    {
        _fixture.Customize<Entities.User>(x => x.With(u => u.Email, "john.doe@email.com"));
        var user = _fixture.Create<Entities.User>();

        _repository.Setup(x => x.GetAsync(It.IsAny<int>(), CancellationToken.None)).ReturnsAsync(user);
        var useCase = new GetUserUseCase(_repository.Object);

        var result = await useCase.Execute(user.Id, CancellationToken.None);
        Assert.Equal(user, result);
    }
}
