using MediatR;
using MoneyCheck.Application.Models.Enteties;

namespace MoneyCheck.Application.Features.VarianceItems.Queries
{
  public class GetVarianceItemQuery : IRequest<BudgetVariance>
  {
    public ItemFilter Filter { get; set; } = new();
  }
}

