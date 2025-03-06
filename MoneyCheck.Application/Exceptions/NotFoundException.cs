namespace MoneyCheck.Application.Exceptions
{
  public class NotFoundException : Exception
  {
    public NotFoundException(string message) : base(message)
    {
    }

    public NotFoundException(string message, string key, object value) : base(message)
    {
      Data[key] = value;
    }
  }
}