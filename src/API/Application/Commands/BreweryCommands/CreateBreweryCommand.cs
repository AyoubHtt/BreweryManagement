using Domain.AggregatesModel.BreweryAggregate;

namespace API.Application.Commands.BreweryCommands;

public record CreateBreweryCommand(string Name) : IRequest<bool>;

public class CreateBreweryCommandHandler : IRequestHandler<CreateBreweryCommand, bool>
{
    private readonly IBreweryRepository _breweryRepository;

    public CreateBreweryCommandHandler(IBreweryRepository breweryRepository) => _breweryRepository = breweryRepository ?? throw new ArgumentNullException(nameof(breweryRepository));

    public async Task<bool> Handle(CreateBreweryCommand request, CancellationToken cancellationToken)
    {
        if (await _breweryRepository.CheckBreweryExistByNameAsync(request.Name, cancellationToken)) throw new DomainException("Brewery name already exsit");

        var newBrewery = new Brewery(request.Name);

        await _breweryRepository.AddAsync(newBrewery, cancellationToken);

        return await _breweryRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
