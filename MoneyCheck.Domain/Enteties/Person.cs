using MoneyCheck.Domain.Common;

namespace MoneyCheck.Domain.Enteties
{
  public class Person : AuditableEntity
  {
    public int Id { get; set; }
    public string PersonName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string HashedPassword { get; set; } = string.Empty;
    public string PasswordSalt { get; set; } = string.Empty;
    public bool IsAdmin { get; set; }
  }
}