using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyCheck.Application.Features.BudgetVersions;
using MoneyCheck.Application.Features.BudgetVersions.Commands.CreateBudgetVersion;
using MoneyCheck.Application.Features.BudgetVersions.Commands.DeleteBudgetVersion;
using MoneyCheck.Application.Features.BudgetVersions.Commands.UpdateBudgetVersion;
using MoneyCheck.Application.Features.BudgetVersions.Queries.GetVersionByBudget;
using MoneyCheck.Application.Features.BudgetYears;
using MoneyCheck.Application.Models.Enteties;

namespace MoneyCheck.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
  public class BudgetVersionController(IMediator mediator) : ControllerBase
  {
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<BudgetVersionDto>> GetBudgetVersionsByBudget(int id)
    {
      var budgetVersion = await _mediator.Send(new GetVersionByBudgetYearQuery() { Id = id });
      return Ok(budgetVersion);
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] ManageBudgetYear manageBudgetYear)
    {
      var createBudgetVersion = new CreateBudgetVersion
      {
        BudgetYearId = manageBudgetYear.BudgetYear.Id,
        Copy = manageBudgetYear.Copy,
        Currencies = manageBudgetYear.Currencies,
      };

      await _mediator.Send(createBudgetVersion);
      return NoContent();
    }

    [HttpPut]
    public async Task<ActionResult> Update([FromBody] ManageBudgetYear manageBudgetYear)
    {
      var updateBudgetVersion = new UpdateBudgetVersion
      {
        Version = manageBudgetYear.Version,
        Currencies = manageBudgetYear.Currencies,
      };

      await _mediator.Send(updateBudgetVersion);
      return NoContent();
    }

    [HttpDelete]
    public async Task<ActionResult> Delete([FromBody] BudgetYearDto budgetYear)
    {
      await _mediator.Send(new DeleteBudgetVersion() { Id = budgetYear.Id });
      return NoContent();
    }
  }
}