using Contract.Abstractions.Messages;

namespace Contract.Services.Industry.SearchIndustries;
public record SearchIndustriesQuery(string? SearchTerm) : IQuery<List<IndustryResponse>>;
