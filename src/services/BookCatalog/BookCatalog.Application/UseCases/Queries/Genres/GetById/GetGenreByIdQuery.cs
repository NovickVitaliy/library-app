using BookCatalog.Application.DTOs.Genres.Responses;
using Shared.CQRS.Queries;
using Shared.ErrorHandling;

namespace BookCatalog.Application.UseCases.Queries.Genres.GetById;

public record GetGenreByIdQuery(Guid GenreId) : IQuery<Result<GenreDto>>;