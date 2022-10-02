using API.Application.Commands.BreweryCommands;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BreweriesController : ControllerBase
{
    private readonly IMediator _bus;

    public BreweriesController(IMediator bus) => _bus = bus ?? throw new ArgumentNullException(nameof(bus));

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Post([FromBody] CreateBreweryCommand createBreweryCommand) 
        => await _bus.Send(createBreweryCommand) ? Ok() : BadRequest();
}
