using MediatR;

namespace MoneyCheck.Application.Features.Person.Queries.GetPerson
{
  public class GetPersonQuery : IRequest<PersonDto>
  {
    public string Email { get; set; } = string.Empty;
  }
}