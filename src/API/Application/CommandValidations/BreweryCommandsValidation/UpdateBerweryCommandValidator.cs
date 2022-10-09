using API.Application.Commands.BreweryCommands;

namespace API.Application.CommandValidations.BreweryCommandsValidation;

public class UpdateBerweryCommandValidator : AbstractValidator<UpdateBreweryCommand>
{
    public UpdateBerweryCommandValidator()
    {
        RuleFor(command => command.Id).NotEmpty();
        RuleFor(command => command.Name).NotEmpty().NotNull();
    }
}
