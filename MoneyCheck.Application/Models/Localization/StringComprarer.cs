using System.Globalization;

namespace MoneyCheck.Application.Models.Localization
{
  public static class Comparers
  {
    public static readonly StringComparer Swedish = StringComparer.Create(new CultureInfo("sv-SE"), false);
  }
}