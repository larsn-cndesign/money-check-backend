using MediatR;
using MoneyCheck.Application.Contracts.Persistance;
using MoneyCheck.Application.Exceptions;
using MoneyCheck.Application.Models.Enteties;
using MoneyCheck.Application.Models.Localization;

namespace MoneyCheck.Application.Features.ActualItems.Queries.GetActualItem
{
  public class GetActualItemQueryHandler(IActualItemRepository actualItemRepository) :
    IRequestHandler<GetActualItemQuery, ManageActualItem>
  {
    private readonly IActualItemRepository _actualItemRepository = actualItemRepository;

    public async Task<ManageActualItem> Handle(GetActualItemQuery request, CancellationToken cancellationToken)
    {
      if (request.Filter == null)
        throw new BadRequestException(new LocaleError(LocaleErrorKey.NotFound, LocaleErrorKey.InvalidFilter).ToJson());

      return await _actualItemRepository.GetActualItems(request.Filter);
    }
  }
}