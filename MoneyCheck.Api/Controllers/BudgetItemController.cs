using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyCheck.Application.Features.BudgetItems;
using MoneyCheck.Application.Features.BudgetItems.Commands.CreateBudgetItem;
using MoneyCheck.Application.Features.BudgetItems.Commands.DeleteBudgetItem;
using MoneyCheck.Application.Features.BudgetItems.Commands.UpdateBudgetItem;
using MoneyCheck.Application.Features.BudgetItems.Queries.GetBudgetItem;
using MoneyCheck.Application.Models.Enteties;

namespace MoneyCheck.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
  public class BudgetItemController(IMediator mediator) : ControllerBase
  {
    private readonly IMediator _mediator = mediator;

    [HttpPost("get")]
    public async Task<ActionResult<ManageBudgetItem>> GetBudgetItems([FromBody] ItemFilter filter)
    {
      var budgetItem = await _mediator.Send(new GetBudgetItemQuery() { Filter = filter });
      return Ok(budgetItem);
    }

    [HttpPost]
    public async Task<ActionResult<BudgetItemDto>> Add([FromBody] BudgetItemDto budgetItem)
    {
      var createBudgetItem = new CreateBudgetItem
      {
        VersionId = budgetItem.VersionId,
        CategoryId = budgetItem.CategoryId,
        UnitId = budgetItem.UnitId,
        CurrencyCode = budgetItem.CurrencyCode,
        UnitValue = budgetItem.UnitValue,
        Note = budgetItem.Note,
      };

      var budgetItemDto = await _mediator.Send(createBudgetItem);

      // Put back names
      budgetItemDto.CategoryName = budgetItem.CategoryName;
      budgetItemDto.UnitName = budgetItem.UnitName;

      return Ok(budgetItemDto);
    }

    [HttpPut]
    public async Task<ActionResult<BudgetItemDto>> Update([FromBody] BudgetItemDto budgetItem)
    {
      var updateBudgetItem = new UpdateBudgetItem
      {
        Id = budgetItem.Id,
        CategoryId = budgetItem.CategoryId,
        UnitId = budgetItem.UnitId,
        CurrencyCode = budgetItem.CurrencyCode,
        UnitValue = budgetItem.UnitValue,
        Note = budgetItem.Note
      };

      await _mediator.Send(updateBudgetItem);
      return Ok(budgetItem);
    }

    [HttpDelete]
    public async Task<ActionResult<BudgetItemDto>> Delete([FromBody] BudgetItemDto budgetItem)
    {
      await _mediator.Send(new DeleteBudgetItem() { Id = budgetItem.Id });
      return Ok(budgetItem);
    }
  }
}