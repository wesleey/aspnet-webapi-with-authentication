using AutoFixture;
using Entities = Backend.Core.Domain.Entities;
using Backend.Core.Domain.Interfaces;
using Backend.Core.Application.UseCases.User.DeleteUser;
using Moq;

namespace Backend.Tests.Core.Application.UseCases.User;

public class DeleteUserUseCaseTests
{
    private readonly Fixture _fixture = new();
    private readonly Mock<IUserRepository> _repository = new();
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public async Task Should_ThrowException_When_UserNotFound()
    {
        _fixture.Customize<Entities.User>(x => x.With(u => u.Email, "john.doe@email.com"));
        var user = _fixture.Create<Entities.User>();

        _repository.Setup(x => x.GetAsync(It.IsAny<int>(), CancellationToken.None)).ReturnsAsync((Entities.User?)null);
        var useCase = new DeleteUserUseCase(_repository.Object, _unitOfWork.Object);

        var exception = await Assert.ThrowsAsync<ArgumentException>(async () =>
            await useCase.Execute(user.Id, CancellationToken.None));

        _repository.Verify(x => x.Delete(user), Times.Never);

        Assert.Equal("User not found", exception.Message);
    }

    [Fact]
    public async Task Should_DeleteUser_When_UserFound()
    {
        _fixture.Customize<Entities.User>(x => x.With(u => u.Email, "john.doe@email.com"));
        var user = _fixture.Create<Entities.User>();

        _repository.Setup(x => x.GetAsync(It.IsAny<int>(), CancellationToken.None)).ReturnsAsync(user);
        var useCase = new DeleteUserUseCase(_repository.Object, _unitOfWork.Object);

        await useCase.Execute(user.Id, CancellationToken.None);

        _repository.Verify(x => x.Delete(user), Times.Once);
    }
}
