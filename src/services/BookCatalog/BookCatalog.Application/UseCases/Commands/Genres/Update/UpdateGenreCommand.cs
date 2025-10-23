using BookCatalog.Application.DTOs.Genres.Requests;
using BookCatalog.Application.DTOs.Genres.Responses;
using Shared.CQRS.Commands;
using Shared.ErrorHandling;

namespace BookCatalog.Application.UseCases.Commands.Genres.Update;

public record UpdateGenreCommand(Guid GenreId, UpdateGenreRequest Request) : ICommand<Result<GenreDto>>;