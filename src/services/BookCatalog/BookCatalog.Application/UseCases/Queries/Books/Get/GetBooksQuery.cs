using BookCatalog.Application.DTOs.Books.Responses;
using BookCatalog.Domain.Models;
using Shared.CQRS.Queries;
using Shared.DTOs;
using Shared.ErrorHandling;

namespace BookCatalog.Application.UseCases.Queries.Books.Get;

public record GetBooksQuery(PaginationRequest Request) : IQuery<Result<PaginationResult<BookDto>>>;