using BookCatalog.Application.DTOs.Reviews.Requests;
using BookCatalog.Application.DTOs.Reviews.Responses;
using Shared.CQRS.Commands;
using Shared.ErrorHandling;

namespace BookCatalog.Application.UseCases.Commands.Reviews.Create;

public record CreateReviewCommand(CreateReviewRequest Request) : ICommand<Result<ReviewDto>>;