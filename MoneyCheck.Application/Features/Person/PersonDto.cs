using MoneyCheck.Application.Contracts;
using MoneyCheck.Application.Features.Trips;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCheck.Application.Features.Person
{
  public class PersonDto
  {
    public string PersonName { get; set; } = string.Empty;
    public string HashedPassword { get; set; } = string.Empty;
    public bool IsAdmin { get; set; }
  }
}