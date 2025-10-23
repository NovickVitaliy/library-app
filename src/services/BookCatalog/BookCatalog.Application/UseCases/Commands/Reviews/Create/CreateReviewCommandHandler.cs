using BookCatalog.Application.Contracts.Repositories;
using BookCatalog.Application.DTOs.Reviews.Responses;
using BookCatalog.Application.Mappers;
using BookCatalog.Domain.Models;
using Shared.CQRS.Commands;
using Shared.ErrorHandling;

namespace BookCatalog.Application.UseCases.Commands.Reviews.Create;

public sealed class CreateReviewCommandHandler : ICommandHandler<CreateReviewCommand, Result<ReviewDto>>
{
    private readonly IReviewRepository _reviewRepository;
    
    public CreateReviewCommandHandler(IReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }
    
    public async Task<Result<ReviewDto>> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        var review = new Review()
        {
            ReviewId = Guid.CreateVersion7(),
            BookId = request.Request.BookId,
            Rating = request.Request.Rating,
            Text = request.Request.Text,
            UserId = request.Request.UserId,
            CreatedDate = DateTimeOffset.UtcNow
        };

        review = await _reviewRepository.CreateAsync(review, cancellationToken);
        
        return Result<ReviewDto>.Created($"/api/reviews/{review.ReviewId}", ReviewMapper.ToDto(review));
    }
}