using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MoneyCheck.Application.Contracts.Authentication;
using MoneyCheck.Application.Models.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace MoneyCheck.Auth.Auth
{
  public class AuthService(IOptions<JwtTokenSettings> jwtTokenSettings) : IAuthService
  {
    public JwtTokenSettings jwtToken { get; } = jwtTokenSettings.Value;

    public string GetBearerToken()
    {
      var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtToken.SecretKey));
      var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
      var tokenOptions = new JwtSecurityToken(
        issuer: jwtToken.Issuer,
        audience: jwtToken.Audience,
        claims: [],
        expires: DateTime.Now.AddDays(7), // Change to test cookie expiration
        signingCredentials: signinCredentials
      );
      return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }
  }
}