using MoneyCheck.Application.Models.Mail;

namespace MoneyCheck.Application.Contracts.Infrastructure
{
  public interface IEmailService
  {
    Task<bool> SendEmail(Email email);
  }
}