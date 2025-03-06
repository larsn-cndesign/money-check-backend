using MediatR;
using MoneyCheck.Application.Contracts.Persistance;
using MoneyCheck.Application.Mapping;

namespace MoneyCheck.Application.Features.Categories.Queries.GetCategoriesByBudget
{
  public class GetCategoriesByBudgetQueryHandler(ISharedRepository sharedRepository) :
    IRequestHandler<GetCategoriesByBudgetQuery, IEnumerable<CategoryDto>>
  {
    private readonly ISharedRepository _sharedRepository = sharedRepository;

    public async Task<IEnumerable<CategoryDto>> Handle(GetCategoriesByBudgetQuery request, CancellationToken cancellationToken)
    {
      var categories = await _sharedRepository.GetCategoriesByBudgetId(request.Id);

      return EntityMapper.CategoriesToDto(categories);
    }
  }
}