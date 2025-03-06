using Microsoft.EntityFrameworkCore;
using MoneyCheck.Application.Contracts.Persistance;
using MoneyCheck.Domain.Enteties;
using MoneyCheck.Persistance.Contexts;

namespace MoneyCheck.Persistance.Repositories
{
  public class PersonRepository(ApplicationDbContext dbContext) : BaseRepository<Person>(dbContext), IPersonRepository
  {
    public async Task<Person?> GetByEmail(string email)
    {
      var person = await _dbContext.Persons.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email);
      return person;
    }
  }
}