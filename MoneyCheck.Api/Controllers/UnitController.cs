using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyCheck.Application.Features.Units;
using MoneyCheck.Application.Features.Units.Commands.CreateUnit;
using MoneyCheck.Application.Features.Units.Commands.DeleteUnit;
using MoneyCheck.Application.Features.Units.Commands.UpdateUnit;
using MoneyCheck.Application.Features.Units.Queries.GetUnitsByBudget;

namespace MoneyCheck.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
  public class UnitController(IMediator mediator) : ControllerBase
  {
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<UnitDto>> GetUnitsByBudgetId(int id)
    {
      var unit = await _mediator.Send(new GetUnitsByBudgetQuery() { Id = id });
      return Ok(unit);
    }

    [HttpPost]
    public async Task<ActionResult<UnitDto>> Add([FromBody] CreateUnit createUnit)
    {
      var unit = await _mediator.Send(createUnit);
      return Ok(unit);
    }

    [HttpPut]
    public async Task<ActionResult<UnitDto>> Update([FromBody] UpdateUnit updateUnit)
    {
      var unit = await _mediator.Send(updateUnit);
      return Ok(unit);
    }

    [HttpDelete]
    public async Task<ActionResult<UnitDto>> Delete([FromBody] DeleteUnit deleteUnit)
    {
      var unit = await _mediator.Send(new DeleteUnit() { Id = deleteUnit.Id });
      return Ok(unit);
    }
  }
}