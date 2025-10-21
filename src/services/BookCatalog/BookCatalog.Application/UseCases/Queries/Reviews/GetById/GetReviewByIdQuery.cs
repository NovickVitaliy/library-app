using BookCatalog.Application.DTOs.Reviews.Responses;
using Shared.CQRS.Queries;
using Shared.ErrorHandling;

namespace BookCatalog.Application.UseCases.Queries.Reviews.GetById;

public record GetReviewByIdQuery(Guid ReviewId) : IQuery<Result<ReviewDto>>;