namespace Contract.Abstractions.Dtos.Search;
public record SearchResponse<T>(int CurrentPage, int TotalPages, T Data);

