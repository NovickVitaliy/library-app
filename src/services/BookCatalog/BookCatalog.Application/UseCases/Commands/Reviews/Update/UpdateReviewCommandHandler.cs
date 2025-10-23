using BookCatalog.Application.Contracts.Repositories;
using BookCatalog.Application.DTOs.Reviews.Responses;
using BookCatalog.Application.Mappers;
using BookCatalog.Domain.Models;
using Shared.CQRS.Commands;
using Shared.ErrorHandling;

namespace BookCatalog.Application.UseCases.Commands.Reviews.Update;

public sealed class UpdateReviewCommandHandler : ICommandHandler<UpdateReviewCommand, Result<ReviewDto>>
{
    private readonly IReviewRepository _reviewRepository;
    
    public UpdateReviewCommandHandler(IReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }
    
    public async Task<Result<ReviewDto>> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
    {
        var review = await _reviewRepository.GetByIdAsync(request.ReviewId, cancellationToken);
        if (review is null)
        {
            return Result<ReviewDto>.NotFound(key: request.ReviewId, entityName: nameof(Review));
        }

        review.Rating = request.Request.Rating;
        review.Text = request.Request.Text;

        await _reviewRepository.UpdateAsync(request.ReviewId, review, cancellationToken);
        
        return Result<ReviewDto>.Ok(ReviewMapper.ToDto(review));
    }
}