using Shared.CQRS.Commands;
using Shared.ErrorHandling;

namespace BookCatalog.Application.UseCases.Commands.Genres.Delete;

public record DeleteGenreCommand(Guid GenreId) : ICommand<Result<bool>>;