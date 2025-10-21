using BookCatalog.Application.Contracts.Repositories;
using BookCatalog.Application.DTOs.Books.Responses;
using BookCatalog.Application.Mappers;
using BookCatalog.Domain.Models;
using Shared.CQRS.Queries;
using Shared.ErrorHandling;

namespace BookCatalog.Application.UseCases.Queries.Books.GetById;

public sealed class GetBookByIdQueryHandler : IQueryHandler<GetBookByIdQuery, Result<BookDto>>
{
    private readonly IBookRepository _bookRepository;
    
    public GetBookByIdQueryHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }
    
    public async Task<Result<BookDto>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIdAsync(request.BookId, cancellationToken);

        return book is null
            ? Result<BookDto>.NotFound(key: request.BookId, entityName: nameof(Book))
            : Result<BookDto>.Ok(BookMapper.ToDto(book));
    }
}