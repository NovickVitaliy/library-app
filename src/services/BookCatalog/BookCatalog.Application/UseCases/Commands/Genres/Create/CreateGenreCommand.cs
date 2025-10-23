using BookCatalog.Application.DTOs.Genres.Requests;
using BookCatalog.Application.DTOs.Genres.Responses;
using Shared.CQRS.Commands;
using Shared.ErrorHandling;

namespace BookCatalog.Application.UseCases.Commands.Genres.Create;

public record CreateGenreCommand(CreateGenreRequest Request) : ICommand<Result<GenreDto>>;