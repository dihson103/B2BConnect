using Contract.Abstractions.Messages;
using Contract.Services.Industry.Share;

namespace Contract.Services.Industry.SearchIndustries;
public record SearchIndustriesQuery(string? SearchTerm) : IQuery<List<IndustryResponse>>;
