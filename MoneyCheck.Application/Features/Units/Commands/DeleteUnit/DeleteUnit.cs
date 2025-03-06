using MediatR;

namespace MoneyCheck.Application.Features.Units.Commands.DeleteUnit
{
  public class DeleteUnit : IRequest<UnitDto>
  {
    public int Id { get; set; }
  }
}