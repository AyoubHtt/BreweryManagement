using Domain.AggregatesModel.BreweryAggregate;

namespace API.Application.Commands.BreweryCommands;

public record UpdateBreweryCommand(Guid Id, string Name) : IRequest<bool>;

public class UpdateBreweryCommandHandler : IRequestHandler<UpdateBreweryCommand, bool>
{
    private readonly IBreweryRepository _breweryRepository;

    public UpdateBreweryCommandHandler(IBreweryRepository breweryRepository) => _breweryRepository = breweryRepository ?? throw new ArgumentNullException(nameof(breweryRepository));

    public async Task<bool> Handle(UpdateBreweryCommand request, CancellationToken cancellationToken)
    {
        if (await _breweryRepository.CheckAnotherBreweryExistByNameAsync(request.Id, request.Name, cancellationToken)) throw new DomainException("Brewery name already exsit");

        var exsitingBrewery = await _breweryRepository.GetByIdAsync(request.Id, cancellationToken) ?? throw new DomainException("Invalid brewery id");

        exsitingBrewery.Update(request.Name);

        _breweryRepository.Update(exsitingBrewery);

        return await _breweryRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
