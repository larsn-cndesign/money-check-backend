using Microsoft.Extensions.Options;
using MoneyCheck.Application.Contracts.Infrastructure;
using MoneyCheck.Application.Models.Mail;

namespace MoneyCheck.Infrastructure.Mail
{
  public class EmailService(IOptions<EmailSettings> mailSettings) : IEmailService
  {
    public EmailSettings _emailSettings { get; } = mailSettings.Value;

    public Task<bool> SendEmail(Email email)
    {
      // TODO
      throw new NotImplementedException();
    }
  }
}