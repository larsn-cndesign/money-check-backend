using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyCheck.Application.Features.Budgets;
using MoneyCheck.Application.Features.Budgets.Commands.CreateBudget;
using MoneyCheck.Application.Features.Budgets.Commands.UpdateBudget;
using MoneyCheck.Application.Features.Budgets.Queries.GetBudgetList;
using MoneyCheck.Application.Features.Budgets.Queries.GetBudgetState;

namespace MoneyCheck.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
  public class BudgetController(IMediator mediator) : ControllerBase
  {
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BudgetDto>>> GetBudgets()
    {
      var budget = await _mediator.Send(new GetBudgetListQuery());
      return Ok(budget);
    } 

    [HttpGet("state")]
    public async Task<ActionResult<BudgetState>> GetBudgetState()
    {
      var budget = await _mediator.Send(new GetBudgetStateQuery());
      return Ok(budget);
    }

    [HttpPost]
    public async Task<ActionResult<BudgetDto>> Add([FromBody] CreateBudget createBudget)
    {
      var budget = await _mediator.Send(createBudget);
      return Ok(budget);
    }

    [HttpPut]
    public async Task<ActionResult<BudgetDto>> Update([FromBody] UpdateBudget updateBudget)
    {
      var budget = await _mediator.Send(updateBudget);
      return Ok(budget);
    }
  }
}