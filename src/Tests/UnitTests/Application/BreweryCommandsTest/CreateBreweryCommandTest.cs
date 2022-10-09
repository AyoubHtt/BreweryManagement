using API.Application.Commands.BreweryCommands;
using AutoFixture;
using Domain.AggregatesModel.BreweryAggregate;
using Domain.Events.BreweryEvents;
using Domain.Exceptions;
using Moq;

namespace UnitTests.Application.BreweryCommandsTest;

public class CreateBreweryCommandTest
{
    private readonly Mock<IBreweryRepository> _breweryRepository;

    public CreateBreweryCommandTest() => _breweryRepository = new Mock<IBreweryRepository>();

    [Fact]
    public async Task Handler_throws_exception_if_brewery_name_already_in_use()
    {
        //Arrange
        var fixture = new Fixture();
        var request = fixture.Create<CreateBreweryCommand>();
        _breweryRepository.Setup(repo => repo.CheckBreweryExistByNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);

        //Act
        var handler = new CreateBreweryCommandHandler(_breweryRepository.Object);
        var cltToken = new CancellationToken();

        //Assert
        var exception = await Assert.ThrowsAsync<DomainException>(() => handler.Handle(request, cltToken));
        Assert.Equal("Brewery name already exsit", exception.Message);
    }

    [Fact]
    public async Task Handler_should_return_true_with_expected_result()
    {
        //Arrange
        var fixture = new Fixture();
        var request = fixture.Create<CreateBreweryCommand>();

        _breweryRepository.Setup(repo => repo.CheckBreweryExistByNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(false);
        _breweryRepository.Setup(repo => repo.UnitOfWork.SaveEntitiesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(true);

        Brewery savedBrewer = default!;
        _breweryRepository.Setup(repo => repo.AddAsync(It.IsAny<Brewery>(), It.IsAny<CancellationToken>())).Callback(new InvocationAction(i => savedBrewer = (Brewery)i.Arguments[0]));

        //Act
        var handler = new CreateBreweryCommandHandler(_breweryRepository.Object);
        var cltToken = new CancellationToken();
        var result = await handler.Handle(request, cltToken);

        //Assert
        Assert.True(result);
        Assert.Equal(request.Name, savedBrewer.Name);
        Assert.Single(savedBrewer.GetDomainEvents());
        Assert.IsType<BreweryCreatedEvent>(savedBrewer.GetDomainEvents().Single());
    }
}
