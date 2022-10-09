using Domain.AggregatesModel.BreweryAggregate;

namespace API.Application.Commands.BreweryCommands;

public record DeleteBreweryCommand(Guid Id) : IRequest<bool>;

public class DeleteBreweryCommandHandler : IRequestHandler<DeleteBreweryCommand, bool>
{
    private readonly IBreweryRepository _breweryRepository;

    public DeleteBreweryCommandHandler(IBreweryRepository breweryRepository) => _breweryRepository = breweryRepository ?? throw new ArgumentNullException(nameof(breweryRepository));

    public async Task<bool> Handle(DeleteBreweryCommand request, CancellationToken cancellationToken)
    {
        var exsitingBrewery = await _breweryRepository.GetByIdAsync(request.Id, cancellationToken) ?? throw new DomainException("Invalid brewery id");

        exsitingBrewery.Delete();

        _breweryRepository.Update(exsitingBrewery);

        return await _breweryRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
