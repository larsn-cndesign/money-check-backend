using FluentValidation;
using MoneyCheck.Application.Contracts.Persistance;

namespace MoneyCheck.Application.Features.ActualItems.Commands
{
  public class ActualItemCommandValidator : AbstractValidator<ActualItemBase>
  {
    private readonly IActualItemRepository _actualItemRepository;

    public ActualItemCommandValidator(IActualItemRepository actualItemRepository)
    {
      _actualItemRepository = actualItemRepository;

      ClassLevelCascadeMode = CascadeMode.Stop;
      RuleLevelCascadeMode = CascadeMode.Stop;

      // TODO Add validations
    }
  }
}