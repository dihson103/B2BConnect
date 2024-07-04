using Microsoft.AspNetCore.Http;

namespace Application.Abstractions.Services;
public interface IFileService
{
    Task<string> Upload(IFormFile file);
    Task<byte[]> GetFile(string fileName);
    void Delete(string fileName);
}
