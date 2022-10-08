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

    [HttpPut]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Put([FromBody] UpdateBerweryCommand updateBerweryCommand)
        => await _bus.Send(updateBerweryCommand) ? Ok() : BadRequest();

    [HttpDelete]
    [Route("{Id:guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Delete([FromRoute] DeleteBreweryCommand deleteBreweryCommand)
        => await _bus.Send(deleteBreweryCommand) ? Ok() : BadRequest();
}
