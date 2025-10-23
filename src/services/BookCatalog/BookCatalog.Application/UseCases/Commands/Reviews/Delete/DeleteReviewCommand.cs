using Shared.CQRS.Commands;
using Shared.ErrorHandling;

namespace BookCatalog.Application.UseCases.Commands.Reviews.Delete;

public record DeleteReviewCommand(Guid ReviewId) : ICommand<Result<bool>>;