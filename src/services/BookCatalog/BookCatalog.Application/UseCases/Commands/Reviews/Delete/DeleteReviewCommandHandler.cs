using BookCatalog.Application.Contracts.Repositories;
using BookCatalog.Domain.Models;
using Shared.CQRS.Commands;
using Shared.ErrorHandling;

namespace BookCatalog.Application.UseCases.Commands.Reviews.Delete;

public sealed class DeleteReviewCommandHandler : ICommandHandler<DeleteReviewCommand, Result<bool>>
{
    private readonly IReviewRepository _reviewRepository;
    
    public DeleteReviewCommandHandler(IReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }
    
    public async Task<Result<bool>> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
    {
        var success = await _reviewRepository.DeleteAsync(request.ReviewId, cancellationToken);

        return success
            ? Result<bool>.NoContent()
            : Result<bool>.NotFound(key: request.ReviewId, entityName: nameof(Review));
    }
}