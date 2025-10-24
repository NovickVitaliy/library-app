using BookCatalog.Application.Contracts.Repositories;
using BookCatalog.Application.DTOs.Reviews.Responses;
using BookCatalog.Application.Mappers;
using BookCatalog.Domain.Models;
using Shared.CQRS.Queries;
using Shared.DTOs;
using Shared.ErrorHandling;

namespace BookCatalog.Application.UseCases.Queries.Reviews.Get;

public sealed class GetReviewsQueryHandler : IQueryHandler<GetReviewsQuery, Result<PaginationResult<ReviewDto>>>
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IBookRepository _bookRepository;
    
    public GetReviewsQueryHandler(IReviewRepository reviewRepository, IBookRepository bookRepository)
    {
        _reviewRepository = reviewRepository;
        _bookRepository = bookRepository;
    }
    
    public async Task<Result<PaginationResult<ReviewDto>>> Handle(GetReviewsQuery request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIdAsync(request.BookId, cancellationToken);
        if (book is null)
        {
            return Result<PaginationResult<ReviewDto>>.NotFound(key: request.BookId, entityName: nameof(Book));
        }
        var paginationResult = await _reviewRepository.GetAsync(request.BookId, request.Request.PageNumber, request.Request.PageSize, cancellationToken);
        
        return Result<PaginationResult<ReviewDto>>.Ok(paginationResult.ToPaginatedDtos(ReviewMapper.ToDto));
    }
}