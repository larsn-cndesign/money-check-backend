using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyCheck.Application.Features.VarianceItems.Queries;
using MoneyCheck.Application.Models.Enteties;

namespace MoneyCheck.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
  public class VarianceItemController(IMediator mediator) : ControllerBase
  {
    private readonly IMediator _mediator = mediator;

    [HttpPost("get")]
    public async Task<ActionResult<BudgetVariance>> GetVarianceItems([FromBody] ItemFilter filter)
    {
      var varianceItem = await _mediator.Send(new GetVarianceItemQuery() { Filter = filter });
      return Ok(varianceItem);
    }
  }
}