using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MoneyCheck.Application.Contracts.Authentication;
using MoneyCheck.Application.Models.Auth;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace MoneyCheck.Auth.Auth
{
  public class AuthService(IOptions<JwtTokenSettings> jwtTokenSettings) : IAuthService
  {
    public JwtTokenSettings JwtToken { get; } = jwtTokenSettings.Value;

    public string GetBearerToken()
    {
      var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtToken.SecretKey));
      var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
      var tokenOptions = new JwtSecurityToken(
        issuer: JwtToken.Issuer,
        audience: JwtToken.Audience,
        claims: [],
        expires: DateTime.Now.AddDays(7), // Change to test cookie expiration
        signingCredentials: signinCredentials
      );
      return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }
  }
}