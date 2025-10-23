using BookCatalog.Application.DTOs.Books.Requests;
using BookCatalog.Application.DTOs.Books.Responses;
using Shared.CQRS.Commands;
using Shared.ErrorHandling;

namespace BookCatalog.Application.UseCases.Commands.Books.Update;

public record UpdateBookCommand(Guid BookId, UpdateBookRequest Request) : ICommand<Result<BookDto>>;