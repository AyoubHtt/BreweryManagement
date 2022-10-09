using API.Application.Commands.BreweryCommands;
using API.Application.Queries.BreweryQueries;
using Domain.AggregatesModel.BreweryAggregate;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BreweriesController : ControllerBase
{
    private readonly IMediator _bus;

    public BreweriesController(IMediator bus) => _bus = bus ?? throw new ArgumentNullException(nameof(bus));

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Brewery>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Get(ODataQueryOptions<Brewery>? query)
        => Ok(await _bus.Send(new BreweryQuery(query)));

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Post([FromBody] CreateBreweryCommand createBreweryCommand)
        => await _bus.Send(createBreweryCommand) ? Ok() : BadRequest();

    [HttpPut]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Put([FromBody] UpdateBreweryCommand updateBerweryCommand)
        => await _bus.Send(updateBerweryCommand) ? Ok() : BadRequest();

    [HttpDelete]
    [Route("{Id:guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Delete([FromRoute] DeleteBreweryCommand deleteBreweryCommand)
        => await _bus.Send(deleteBreweryCommand) ? Ok() : BadRequest();
}
