using BookCatalog.Application.Contracts.Repositories;
using BookCatalog.Application.DTOs.Books.Responses;
using BookCatalog.Application.Mappers;
using Shared.CQRS.Queries;
using Shared.DTOs;
using Shared.ErrorHandling;

namespace BookCatalog.Application.UseCases.Queries.Books.Get;

public sealed class GetBooksQueryHandler : IQueryHandler<GetBooksQuery, Result<PaginationResult<BookDto>>>
{
    private readonly IBookRepository _bookRepository;
    
    public GetBooksQueryHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }
    
    public async Task<Result<PaginationResult<BookDto>>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        var paginationResult = await _bookRepository.GetAsync(request.Request.PageNumber, request.Request.PageSize, cancellationToken);

        return Result<PaginationResult<BookDto>>.Ok(paginationResult.ToPaginatedDtos(BookMapper.ToDto));
    }
}