namespace Shared.DTOs;

public record PaginationResult<TEntity>(
    TEntity[] Entities, 
    long TotalCount,
    int CurrentPage,
    decimal TotalPages,
    int PageSize)
{
    public bool HasNext => CurrentPage < PageSize;
    public bool HasPrevious => CurrentPage > 1;
}