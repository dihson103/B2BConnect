using Application.Abstractions.Services;
using Contract.Abstractions.Dtos.Results;
using Contract.Abstractions.Messages;
using Contract.Services.File.GetFile;
using Domain.Abstractioins.Exceptions;

namespace Application.UseCases.Queries.Files.GetFile;
internal class GetFileQueryHandler(IFileService _fileService)
    : IQueryHandler<GetFileQuery, byte[]>
{
    public async Task<Result.Success<byte[]>> Handle(GetFileQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.fileName))
        {
            throw new MyNotFoundException("Không tìm thấy tên file");
        }

        var result = await _fileService.GetFile(request.fileName);

        return Result.Success<byte[]>.Get(result);
    }
}
