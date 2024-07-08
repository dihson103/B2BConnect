using System.Net;

namespace Domain.Abstractioins.Exceptions;
public class MyUnauthorizedException : MyException
{
    public MyUnauthorizedException(string message = "Không được xác thực.") 
        : base((int)HttpStatusCode.Unauthorized, message)
    {
        
    }

}