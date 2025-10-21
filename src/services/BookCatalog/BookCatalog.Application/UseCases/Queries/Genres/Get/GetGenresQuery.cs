using BookCatalog.Application.DTOs.Genres.Responses;
using Shared.CQRS.Queries;
using Shared.DTOs;
using Shared.ErrorHandling;

namespace BookCatalog.Application.UseCases.Queries.Genres.Get;

public record GetGenresQuery(PaginationRequest Request) : IQuery<Result<PaginationResult<GenreDto>>>;