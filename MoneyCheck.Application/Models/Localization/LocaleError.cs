using System.Text.Json;

namespace MoneyCheck.Application.Models.Localization
{
  public class LocaleError(string localeKey, params string?[] localeParams)
  {
    public string LocaleKey { get; set; } = localeKey;
    public List<string> LocaleParams { get; set; } = [.. localeParams];

    public string ToJson()
    {
      return JsonSerializer.Serialize(this);
    }
  }
}