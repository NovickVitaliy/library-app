using BookCatalog.Application.Contracts.Repositories;
using BookCatalog.Application.DTOs.Reviews.Responses;
using BookCatalog.Application.Mappers;
using Shared.CQRS.Queries;
using Shared.DTOs;
using Shared.ErrorHandling;

namespace BookCatalog.Application.UseCases.Queries.Reviews.Get;

public sealed class GetReviewsQueryHandler : IQueryHandler<GetReviewsQuery, Result<PaginationResult<ReviewDto>>>
{
    private readonly IReviewRepository _reviewRepository;
    
    public GetReviewsQueryHandler(IReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }
    
    public async Task<Result<PaginationResult<ReviewDto>>> Handle(GetReviewsQuery request, CancellationToken cancellationToken)
    {
        var paginationResult = await _reviewRepository.GetAsync(request.BookId, request.Request.PageNumber, request.Request.PageSize, cancellationToken);
        
        return Result<PaginationResult<ReviewDto>>.Ok(paginationResult.ToPaginatedDtos(ReviewMapper.ToDto));
    }
}