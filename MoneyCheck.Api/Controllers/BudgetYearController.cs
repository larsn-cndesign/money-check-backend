using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyCheck.Application.Features.BudgetYears;
using MoneyCheck.Application.Features.BudgetYears.Commands.CreateBudgetYear;
using MoneyCheck.Application.Features.BudgetYears.Commands.DeleteBudgetYear;
using MoneyCheck.Application.Features.BudgetYears.Queries.GetBudgetYearsByBudget;
using MoneyCheck.Application.Models.Enteties;

namespace MoneyCheck.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
  public class BudgetYearController(IMediator mediator) : ControllerBase
  {
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<ManageBudgetYear>> GetBudgetYearsByBudget(int id)
    {
      var budgetYear = await _mediator.Send(new GetBudgetYearsByBudgetQuery() { Id = id });
      return Ok(budgetYear);
    }

    [HttpPost]
    public async Task<ActionResult<BudgetYearDto>> Add([FromBody] ManageBudgetYear manageBudgetYear)
    {
      var createBudgetYear = new CreateBudgetYear
      {
        BudgetId = manageBudgetYear.BudgetYear.BudgetId,
        Year = manageBudgetYear.BudgetYear.Year,
        Copy = manageBudgetYear.Copy,
        Currencies = manageBudgetYear.Currencies,
      };

      var budgetYear = await _mediator.Send(createBudgetYear);
      return Ok(budgetYear);
    }

    [HttpDelete]
    public async Task<ActionResult> Delete([FromBody] DeleteBudgetYear deleteBudgetYear)
    {
      await _mediator.Send(new DeleteBudgetYear() { Id = deleteBudgetYear.Id });
      return NoContent();
    }
  }
}