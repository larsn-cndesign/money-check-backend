using MediatR;
using MoneyCheck.Application.Contracts.Persistance;
using MoneyCheck.Application.Mapping;

namespace MoneyCheck.Application.Features.Person.Queries.GetPerson
{
  public class GetPersonQueryHandler(IPersonRepository personRepository) : IRequestHandler<GetPersonQuery, PersonDto?>
  {
    private readonly IPersonRepository _personRepository = personRepository;

    public async Task<PersonDto?> Handle(GetPersonQuery request, CancellationToken cancellationToken)
    {
      var person = await _personRepository.GetByEmail(request.Email);
      return person != null ? EntityMapper.PersonToDto(person) : null;
    }
  }
}