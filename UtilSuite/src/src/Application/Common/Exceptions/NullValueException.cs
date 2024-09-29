namespace Application.Common.Exceptions;

public class NullValueException : Exception
{
    public NullValueException() : base() { }

    public NullValueException(string message) : base(message) { }
}
