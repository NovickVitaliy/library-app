using BookCatalog.Application.DTOs.Books.Responses;
using Shared.CQRS.Queries;
using Shared.ErrorHandling;

namespace BookCatalog.Application.UseCases.Queries.Books.GetById;

public record GetBookByIdQuery(Guid BookId) : IQuery<Result<BookDto>>;