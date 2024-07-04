using System.Net;

namespace Domain.Abstractioins.Exceptions;
public class UploadFileException : MyException
{
    public UploadFileException(string message = "Quá trình tải file xảy ra lỗi")
        : base((int) HttpStatusCode.BadRequest, message)
    {
    }
}
