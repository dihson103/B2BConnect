namespace Domain.Abstractioins.Exceptions;
public class MyException : Exception
{
    public int StatusCode { get; private set; }
    public object Error { get; private set; }
    public MyException(int code, string message) : base(message)
    {
        StatusCode = code;
        Error = null;
    }

    public MyException(int code, string message, object error) : base(message)
    {
        StatusCode = code;
        Error = error;
    }
}
