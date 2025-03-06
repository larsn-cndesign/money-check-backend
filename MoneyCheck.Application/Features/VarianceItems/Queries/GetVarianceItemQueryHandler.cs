using MediatR;
using MoneyCheck.Application.Contracts.Persistance;
using MoneyCheck.Application.Exceptions;
using MoneyCheck.Application.Models.Enteties;
using MoneyCheck.Application.Models.Localization;

namespace MoneyCheck.Application.Features.VarianceItems.Queries
{
  public class GetVarianceItemQueryHandler(IVarianceItemRepository varianceItemRepository) :
    IRequestHandler<GetVarianceItemQuery, BudgetVariance>
  {
    private readonly IVarianceItemRepository _varianceItemRepository = varianceItemRepository;

    public async Task<BudgetVariance> Handle(GetVarianceItemQuery request, CancellationToken cancellationToken)
    {
      if (request.Filter == null)
        throw new BadRequestException(new LocaleError(LocaleErrorKey.NotFound, LocaleErrorParam.Filter).ToJson());

      return await _varianceItemRepository.GetVarianceItems(request.Filter);
    }
  }
}