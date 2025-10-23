using BookCatalog.Application.DTOs.Reviews.Requests;
using BookCatalog.Application.DTOs.Reviews.Responses;
using Shared.CQRS.Commands;
using Shared.ErrorHandling;

namespace BookCatalog.Application.UseCases.Commands.Reviews.Update;

public record UpdateReviewCommand(Guid ReviewId, UpdateReviewRequest Request) : ICommand<Result<ReviewDto>>;