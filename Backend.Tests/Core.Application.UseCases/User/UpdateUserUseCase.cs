using AutoFixture;
using Backend.Core.Application.UseCases.User.UpdateUser;
using Entities = Backend.Core.Domain.Entities;
using Backend.Core.Domain.Interfaces;
using Moq;

namespace Backend.Tests.Core.Application.UseCases.User;

public class UpdateUserUseCaseTests
{
    private readonly Fixture _fixture = new();
    private readonly Mock<IUserRepository> _repository = new();
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public async Task Should_ThrowException_When_EmailAddressIsAlreadyInUse()
    {
        _fixture.Customize<Entities.User>(x => x.With(u => u.Email, "john.doe@email.com"));
        var user = _fixture.Create<Entities.User>();

        _repository.Setup(x => x.GetByEmailAsync(It.IsAny<string>(), CancellationToken.None)).ReturnsAsync(user);
        var useCase = new UpdateUserUseCase(_repository.Object, _unitOfWork.Object);

        var exception = await Assert.ThrowsAsync<ArgumentException>(async () =>
            await useCase.Execute(user, CancellationToken.None));

        Assert.Equal("Email address is already in use", exception.Message);
    }

    [Fact]
    public async Task Should_UpdateUser_When_EmailAddressIsNotAlreadyInUse()
    {
        _fixture.Customize<Entities.User>(x => x.With(u => u.Email, "john.doe@email.com"));
        var user = _fixture.Create<Entities.User>();

        _repository.Setup(x => x.GetByEmailAsync(It.IsAny<string>(), CancellationToken.None)).ReturnsAsync((Entities.User?)null);
        var useCase = new UpdateUserUseCase(_repository.Object, _unitOfWork.Object);

        await useCase.Execute(user, CancellationToken.None);

        _repository.Verify(x => x.Update(user), Times.Once);
    }
}
