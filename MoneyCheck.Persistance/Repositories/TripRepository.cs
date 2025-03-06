using Microsoft.EntityFrameworkCore;
using MoneyCheck.Application.Contracts.Persistance;
using MoneyCheck.Domain.Enteties;
using MoneyCheck.Persistance.Contexts;

namespace MoneyCheck.Persistance.Repositories
{
  public class TripRepository(ApplicationDbContext dbContext) : BaseRepository<Trip>(dbContext), ITripRepository
  {
  }
}