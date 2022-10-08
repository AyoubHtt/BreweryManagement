using API.Application.Commands.BreweryCommands;

namespace API.Application.CommandValidations.BreweryCommandsValidation;

public class CreateBreweryCommandValidator : AbstractValidator<CreateBreweryCommand>
{
    public CreateBreweryCommandValidator() => RuleFor(x => x.Name).NotEmpty().NotNull();
}
