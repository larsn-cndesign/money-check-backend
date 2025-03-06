namespace MoneyCheck.Application.Models.Mail
{
  public class EmailSettings
  {
    public string To { get; set; } = string.Empty;
    public string From { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; }
    public string TestPickupPath { get; set; } = string.Empty;
  }
}