using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyCheck.Api.Models;
using MoneyCheck.Application.Contracts.Authentication;
using MoneyCheck.Application.Features.Person.Queries.GetPerson;

namespace MoneyCheck.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AuthenticateController(IMediator mediator, IAuthService authService) : ControllerBase
  {
    private readonly IMediator _mediator = mediator;
    private readonly IAuthService _authService = authService;

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<AppUser>> Authenticate(UserCredential credential)
    {
      var person = await _mediator.Send(new GetPersonQuery() { Email = credential.Email });

      if (person == null || !BCrypt.Net.BCrypt.Verify(credential.Password, person.HashedPassword))
        throw new UnauthorizedAccessException();

      var token = _authService.GetBearerToken();

      AppUser user = new() { Name = person.PersonName, IsAdmin = person.IsAdmin };

      Response.Headers.Append("Authorization", $"Bearer {token}"); // Set authorization header

      return Ok(user);
    }
  }
}