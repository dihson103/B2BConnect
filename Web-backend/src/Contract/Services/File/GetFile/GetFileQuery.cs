using Contract.Abstractions.Messages;

namespace Contract.Services.File.GetFile;
public record GetFileQuery(string fileName) : IQuery<byte[]>;
