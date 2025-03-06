using MoneyCheck.Application.Models.Auth;

namespace MoneyCheck.Application.Contracts.Authentication
{
  public interface IAuthService
  {
    string GetBearerToken();
  }
}