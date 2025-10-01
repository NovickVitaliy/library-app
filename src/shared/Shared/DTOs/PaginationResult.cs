namespace Shared.DTOs;

public record PaginationResult<TEntity>(
    TEntity[] Entities, 
    long TotalCount,
    int CurrentPage,
    decimal TotalPages,
    int PageSize);