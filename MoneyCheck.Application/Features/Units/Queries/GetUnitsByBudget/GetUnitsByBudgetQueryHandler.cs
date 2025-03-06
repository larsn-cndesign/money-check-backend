using MediatR;
using MoneyCheck.Application.Contracts.Persistance;
using MoneyCheck.Application.Mapping;

namespace MoneyCheck.Application.Features.Units.Queries.GetUnitsByBudget
{
  public class GetUnitsByBudgetQueryHandler(ISharedRepository sharedRepository) :
    IRequestHandler<GetUnitsByBudgetQuery, IEnumerable<UnitDto>>
  {
    private readonly ISharedRepository _sharedRepository = sharedRepository;

    public async Task<IEnumerable<UnitDto>> Handle(GetUnitsByBudgetQuery request, CancellationToken cancellationToken)
    {
      var units = await _sharedRepository.GetUnitsByBudgetId(request.Id);

      return EntityMapper.UnitsToDto(units);
    }
  }
}