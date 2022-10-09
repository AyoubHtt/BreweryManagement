using API.Application.Commands.BreweryCommands;

namespace API.Application.CommandValidations.BreweryCommandsValidation;

public class DeleteBreweryCommandValidator : AbstractValidator<DeleteBreweryCommand>
{
    public DeleteBreweryCommandValidator() => RuleFor(command => command.Id).NotEmpty();
}
