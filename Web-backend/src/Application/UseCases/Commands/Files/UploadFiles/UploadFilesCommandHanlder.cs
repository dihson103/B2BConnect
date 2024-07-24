using Application.Abstractions.Services;
using Contract.Abstractions.Dtos.Results;
using Contract.Abstractions.Messages;
using Contract.Services.File.UploadFiles;

namespace Application.UseCases.Commands.Files.UploadFiles;
internal class UploadFilesCommandHanlder(IFileService _fileService) : ICommandHandler<UploadFilesCommand, List<string>>
{
    public async Task<Result.Success<List<string>>> Handle(UploadFilesCommand request, CancellationToken cancellationToken)
    {
        var fileNames = new List<string>();

        foreach (var file in request.ReceivedFiles)
        {
            var uploadedFileName = await _fileService.Upload(file);
            fileNames.Add(uploadedFileName);
        }

        return Result.Success<List<string>>.ActionCommand(fileNames);
    }
}
