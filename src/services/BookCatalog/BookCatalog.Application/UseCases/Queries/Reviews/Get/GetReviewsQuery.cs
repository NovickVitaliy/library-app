using BookCatalog.Application.DTOs.Reviews.Responses;
using Shared.CQRS.Queries;
using Shared.DTOs;
using Shared.ErrorHandling;

namespace BookCatalog.Application.UseCases.Queries.Reviews.Get;

public record GetReviewsQuery(Guid BookId, PaginationRequest Request) : IQuery<Result<PaginationResult<ReviewDto>>>;