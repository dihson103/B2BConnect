using System.Net;

namespace Domain.Abstractioins.Exceptions;
public class MyBadRequestException : MyException
{
    public MyBadRequestException(string message = "Yêu cầu không chính xác") 
        : base((int) HttpStatusCode.BadRequest, message)
    {
    }
}
