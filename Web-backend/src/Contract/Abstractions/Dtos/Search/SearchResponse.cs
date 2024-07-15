namespace Contract.Abstractions.Dtos.Search;
public record SearchResponse<T>
{
    public int CurrentPage { get; init; }
    public int TotalPages { get; init; }
    public bool HasNextPage { get; init; }
    public bool HasPreviousPage { get; init; }
    public int TotalItems { get; init; }
    public T Data { get; init; }

    public SearchResponse(int currentPage, int totalPages, int totalItems, T data)
    {
        CurrentPage = currentPage;
        TotalPages = totalPages;
        HasNextPage = currentPage < totalPages;
        HasPreviousPage = currentPage > 1;
        TotalItems = totalItems;
        Data = data;
    }
}
