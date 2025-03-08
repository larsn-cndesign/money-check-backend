using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyCheck.Application.Features.ActualItems;
using MoneyCheck.Application.Features.ActualItems.Commands.CreateActualItem;
using MoneyCheck.Application.Features.ActualItems.Commands.DeleteActualItem;
using MoneyCheck.Application.Features.ActualItems.Commands.UpdateActualItem;
using MoneyCheck.Application.Features.ActualItems.Queries.GetActualItem;
using MoneyCheck.Application.Models.Enteties;

namespace MoneyCheck.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
  public class ActualItemController(IMediator mediator) : ControllerBase
  {
    private readonly IMediator _mediator = mediator;

    [HttpPost("get")]
    public async Task<ActionResult<ManageActualItem>> GetActualItems([FromBody] ItemFilter filter)
    {
      var actualItem = await _mediator.Send(new GetActualItemQuery() { Filter = filter });
      return Ok(actualItem);
    }

    [HttpPost]
    public async Task<ActionResult<ActualItemDto>> Add([FromBody] ActualItemDto actualItem)
    {
      var createActualItem = new CreateActualItem
      {
        BudgetId = actualItem.BudgetId,
        CategoryId = actualItem.CategoryId,
        TripId = actualItem.TripId,
        PurchaseDate = actualItem.PurchaseDate,
        CurrencyCode = actualItem.CurrencyCode,
        Amount = actualItem.Amount,
        Note = actualItem.Note,
      };

      var actualItemDto = await _mediator.Send(createActualItem);

      // Put back names
      actualItemDto.CategoryName = actualItem.CategoryName;
      actualItemDto.TripName = actualItem.TripName;

      return Ok(actualItemDto);
    }

    [HttpPut]
    public async Task<ActionResult<ActualItemDto>> Update([FromBody] ActualItemDto actualItem)
    {
      var updateActualItem = new UpdateActualItem
      {
        Id = actualItem.Id,
        CategoryId = actualItem.CategoryId,
        TripId = actualItem.TripId,
        PurchaseDate = actualItem.PurchaseDate,
        CurrencyCode = actualItem.CurrencyCode,
        Amount = actualItem.Amount,
        Note = actualItem.Note,
      };

      await _mediator.Send(updateActualItem);
      return Ok(actualItem);
    }

    [HttpDelete]
    public async Task<ActionResult<ActualItemDto>> Delete([FromBody] ActualItemDto actualItem)
    {
      await _mediator.Send(new DeleteActualItem() { Id = actualItem.Id });
      return Ok(actualItem);
    }
  }
}