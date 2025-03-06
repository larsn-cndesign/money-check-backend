using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyCheck.Application.Features.Trips;
using MoneyCheck.Application.Features.Trips.Commands.CreateTrip;
using MoneyCheck.Application.Features.Trips.Commands.DeleteTrip;
using MoneyCheck.Application.Features.Trips.Commands.UpdateTrip;
using MoneyCheck.Application.Features.Trips.Queries.GetTripsByBudget;

namespace MoneyCheck.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
  public class TripController(IMediator mediator) : ControllerBase
  {
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<TripDto>> GetTripsByBudget(int id)
    {
      var trip = await _mediator.Send(new GetTripsByBudgetQuery() { Id = id });
      return Ok(trip);
    }

    [HttpPost]
    public async Task<ActionResult<TripDto>> Add([FromBody] CreateTrip createTrip)
    {
      var trip = await _mediator.Send(createTrip);
      return Ok(trip);
    }

    [HttpPut]
    public async Task<ActionResult<TripDto>> Update([FromBody] UpdateTrip updateTrip)
    {
      var trip = await _mediator.Send(updateTrip);
      return Ok(trip);
    }

    [HttpDelete]
    public async Task<ActionResult<TripDto>> Delete([FromBody] DeleteTrip deleteTrip)
    {
      var trip = await _mediator.Send(new DeleteTrip() { Id = deleteTrip.Id });
      return Ok(trip);
    }
  }
}