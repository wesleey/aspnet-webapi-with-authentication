using AutoFixture;
using Backend.Core.Application.UseCases.User.CreateUser;
using Entities = Backend.Core.Domain.Entities;
using Backend.Core.Domain.Interfaces;
using Moq;

namespace Backend.Tests.Core.Application.UseCases.User;

public class CreateUserUseCaseTests
{
    private readonly Fixture _fixture = new();
    private readonly Mock<IUserRepository> _repository = new();
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public async void Should_ThrowException_When_EmailAddressIsAlreadyInUse()
    {
        _fixture.Customize<Entities.User>(x => x.With(u => u.Email, "john@email.com"));
        var user = _fixture.Create<Entities.User>();

        _repository.Setup(x => x.GetByEmailAsync(It.IsAny<string>(), CancellationToken.None)).ReturnsAsync(user);
        var useCase = new CreateUserUseCase(_repository.Object, _unitOfWork.Object);

        var exception = await Assert.ThrowsAsync<ArgumentException>(async () =>
            await useCase.Execute(user, CancellationToken.None));

        Assert.Equal("Email address is already in use", exception.Message);
    }

    [Fact]
    public async Task Should_CreateUser_When_EmailAddressIsNotAlreadyInUse()
    {
        _fixture.Customize<Entities.User>(x => x.With(u => u.Email, "john.doe@email.com"));
        var user = _fixture.Create<Entities.User>();

        var useCase = new CreateUserUseCase(_repository.Object, _unitOfWork.Object);

        await useCase.Execute(user, CancellationToken.None);
        _repository.Verify(x => x.Create(user), Times.Once);
    }
}
