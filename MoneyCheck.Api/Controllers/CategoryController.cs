using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyCheck.Application.Features.Categories;
using MoneyCheck.Application.Features.Categories.Commands.CreateCategory;
using MoneyCheck.Application.Features.Categories.Commands.DeleteCategory;
using MoneyCheck.Application.Features.Categories.Commands.UpdateCategory;
using MoneyCheck.Application.Features.Categories.Queries.GetCategoriesByBudget;

namespace MoneyCheck.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
  public class CategoryController(IMediator mediator) : ControllerBase
  {
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<CategoryDto>> GetCategoriesByBudget(int id)
    {
      var category = await _mediator.Send(new GetCategoriesByBudgetQuery() { Id = id });
      return Ok(category);
    }

    [HttpPost]
    public async Task<ActionResult<CategoryDto>> Add([FromBody] CreateCategory createCategory)
    {
      var category = await _mediator.Send(createCategory);
      return Ok(category);
    }

    [HttpPut]
    public async Task<ActionResult<CategoryDto>> Update([FromBody] UpdateCategory updateCategory)
    {
      var category = await _mediator.Send(updateCategory);
      return Ok(category);
    }

    [HttpDelete]
    public async Task<ActionResult<CategoryDto>> Delete([FromBody] DeleteCategory deleteCategory)
    {
      var category = await _mediator.Send(new DeleteCategory() { Id = deleteCategory.Id });
      return Ok(category);
    }
  }
}