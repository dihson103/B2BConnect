using Contract.Abstractions.Messages;
using Microsoft.AspNetCore.Http;

namespace Contract.Services.File.UploadFiles;
public record UploadFilesCommand(IFormFileCollection ReceivedFiles) : ICommand<List<string>>;
