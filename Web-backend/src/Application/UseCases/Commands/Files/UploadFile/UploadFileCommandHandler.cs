using Application.Abstractions.Services;
using Contract.Abstractions.Dtos.Results;
using Contract.Abstractions.Messages;
using Contract.Services.File.UploadFile;

namespace Application.UseCases.Commands.Files.UploadFile;
internal class UploadFileCommandHandler(IFileService _fileService)
    : ICommandHandler<UploadFileCommand, UploadFileResponse>
{
    public async Task<Result.Success<UploadFileResponse>> Handle(
        UploadFileCommand request, 
        CancellationToken cancellationToken)
    {
        var fileName = await _fileService.Upload(request.File);
        var uploadFileResponse = new UploadFileResponse(fileName);

        return Result.Success<UploadFileResponse>.ActionCommand(uploadFileResponse, "Upload file thành công");
    }
}
