using FluentValidation.Results;

namespace MoneyCheck.Application.Exceptions
{
  public class ValidationException : Exception
  {
    public string ValdationError { get; set; }

    public ValidationException(ValidationResult validationResult)
    {
      ValdationError = "";

      if (validationResult.Errors.Count > 0)
        ValdationError = validationResult.Errors[0].ErrorMessage;
    }
  }
}