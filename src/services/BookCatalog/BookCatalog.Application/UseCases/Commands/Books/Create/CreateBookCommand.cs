using BookCatalog.Application.DTOs.Books.Requests;
using BookCatalog.Application.DTOs.Books.Responses;
using Shared.CQRS.Commands;
using Shared.ErrorHandling;

namespace BookCatalog.Application.UseCases.Commands.Books.Create;

public record CreateBookCommand(CreateBookRequest Request) : ICommand<Result<BookDto>>;