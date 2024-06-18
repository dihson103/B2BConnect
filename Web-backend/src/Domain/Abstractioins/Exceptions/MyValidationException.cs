using System.Net;

namespace Domain.Abstractioins.Exceptions;
public class MyValidationException : MyException
{
    public MyValidationException(object error) : base(
        (int) HttpStatusCode.BadRequest,
        "Validation Error", 
        error)
    {
    }
}
