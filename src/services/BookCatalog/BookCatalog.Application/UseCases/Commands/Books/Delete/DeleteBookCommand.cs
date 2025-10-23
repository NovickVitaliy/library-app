using Shared.CQRS.Commands;
using Shared.ErrorHandling;

namespace BookCatalog.Application.UseCases.Commands.Books.Delete;

public record DeleteBookCommand(Guid BookId) : ICommand<Result<bool>>;