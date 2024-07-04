using Contract.Abstractions.Messages;
using Microsoft.AspNetCore.Http;

namespace Contract.Services.File.UploadFile;
public record UploadFileCommand(IFormFile File) : ICommand<UploadFileResponse>;
