using System.Net;

namespace Domain.Abstractioins.Exceptions;
public class MyNotFoundException : MyException
{
    public MyNotFoundException(string message = "Không tìm thấy kết quả") : base(
        (int) HttpStatusCode.NotFound, message)
    {
    }
}
