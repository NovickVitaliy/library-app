using BookCatalog.Application.Contracts.Repositories;
using BookCatalog.Application.DTOs.Reviews.Responses;
using BookCatalog.Application.Mappers;
using BookCatalog.Domain.Models;
using Shared.CQRS.Queries;
using Shared.ErrorHandling;

namespace BookCatalog.Application.UseCases.Queries.Reviews.GetById;

public sealed class GetReviewByIdQueryHandler : IQueryHandler<GetReviewByIdQuery, Result<ReviewDto>>
{
    private readonly IReviewRepository _reviewRepository;
    
    public GetReviewByIdQueryHandler(IReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }
    
    public async Task<Result<ReviewDto>> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
    {
        var review = await _reviewRepository.GetByIdAsync(request.ReviewId, cancellationToken);

        return review is null
            ? Result<ReviewDto>.NotFound(key: request.ReviewId, entityName: nameof(Review))
            : Result<ReviewDto>.Ok(ReviewMapper.ToDto(review));
    }
}