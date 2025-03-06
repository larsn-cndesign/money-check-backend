﻿namespace MoneyCheck.Application.Models.Auth
{
  public class JwtTokenSettings
  {
    public string Issuer { get; set; } = "";
    public string Audience { get; set; } = "";
    public string SecretKey { get; set; } = "";
  }
}